using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Transform gameOver;
	public Transform youWin;

	bool isAlive;
	bool isWinner;
	public bool Winner { get { return isWinner; } }

	// Use this for initialization
	void Start () 
	{
		isAlive = true;
		isWinner = false;
		gameOver.gameObject.SetActive (false);
		youWin.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isAlive)
		{
			gameOver.gameObject.SetActive (true);
		}
		if (isWinner)
		{
			youWin.gameObject.SetActive (true);
		}
		if (transform.position.y < -3)
		{
			youWin.gameObject.SetActive (true);
		}
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.name == "The Enemy" && !isWinner)
		{
			TheEnemy e = coll.gameObject.GetComponent<TheEnemy> ();

			kill ();

		}
		if (coll.gameObject.name == "Escape" && !isWinner)
		{
			isWinner = true;
		}
	}
		
	public void kill()
	{
		isAlive = false;
	}
}
