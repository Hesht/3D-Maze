using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Transform gameOver;
	public Transform youWin;

	bool isAlive;
	bool isWinner;
	public bool Winner { get { return isWinner; } }
	public float countdownToGameOver;
	public string gameOverSceneName;

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
			countdownToGameOver -= Time.deltaTime;
			if (countdownToGameOver <= 0)
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene (gameOverSceneName);
			}
		}
		if (transform.position.y < -3)
		{
			isWinner = true;
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
