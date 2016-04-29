using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MyBullet : NetworkBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		if(!hasAuthority)
		{
			return;
		}

		var hit = collision.gameObject;
		var health = hit.GetComponent<Health>();

		if(health != null)
		{
			health.TakeDamage(10);
		}

		Destroy(gameObject);


	}
}