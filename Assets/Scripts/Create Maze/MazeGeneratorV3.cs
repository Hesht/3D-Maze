using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class MazeGeneratorV3 : MonoBehaviour {
	public Transform corridor;
	public Transform deadEnd;
	public Transform turnRight;
	public Transform turnLeft;
	public Transform junction;
	public Transform noWay;
	public int height;
	public int width;
	public Vector3 target;
	public Vector3 startpoint;

	private Dictionary<Vector3, Transform> map = new Dictionary<Vector3, Transform>();
	// Use this for initialization
	void Start () {
		target = new Vector3 (Random.Range (0, width), 0, 0);
		startpoint = new Vector3(0,0,Random.Range(0,height));
		if(height == 0)
		{
			height = 5;
		}
		if(width == 0)
		{
			width = 5;
		}
		//edges ();
		//buildMaze();
		for (int i = 0; i < 5; i++) {
			//buildMaze(false);
		}
	}

	// Update is called once per frame
	void Update () {

	}

	private void makeWinnable()
	{
		bool win = false;
		List<Vector2> lookAround = new List<Vector2>();
		Vector2 lastMove = new Vector2(0,0);
		List<Transform> pathNotTaken = new List<Transform>();

		lookAround.Add (new Vector2 (1, 0));
		lookAround.Add (new Vector2 (-1, 0));
		lookAround.Add (new Vector2 (0, 1));
		lookAround.Add (new Vector2 (0, -1));

		Transform currentPiece = map [startpoint];

		while (!win) 
		{
			MazePiece p = currentPiece.GetComponent<MazePiece> ();

			foreach (Vector2 look in lookAround) 
			{
				if(p.waypoints.Contains(look) && look != lastMove * -1)
				{
					Vector3 checkThis = new Vector3 (currentPiece.position.x + (look.y * 5), 0, currentPiece.position.y + (look.y * 5));
					if(map.ContainsKey(checkThis))
					{
						pathNotTaken.Add(map [checkThis]);
					}
				}	
			}
			if (pathNotTaken.Count > 0) 
			{

			}
		}
	}

	private void buildMaze()
	{
		bool win = false;
		List<Vector2> lookAround = new List<Vector2>();
		Vector2 lastMove = new Vector2(0,0);
		List<Transform> pathNotTaken = new List<Transform>();

		lookAround.Add (new Vector2 (1, 0));
		lookAround.Add (new Vector2 (-1, 0));
		lookAround.Add (new Vector2 (0, 1));
		lookAround.Add (new Vector2 (0, -1));

		Transform currentPiece = map [startpoint];

		while (!win) 
		{
			MazePiece p = currentPiece.GetComponent<MazePiece> ();

			foreach (Vector2 look in lookAround) 
			{
				if(p.waypoints.Contains(look))
				{
					Vector3 checkThis = new Vector3 (currentPiece.position.x + (look.y * 5), 0, currentPiece.position.y + (look.y * 5));

					if(map.ContainsKey(checkThis))
					{
						pathNotTaken.Add(map [checkThis]);
					}
				}	
			}
			if (pathNotTaken.Count > 0) 
			{

			}
		}
	}

	private Transform choosePiece(Vector3 look)
	{
		Transform piece = noWay;

		return piece;
	}
}
