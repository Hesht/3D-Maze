using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Transform gameOver;

	bool isAlive;

	// Use this for initialization
	void Start () {
		isAlive = true;
		gameOver.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isAlive)
		{
			gameOver.gameObject.SetActive (true);
		}
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.name == "The Enemy")
		{
			TheEnemy e = coll.gameObject.GetComponent<TheEnemy> ();
			if (e.countDown <= 0)
			{
				isAlive = false;
			}
		}
	}
		
	public void kill()
	{
		isAlive = false;
	}
}
