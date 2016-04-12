using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

	public PlayerMove instigator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!hasAuthority)
		{
			return;
		}


	}

	void OnCollisionEnter (Collision col)
	{
		if(!hasAuthority)
		{
			return;
		}


		var player = col.gameObject.GetComponent<PlayerMove>();
		if(player != instigator)
		{
			player.RpcHitByBullet(5);
		}
	}
}
