using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Health : NetworkBehaviour {

	public bool destroyOnDeath;

	public RectTransform healthBar;

	public const int maxHealth = 100;

	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void TakeDamage(int amount)
	{
		if(!isServer)
		{
			return;
		}
		currentHealth -= amount;
		if (currentHealth <= 0)
		{
			
			currentHealth = maxHealth;
			if(destroyOnDeath)
			{
				Destroy(gameObject);
			}
			else
			{
				RpcRespawn();
			}

		}


	}

	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer)
		{
			// move back to zero location
			var networkTransform = NetworkManager.singleton.GetStartPosition();
			transform.position = networkTransform.position;
			transform.rotation = networkTransform.rotation;

		}
	}

	void OnChangeHealth (int health)
	{
		healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
	}
}
