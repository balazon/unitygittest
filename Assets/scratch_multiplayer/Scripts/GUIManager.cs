using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public static GUIManager Instance { get; private set;}

	public GameObject healthBarPrefab;

	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnCombatSpawned(Combat combat)
	{
		var hb = Instantiate(healthBarPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		hb.transform.SetParent(GameObject.Find("Canvas").transform, false);
		hb.GetComponent<HealthBar>().SetCombat(combat);
	}
}
