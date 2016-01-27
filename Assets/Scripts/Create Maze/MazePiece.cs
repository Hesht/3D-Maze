using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazePiece : MonoBehaviour {
	public List<Vector2> waypoints = new List<Vector2>();
	public float rotation = 0.0f;
	int rotTried = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int checkNearby(ref Transform piece, List<Vector2> left, List<Vector2> right, List<Vector2> up, List<Vector2> down)
	{
		int path = 0;

		foreach (Vector2 way in waypoints) 
		{
			foreach (Vector2 point in left) 
			{
				if (point.x == -1 && way.x == 1) 
				{
					path++;
				}
			}

			foreach (Vector2 point in right) 
			{
				if (point.x == 1 && way.x == -1) 
				{
					path++;
				}
			}

			foreach (Vector2 point in up) 
			{
				if (point.y == -1 && way.y == 1) 
				{
					path++;
				}
			}

			foreach (Vector2 point in down) 
			{
				if (point.y == 1 && way.y == -1) 
				{
					path++;
				}
			}

			//For rotation, switch the x/y values and multiply by minus 1
		}
		if (path <= 0) 
		{
			if (rotTried < 4) 
			{
				rotTried++;
				rotation += 90;
				piece.Rotate (new Vector3 (0, 90, 0));

				rotatePoints ();

				return checkNearby (ref piece, left, right, up, down);
			} 
			else 
			{
				return 0;
			}
		}
		return path;
	}

	public void rotatePoints()
	{
		int rots = (int)rotation / 90;

		for (int j = 0; j < rots; j++) {
			for (int i = 0; i < waypoints.Count; i++) {
				if (waypoints [i] == new Vector2 (1, 0)) {
					waypoints [i] = new Vector2 (0, 1);
				} else if (waypoints [i] == new Vector2 (-1, 0)) {
					waypoints [i] = new Vector2 (0, -1);
				} else if (waypoints [i] == new Vector2 (0, 1)) {
					waypoints [i] = new Vector2 (-1, 0);
				} else if (waypoints [i] == new Vector2 (0, -1)) {
					waypoints [i] = new Vector2 (1, 0);
				}
			}
		}
	}
}
