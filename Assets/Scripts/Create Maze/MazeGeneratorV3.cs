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
	public Transform player;
	public Transform exit;
	public Dictionary<Vector3, Transform> Map{get{return map;}}

	private Dictionary<Vector3, Transform> map = new Dictionary<Vector3, Transform>();
	private List<Transform> deadEnds;
	// Use this for initialization
	void Start () {
		//target = new Vector3 (Random.Range (0, width), 0, 0);
		startpoint = new Vector3(0,0,(Random.Range(1,height - 1)) * 5);
		if(height == 0)
		{
			height = 5;
		}
		if(width == 0)
		{
			width = 5;
		}
		while((map.Count < ((height + width) * 2) + (height * 2)))
		{
			edges ();
			buildMaze();

			if (!(map.Count > ((height + width) * 2) + (height * 2))) 
			{
				clearMap ();
			}
		}

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		makeWinnable ();
		

		GameObject pl = GameObject.FindGameObjectWithTag ("Player");
		pl.transform.position = new Vector3(5, 0, startpoint.z);
	}

	private void clearMap()
	{
		foreach (KeyValuePair<Vector3,Transform> p in map) 
		{
			Destroy (p.Value.gameObject);
		}
		map = new Dictionary<Vector3, Transform> ();
	}

	private void edges()
	{
		for (int i = 0; i < width; i++) {
			Transform piece = (Transform)Instantiate (deadEnd);
			piece.transform.position = new Vector3 (i * 5, 0, 0);
			piece.Rotate(new Vector3(0, 90, 0));
			piece.GetComponent<MazePiece> ().rotation = 90;
			piece.GetComponent<MazePiece> ().rotatePoints (0);
			map.Add (piece.transform.position, piece);

		}
		for (int j = 0; j < height; j++) {
			Transform piece = (Transform)Instantiate (deadEnd);
			piece.transform.position = new Vector3 (0, 0, j * 5);
			if (!map.ContainsKey (piece.transform.position)) {
				piece.Rotate (new Vector3 (0, 180, 0));
				piece.GetComponent<MazePiece> ().rotation = 180;
				piece.GetComponent<MazePiece> ().rotatePoints ();
				map.Add (piece.transform.position, piece);
			}
		}
		for (int j = 0; j < height; j++) {
			Transform piece = (Transform)Instantiate (deadEnd);
			piece.transform.position = new Vector3 (width * 5, 0, j * 5);
			if (!map.ContainsKey (piece.transform.position)) {
				piece.Rotate (new Vector3 (0, 0, 0));
				piece.GetComponent<MazePiece> ().rotation = 0;
				piece.GetComponent<MazePiece> ().rotatePoints (0);
				map.Add (piece.transform.position, piece);
			}
		}
		for (int i = 0; i < width; i++) {
			Transform piece = (Transform)Instantiate (deadEnd);
			piece.transform.position = new Vector3 (i * 5, 0, height * 5);
			if (!map.ContainsKey (piece.transform.position)) {
				piece.Rotate (new Vector3 (0, 270, 0));
				piece.GetComponent<MazePiece> ().rotation = 270;
				piece.GetComponent<MazePiece> ().rotatePoints ();
				map.Add (piece.transform.position, piece);
			}
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
		Dictionary<Vector2, KeyValuePair<Vector2, Transform>> pathNotTaken = new Dictionary<Vector2, KeyValuePair<Vector2, Transform>>();
		List<Transform> deadEnds = new List<Transform> ();
		List<Transform> used = new List<Transform> ();

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
					Vector3 checkThis = new Vector3 (currentPiece.position.x + (look.x * 5), 0, currentPiece.position.z + (look.y * 5));
					if(map.ContainsKey(checkThis) && !used.Contains(map[checkThis]) && !pathNotTaken.ContainsKey(new Vector2(checkThis.x, checkThis.z)))
					{
						try
						{
							KeyValuePair<Vector2, Transform> point = new KeyValuePair<Vector2, Transform>(look, map[checkThis]);
							pathNotTaken.Add(new Vector2(checkThis.x, checkThis.z), point);
						}
						catch
						{
							Debug.Log (checkThis.ToString());
							Debug.Log (pathNotTaken[new Vector2(checkThis.x, checkThis.z)].ToString());
							throw new UnityException ();
						}

					}
				}	
			}
			if (pathNotTaken.Count > 0) 
			{
				KeyValuePair<Vector2, KeyValuePair<Vector2, Transform>> pair = new KeyValuePair<Vector2, KeyValuePair<Vector2, Transform>> ();
				foreach (KeyValuePair<Vector2, KeyValuePair<Vector2, Transform>> point in pathNotTaken) 
				{
					pair = point;
					currentPiece = point.Value.Value;
					lastMove = point.Value.Key;
					break;
				}
				used.Add (pair.Value.Value);
				pathNotTaken.Remove (pair.Key);
			} 
			else 
			{
				int ran = Random.Range (0, deadEnds.Count);
				try
				{
					Transform winner = (Transform)Instantiate (exit);
					winner.position = deadEnds [ran].position;
					winner.rotation = deadEnds[ran].rotation;
					Destroy (deadEnds [ran].gameObject);
					win = true;
				}
				catch 
				{
					Debug.Log (ran.ToString());
				}
				finally 
				{
					win = true;
				}
			}
			if (currentPiece.tag == "Dead End" && currentPiece.position != startpoint && !(currentPiece.position.x == 0 && currentPiece.position.y == height * 5)
				&& !(currentPiece.position.x == width * 5 && currentPiece.position.y == 0) && !(currentPiece.position.x == 0 && currentPiece.position.y == 0) ) 
			{
				deadEnds.Add (currentPiece);
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
					Vector3 checkThis = new Vector3 (currentPiece.position.x + (look.x * 5), 0, currentPiece.position.z + (look.y * 5));

					if(!map.ContainsKey(checkThis))
					{
						Transform piece = choosePiece (look, checkThis);
						pathNotTaken.Add (piece);
					}
				}	
			}
			if (pathNotTaken.Count > 0) 
			{
				currentPiece = pathNotTaken [0];
				pathNotTaken.RemoveAt(0);
			} 
			else 
			{
				win = true;
			}
		}
	}

	private Transform choosePiece(Vector3 look, Vector3 pos)
	{
		Transform piece = noWay;

		if (pos.x < width && pos.x > 0 && pos.y < height && pos.y > 0) 
		{
			piece = (Transform)Instantiate (deadEnd);
			piece.transform.position = pos;

			int paths = piece.GetComponent<MazePiece> ().checkNearby (ref piece, look);

			map.Add (pos, piece);
		} 
		else 
		{
			int type = Random.Range (0, 3);
			int rotType = Random.Range (1, 5);
			int rot = 0;

			if (rotType == 1) {
				rot = 90;
			} else if (rotType == 2) {
				rot = 180;
			} else if (rotType == 3) {
				rot = 270;
			}
			if (type == 0) 
			{
				piece = (Transform)Instantiate (corridor);
				piece.transform.position = pos;
				piece.transform.Rotate (new Vector3 (0, rot, 0));
				piece.GetComponent<MazePiece> ().rotation = rot;
				piece.GetComponent<MazePiece> ().rotatePoints ();
				int paths = piece.GetComponent<MazePiece> ().checkNearby (ref piece, look);

				if (paths == 0) {
					Destroy (piece.gameObject);
					type = 1;
				} else {
					map.Add (pos, piece);
				}
			} 
			if (type == 1) 
			{
				piece = (Transform)Instantiate (turnRight);
				piece.transform.position = pos;
				piece.transform.Rotate (new Vector3 (0, rot, 0));
				piece.GetComponent<MazePiece> ().rotation = rot;
				piece.GetComponent<MazePiece> ().rotatePoints ();
				int paths = piece.GetComponent<MazePiece> ().checkNearby (ref piece, look);

				if (paths == 0) {
					Destroy (piece.gameObject);
					type = 2;
				}else {
					map.Add (pos, piece);
				}
			} 
			if (type == 2) 
			{
				piece = (Transform)Instantiate (junction);
				piece.transform.position = pos;
				piece.transform.Rotate (new Vector3 (0, rot, 0));
				piece.GetComponent<MazePiece> ().rotation = rot;
				piece.GetComponent<MazePiece> ().rotatePoints ();
				piece.GetComponent<MazePiece> ().checkNearby (ref piece, look);

				map.Add (pos, piece);
			} 
		}

		return piece;
	}
}
