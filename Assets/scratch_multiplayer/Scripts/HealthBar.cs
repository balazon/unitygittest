using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public Combat combat;

	float _percent;
	public float Percent
	{
		get { return _percent;}
		set
		{
			_percent = Mathf.Clamp(value, 0, 1.0f);
			image.fillAmount = _percent;
		}
	}

	Image image;
	Camera cam;

	void Awake()
	{
		image = transform.FindChild("Image").GetComponent<Image>();
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	public void SetCombat(Combat c)
	{
		combat = c;
		Percent = (float) combat.health / Combat.maxHealth;
		SetPosition(combat.gameObject.transform.position + Vector3.down * 1.0f);
	}

	void SetPosition(Vector3 worldPos)
	{
		var screenPos = cam.WorldToScreenPoint(worldPos);
		screenPos.z = 0;
		screenPos -= new Vector3(0, 40.0f, 0);
		gameObject.transform.position = screenPos;
	}

//	// Use this for initialization
//	void Start () {
//	
//	}
//	
	// Update is called once per frame
	void Update () {
		if(combat == null)
		{
			Destroy(gameObject);
			return;
		}

		Percent = (float) combat.health / Combat.maxHealth;
		SetPosition(combat.gameObject.transform.position);
	}
}
