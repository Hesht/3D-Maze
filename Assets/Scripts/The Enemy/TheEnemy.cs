using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheEnemy : MonoBehaviour {

	public Vector3 pos;
	public float countDown;
	public float speed;
	public Transform p;
	public AudioClip step1;
	public AudioClip step2;

	bool one = true;
	AudioSource source;
	List<Transform> path;
	Transform target;
	TrackPlayer player;
	Player _player;

	// Use this for initialization
	void Start () {
		player = p.GetComponent<TrackPlayer> ();
		_player = p.GetComponent<Player> ();
		source = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (countDown > 0) 
		{
			countDown -= Time.deltaTime;
		} 
		else
		{
			itFollows ();
		}
	}

	private void itFollows()
	{
		if (player.Path.Count > 0)
		{
			transform.position = Vector3.MoveTowards (transform.position, player.Path [0], speed);
			if (transform.position == player.Path [0])
			{
				player.removePoint ();
			}
			if (!source.isPlaying)
			{
				if (one)
				{
					source.clip = step1;
					one = false;
				}
				else
				{
					source.clip = step2;
					one = true;
				}
				source.Play ();
			}
		}
		else
		{
			//KILL KILLLLLL!.... Also kill if they intersect.
			_player.kill();
		}
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.name == "Floor")
		{
			if (player.Path.Count > 0)
			{
				if (coll.transform.parent.position == player.Path [0])
				{
					player.removePoint ();
				}
			}
		}
		else if (countDown <= 0 && coll.gameObject.name == "Player")
		{
			
		}
	}
}
