using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheEnemy : MonoBehaviour {

	public Vector3 pos;
	public float countDown;
	public float speed;
	public Transform p;
	List<Transform> path;
	Transform target;
	TrackPlayer player;

	// Use this for initialization
	void Start () {
		player = p.GetComponent<TrackPlayer> ();
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
		}
		else
		{
			//KILL KILLLLLL!.... Also kill if they intersect.
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
	}
}
