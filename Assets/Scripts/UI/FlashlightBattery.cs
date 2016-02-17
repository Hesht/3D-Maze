using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashlightBattery : MonoBehaviour {
	public Flashlight _light;
	// Use this for initialization
	void Start () {
		_light = GameObject.FindGameObjectWithTag ("Flashlight").GetComponent<Flashlight>();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().text = "Battery: " + _light.battery.ToString ();
	}
}
