using UnityEngine;
using System.Collections;

public class ActivateEnemy : MonoBehaviour {
	public Transform theEnemy;
	public float countDown;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (countDown > 0)
		{
			countDown -= Time.deltaTime;
		}
		else
		{
			theEnemy.gameObject.SetActive (true);
			gameObject.SetActive (false);
		}
	}

	public void deactivateEnemy()
	{
		theEnemy.gameObject.SetActive (false);
	}
}
