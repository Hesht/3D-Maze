using UnityEngine;
using System.Collections;

public class Battery : MonoBehaviour {
	Flashlight _light;
	// Use this for initialization
	void Start () {
		_light = GameObject.FindGameObjectWithTag ("Flashlight").GetComponent<Flashlight>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.name == "FPSController")
		{
			_light.spares++;
			Destroy (gameObject.transform.parent.gameObject);
		}
	}
}
