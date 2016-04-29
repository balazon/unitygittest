﻿using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
	public GameObject bulletPrefab;

	public Transform bulletSpawn;



	void Update()
	{
		if(!isLocalPlayer)
		{
			return;
		}

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

		if(Input.GetKeyDown(KeyCode.Space))
		{
			CmdFire();
		}
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}

	[Command]
	public void CmdFire()
	{
		var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6.0f;
		Destroy(bullet, 2.0f);

		NetworkServer.Spawn(bullet);
	}
		
}