using UnityEngine;
using UnityEngine.Networking;

public class Combat : NetworkBehaviour 
{
	public const int maxHealth = 10;
	public bool destroyOnDeath;

	[SyncVar]
	public int health = maxHealth;


	void Awake()
	{
		Reset();
		GUIManager.Instance.OnCombatSpawned(this);
	}

	public void Reset()
	{
		health = maxHealth;
	}


	public void TakeDamage(int amount)
	{
		if(!isServer)
		{
			return;
		}
		if(health > 0)
		{
			health -= amount;
			if(health <= 0)
			{
				if(destroyOnDeath)
				{
					Destroy(gameObject);
				}
				else
				{
					Reset();
					RpcRespawn();
				}
			}
		}


//		health -= amount;
//		if (health <= 0)
//		{
//			health = 0;
//			Debug.Log("Dead!");
//		}
	}

	[ClientRpc]
	public void RpcRespawn()
	{
		if(isLocalPlayer)
		{
			transform.position = new Vector3(0, 0.5f, 0);
		}

	}


}