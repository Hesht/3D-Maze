using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheEnemy : MonoBehaviour {

	public Vector3 pos;
	public float countDown;
	public float speed;
	public TrackPlayer player;
	public MazeGeneratorV3 maze;
	public Vector2 look;
	List<Transform> path;
	Transform target;

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
			itFollows ();
		}
	}

	private void itFollows()
	{
		
	}

	private void getBestPath()
	{
		Vector3 posCheck = player.piecePosition;
		target = maze.Map[posCheck];

		foreach(KeyValuePair<Vector3, Transform> piece in maze.Map)
		{
			//It needs to know when it decided to go one way rather than another
			//Also it needs to know the entire path, maybe also add in something to change the current path rather than completely recalculate it each time?
		}
	}
}
