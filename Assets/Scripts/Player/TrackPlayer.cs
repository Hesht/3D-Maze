using UnityEngine;
using System.Collections;

public class TrackPlayer : MonoBehaviour {
	public Vector3 piecePosition;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collider coll)
	{
		piecePosition = coll.transform.position;
	}
}
