using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {
	public bool _isOn = true;
	public float battery = 100;
	public int spares = 0;
	Light light;
	// Use this for initialization
	void Start () {
		light = GameObject.FindGameObjectWithTag ("Light").GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0) && battery > 0) {
			toggleOn ();
		}
		if (_isOn) {
			battery -= Time.deltaTime;
			flicker ();
		}
		if (battery <= 0 && _isOn) 
		{
			if (spares > 0)
			{
				loadNew ();
			}
			else
			{
				toggleOn ();
			}
		}
	}

	private void toggleOn()
	{
		if (_isOn) {
			_isOn = false;
			switchOnOff ();
		} else {
			_isOn = true;
			switchOnOff ();
		}
	}

	private void switchOnOff()
	{
		if (light.intensity == 0) 
		{
			light.intensity = 1.0f;
		} 
		else 
		{
			light.intensity = 0;
		}
	}

	private void flicker()
	{
		if (light.intensity > 0) {
			float rnd = Random.Range (battery/100, 1.0f);
			light.intensity = rnd;
		}
	}

	private void loadNew()
	{
		spares--;
		battery = 100;
	}
}
