using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

	public GameObject instigator;

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

	void OnTriggerEnter(Collider other)
	{
//		if(!hasAuthority)
//		{
//			
//			return;
//		}


		//Debug.LogFormat("BulletHit");

		var combat = other.gameObject.GetComponent<Combat>();

		if(combat != null && other.gameObject != instigator)
		{
			
			combat.TakeDamage(5);
			Destroy(gameObject);
		}

	}


}
