using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashlightBattery : MonoBehaviour {
	public Flashlight light;
	// Use this for initialization
	void Start () {
		light = GameObject.FindGameObjectWithTag ("Flashlight").GetComponent<Flashlight>();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().text = "Battery: " + light.battery.ToString ();
	}
}
