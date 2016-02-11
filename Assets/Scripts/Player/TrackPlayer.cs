using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackPlayer : MonoBehaviour {
	public Vector3 piecePosition;
	public List<Vector3> Path { get { return _path; } }
	List<Vector3> _path;
	// Use this for initialization
	void Start () {
		_path = new List<Vector3> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.name == "Floor")
		{
			piecePosition = coll.transform.parent.position;
			Vector3 lastPiece = new Vector3 (0,1000,0);
			if (_path.Count > 0) 
			{
				lastPiece = _path [_path.Count - 1];
			}
			if (!(piecePosition == lastPiece))
			{
				_path.Add (piecePosition);
			}
		}
	}

	void OnCollisionEnter(Collision coll)
	{
		piecePosition = coll.collider.transform.position;
		Vector3 lastPiece = new Vector3 (0,1000,0);
		if (_path.Count > 1) 
		{
			lastPiece = _path [_path.Count - 1];
		}
		if (!(piecePosition == lastPiece))
		{
			_path.Add (piecePosition);
		}
	}

	public void removePoint()
	{
		_path.RemoveAt (0);
	}
}
