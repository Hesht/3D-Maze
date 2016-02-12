using UnityEngine;
using System.Collections;

public class Battery : MonoBehaviour {
	Flashlight light;
	// Use this for initialization
	void Start () {
		light = GameObject.FindGameObjectWithTag ("Flashlight").GetComponent<Flashlight>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.name == "FPSController")
		{
			light.spares++;
			Destroy (gameObject.transform.parent.gameObject);
		}
	}
}
