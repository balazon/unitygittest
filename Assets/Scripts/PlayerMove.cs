using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
	

	int health;

	public GameObject bulletPrefab;

	Camera cam;

	void Update()
	{
		if(!isLocalPlayer)
		{
			return;
		}
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 4.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 4.0f;


		transform.Translate(x, 0, z,  Space.World);



		var fwd = Input.mousePosition - cam.WorldToScreenPoint(transform.position);
		Debug.LogFormat("mouse: {0}, tr: {1}, fwd: {2}", Input.mousePosition, cam.WorldToScreenPoint(transform.position), fwd);
		fwd.z = fwd.y;
		fwd.y = 0;
		if(fwd.magnitude > 0.01)
		{
			transform.rotation = Quaternion.LookRotation(fwd);
		}


		if(Input.GetKeyDown(KeyCode.Space))
		{
			Fire();
		}
	}

	public override void OnStartLocalPlayer()
	{
		health = 20;
		GetComponent<MeshRenderer>().material.color = Color.red;
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	void Fire()
	{
		var go = Instantiate(bulletPrefab, transform.position + transform.forward * 0.7f, Quaternion.identity) as GameObject;
		go.GetComponent<Rigidbody>().velocity = transform.forward * 4.0f;
		go.GetComponent<Bullet>().instigator = this;

		NetworkServer.Spawn(go);

		Destroy(go, 2.0f);
	}

	[ClientRpc]
	public void RpcHitByBullet(int dmg)
	{
		if(health > 0)
		{
			health -= dmg;
			if(health <= 0)
			{
				//respawn
				health = 20;
				transform.position = Vector3.zero;

			}
		}
	}


}