using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Spares : MonoBehaviour {
	Flashlight flashLight;
	// Use this for initialization
	void Start () {
		flashLight = GameObject.FindGameObjectWithTag ("Flashlight").GetComponent<Flashlight>();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().text = "Spare Batteries: \n" + flashLight.spares.ToString ();
	}
}
