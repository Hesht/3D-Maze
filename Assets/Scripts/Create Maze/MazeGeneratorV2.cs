using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class MazeGeneratorV2 : MonoBehaviour {
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
		edges ();
		buildMaze();
		for (int i = 0; i < 5; i++) {
			buildMaze(false);
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

	private void edges()
	{
		for (int i = 0; i < width; i++) {
			Transform piece = (Transform)Instantiate (deadEnd);
			piece.transform.position = new Vector3 (i * 5, 0, 0);
			piece.Rotate(new Vector3(0, 90, 0));
			piece.GetComponent<MazePiece> ().rotation = 90;
			piece.GetComponent<MazePiece> ().rotatePoints (90);
			map.Add (piece.transform.position, piece);

		}
		for (int j = 0; j < height; j++) {
			Transform piece = (Transform)Instantiate (deadEnd);
			piece.transform.position = new Vector3 (0, 0, j * 5);
			if (!map.ContainsKey (piece.transform.position)) {
				piece.Rotate (new Vector3 (0, 180, 0));
				piece.GetComponent<MazePiece> ().rotation = 180;
				piece.GetComponent<MazePiece> ().rotatePoints (180);
				map.Add (piece.transform.position, piece);
			}
		}
		for (int j = 0; j < height; j++) {
			Transform piece = (Transform)Instantiate (deadEnd);
			piece.transform.position = new Vector3 (width * 5, 0, j * 5);
			if (!map.ContainsKey (piece.transform.position)) {
				piece.Rotate (new Vector3 (0, 0, 0));
				piece.GetComponent<MazePiece> ().rotation = 0;
				piece.GetComponent<MazePiece> ().rotatePoints ();
				map.Add (piece.transform.position, piece);
			}
		}
		for (int i = 0; i < width; i++) {
			Transform piece = (Transform)Instantiate (deadEnd);
			piece.transform.position = new Vector3 (i * 5, 0, height * 5);
			if (!map.ContainsKey (piece.transform.position)) {
				piece.Rotate (new Vector3 (0, 270, 0));
				piece.GetComponent<MazePiece> ().rotation = 270;
				piece.GetComponent<MazePiece> ().rotatePoints (270);
				map.Add (piece.transform.position, piece);
			}
		}
	}

	public void buildMaze(bool firstBuild = true)
	{
		bool overrideRandom = false;
		int overriden = 0;
		for(int x = 0; x < width; x++)
		{
			for(int z = 0; z < height; z++)
			{
				if (x == 0 && z == 0 && firstBuild) 
				{
					//Transform piece = (Transform)Instantiate (deadEnd);
					//piece.transform.position = new Vector3 (x * 5, 0, z * 5);
					//piece.Rotate(new Vector3(0, 90, 0));
					//piece.GetComponent<MazePiece> ().rotation = 90;
					//piece.GetComponent<MazePiece> ().rotatePoints ();
					//map.Add (piece.transform.position, piece);
				} 
				else 
				{
					int y;
					Vector3 left = new Vector3((x +1) * 5, 0, z*5);
					Vector3 right = new Vector3((x-1) * 5, 0, z*5);
					Vector3 up = new Vector3(x*5, 0, (z-1) * 5);
					Vector3 down = new Vector3(x*5, 0, (z+1) * 5);
					Vector3 pos = new Vector3(x * 5, 0, z * 5);
					int rotType = Random.Range (1, 5);
					int rot = 0;

					if (rotType == 1) {
						rot = 90;
					} else if (rotType == 2) {
						rot = 180;
					} else if (rotType == 3) {
						rot = 270;
					}

					if (!map.ContainsKey (pos)) {
						if ((x > 0 && z > 0) && (x < width - 1 && z < height - 1)) {
							y = Random.Range (1, 4);
						} else if (overrideRandom) {
							y = 0;
						} else{
							y = Random.Range (1, 4);
						}

						if (!firstBuild){
							y = Random.Range (1, 4);
						}

						Transform leftPiece = noWay;
						Transform rightPiece = noWay;
						Transform upPiece = noWay;
						Transform downPiece = noWay;
						if (map.ContainsKey (left)) {
							leftPiece = map [left];
						}
						if (map.ContainsKey (right)) {
							rightPiece = map [right];
						}
						if (map.ContainsKey (up)) {
							upPiece = map [up];
						}
						if (map.ContainsKey (down)) {
							downPiece = map [down];
						}

						if (y == 0) {
							Transform piece = (Transform)Instantiate (deadEnd);
							piece.transform.position = new Vector3 (x * 5, 0, z * 5);
							piece.transform.Rotate (new Vector3 (0, rot, 0));
							piece.GetComponent<MazePiece> ().rotation = rot;
							piece.GetComponent<MazePiece> ().rotatePoints ();

							int paths = piece.GetComponent<MazePiece> ().checkNearby (ref piece, leftPiece.GetComponent<MazePiece> ().waypoints, 
								           rightPiece.GetComponent<MazePiece> ().waypoints, upPiece.GetComponent<MazePiece> ().waypoints, downPiece.GetComponent<MazePiece> ().waypoints);

							if (paths == 0) {
								if (overrideRandom) {
									overrideRandom = false;
								}
								//If there's nowhere to go any of the ways, abort
								Destroy (piece.gameObject);
								y = 1;
							} else if (paths > 0) {
								map.Add (piece.transform.position, piece);
							}
						}

						if (y == 1) {
							Transform piece = (Transform)Instantiate (corridor);
							piece.transform.position = new Vector3 (x * 5, 0, z * 5);
							piece.transform.Rotate (new Vector3 (0, rot, 0));
							piece.GetComponent<MazePiece> ().rotation = rot;
							piece.GetComponent<MazePiece> ().rotatePoints ();
							int paths = piece.GetComponent<MazePiece> ().checkNearby (ref piece, leftPiece.GetComponent<MazePiece> ().waypoints, 
								           rightPiece.GetComponent<MazePiece> ().waypoints, upPiece.GetComponent<MazePiece> ().waypoints, downPiece.GetComponent<MazePiece> ().waypoints);

							if (paths == 0) {
								//If there's nowhere to go any of the ways, abort
								Destroy (piece.gameObject);
								y = 2;
							} else if (paths > 0) {
								map.Add (piece.transform.position, piece);
							}
						}

						if (y == 2) {
							Transform piece = (Transform)Instantiate (junction);
							piece.transform.position = new Vector3 (x * 5, 0, z * 5);
							piece.transform.Rotate (new Vector3 (0, rot, 0));
							piece.GetComponent<MazePiece> ().rotation = rot;
							piece.GetComponent<MazePiece> ().rotatePoints ();

							int paths = piece.GetComponent<MazePiece> ().checkNearby (ref piece, leftPiece.GetComponent<MazePiece> ().waypoints, 
								           rightPiece.GetComponent<MazePiece> ().waypoints, upPiece.GetComponent<MazePiece> ().waypoints, downPiece.GetComponent<MazePiece> ().waypoints);

							if (paths == 0) {
								//If there's nowhere to go any of the ways, abort
								Destroy (piece.gameObject);
								y = 3;
							} else if (paths > 0) {
								map.Add (piece.transform.position, piece);
							}
						}

						if (y == 3) {
							Transform piece = (Transform)Instantiate (turnRight);
							piece.transform.position = new Vector3 (x * 5, 0, z * 5);
							piece.transform.Rotate (new Vector3 (0, rot, 0));
							piece.GetComponent<MazePiece> ().rotation = rot;
							piece.GetComponent<MazePiece> ().rotatePoints ();

							int paths = piece.GetComponent<MazePiece> ().checkNearby (ref piece, leftPiece.GetComponent<MazePiece> ().waypoints, 
								           rightPiece.GetComponent<MazePiece> ().waypoints, upPiece.GetComponent<MazePiece> ().waypoints, downPiece.GetComponent<MazePiece> ().waypoints);

							if (paths == 0) {
								//If there's nowhere to go any of the ways, abort
								Destroy (piece.gameObject);
								overrideRandom = true;
								overriden++;
								if (overriden <= 3) {
									z--;
								}
							} else if (paths > 0) {
								map.Add (piece.transform.position, piece);
							}
						}
					}
				}
			}
		}
	}
}
