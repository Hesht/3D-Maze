using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour {
    public Transform corridor;
    public Transform deadEnd;
    public Transform turnRight;
    public Transform turnLeft;
    public Transform junction;
    public int height;
    public int width;

    private Dictionary<Vector2, Transform> map = new Dictionary<Vector2, Transform>();
    // Use this for initialization
    void Start () {
        if(height == 0)
        {
            height = 5;
        }
        if(width == 0)
        {
            width = 5;
        }

        buildMaze();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void buildMaze()
    {
        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                if(x == 0 && z == 0)
                {
                    Transform piece = (Transform)Instantiate(deadEnd);
                    piece.transform.position = new Vector3(x * 5, 0, z * 5);
					map.Add (piece.transform.position, piece);
                }
                else
                {
                    int y;
					Vector2 left = new Vector2((x +1) * 5, z*5);
					Vector2 right = new Vector2((x-1) * 5, z*5);
					Vector2 up = new Vector2(x*5, (z-1) * 5);
					Vector2 down = new Vector2(x*5, (z+1) * 5);
                    if ((x > 0 && z > 0) && (x < width - 1 && z < height - 1))
                    {
                        y = Random.Range(0, 4);
                    }
                    else
                    {
                        y = Random.Range(1, 4);
                    }

                    if (y == 0)
                    {
                        Transform piece = (Transform)Instantiate(corridor);
                        piece.transform.position = new Vector3(x * 5, 0, z * 5);
                        int paths = checkCorridorPath(0, left, right, up, down);
                        if (paths <= 1)
                        {
                            piece.Rotate(new Vector3(0, 90));
                            paths = checkCorridorPath(90, left, right, up, down);
                            if (paths <= 1)
                            {
                                //If there's nowhere to go any of the ways, abort
                                Destroy(piece.gameObject);
                                y = Random.Range(1, 4);
                            }
                        }
						if (paths > 1) 
						{
							map.Add (piece.transform.position, piece);
						}
                    }
                    if (y == 1)
                    {
                        Transform piece = (Transform)Instantiate(junction);
                        piece.transform.position = new Vector3(x * 5, 0, z * 5);
                        int paths = deadEndChecks(piece, left, right, up, down);
                        if (paths == 0)
                        {
                            piece.Rotate(new Vector3(0, 90));
                            paths = junctionChecks(piece, left, right, up, down);
                            if (paths == 0)
                            {
                                piece.Rotate(new Vector3(0, 180));
                                paths = junctionChecks(piece, left, right, up, down);
                                if (paths == 0)
                                {
                                    piece.Rotate(new Vector3(0, 270));
                                    paths = junctionChecks(piece, left, right, up, down);
                                }
                            }
                        }
                        if (paths == 0)
                        {
                            //If there's nowhere to go any of the ways, abort
                            Destroy(piece.gameObject);
                            y = Random.Range(2, 5);
						}
						else if (paths > 0) 
						{
							map.Add (piece.transform.position, piece);
						}
                    }
                    if (y == 2)
                    {
                        Transform piece = (Transform)Instantiate(deadEnd);
                        piece.transform.position = new Vector3(x * 5, 0, z * 5);
                        int paths = deadEndChecks(piece, left, right, up, down);
                        if (paths == 0 && !((x == 0 && z == height - 1) || (x == width - 1 && z == height - 1)))
                        {
                            piece.Rotate(new Vector3(0, 90));
                            paths = deadEndChecks(piece, left, right, up, down);
                            if (paths == 0 || (x == 0 && (z == 0 || z == width)))
                            {
                                piece.Rotate(new Vector3(0, 180));
                                paths = deadEndChecks(piece, left, right, up, down);
                                if (paths == 0)
                                {
                                    piece.Rotate(new Vector3(0, 270));
                                    paths = deadEndChecks(piece, left, right, up, down);
                                }
                            }
                        }
                        if (paths == 0)
                        {
                            //If there's nowhere to go any of the ways, abort
                            Destroy(piece.gameObject);
                            y = Random.Range(2, 4);
						}
						else if (paths > 1) 
						{
							map.Add (piece.transform.position, piece);
						}
                    }
                    if (y == 3)
                    {
                        Transform piece = (Transform)Instantiate(turnRight);
                        piece.transform.position = new Vector3(x * 5, 0, z * 5);
                        int paths = deadEndChecks(piece, left, right, up, down);
                        if (paths == 0)
                        {
                            piece.Rotate(new Vector3(0, 90));
                            paths = junctionChecks(piece, left, right, up, down);
                            if (paths == 0)
                            {
                                piece.Rotate(new Vector3(0, 180));
                                paths = junctionChecks(piece, left, right, up, down);
                                if (paths == 0)
                                {
                                    piece.Rotate(new Vector3(0, 270));
                                    paths = junctionChecks(piece, left, right, up, down);
                                }
                            }
                        }
                        if (paths == 0)
                        {
                            //If there's nowhere to go any of the ways, abort
                            Destroy(piece.gameObject);
                            y = Random.Range(2, 5);
						}
						else if (paths > 1) 
						{
							map.Add (piece.transform.position, piece);
						}
                    }
                }
                
            }
        }
    }


    #region Junction Checks
    private int junctionChecks(Transform piece, Vector2 left, Vector2 right, Vector2 up, Vector2 down)
    {
        int paths = 0;
        if (map.ContainsKey(left) && piece.rotation.y != 90)
        {
            Transform totheLeft = map[left];
            if ((totheLeft.tag == "Corridor" && totheLeft.rotation.y == 0) ||
                (totheLeft.tag == "T Junction" && totheLeft.rotation.y != 270) ||
                (totheLeft.tag == "Turn Right" && (totheLeft.rotation.y == 90 || totheLeft.rotation.y == 180)))
            {
                paths++;
            }
        }
        if (map.ContainsKey(right) && piece.rotation.y != 270)
        {
            Transform totheLeft = map[right];
            if ((totheLeft.tag == "Corridor" && totheLeft.rotation.y == 0) ||
                (totheLeft.tag == "T Junction" && totheLeft.rotation.y != 90) ||
                (totheLeft.tag == "Turn Right" && (totheLeft.rotation.y == 0 || totheLeft.rotation.y == 270)))
            {
                paths++;
            }
        }
        if (map.ContainsKey(up) && piece.rotation.y != 180)
        {
            Transform totheLeft = map[up];
            if ((totheLeft.tag == "Corridor" && totheLeft.rotation.y == 90) ||
                (totheLeft.tag == "T Junction" && totheLeft.rotation.y != 180) ||
                (totheLeft.tag == "Turn Right" && (totheLeft.rotation.y == 0 || totheLeft.rotation.y == 90)))
            {
                paths++;
            }
        }
        if (map.ContainsKey(down) && piece.rotation.y == 0)
        {
            Transform totheLeft = map[down];
            if ((totheLeft.tag == "Corridor" && totheLeft.rotation.y == 90) ||
                (totheLeft.tag == "T Junction" && totheLeft.rotation.y != 0) ||
                (totheLeft.tag == "Turn Right" && (totheLeft.rotation.y == 180 || totheLeft.rotation.y == 270)))
            {
                paths++;
            }
        }
        return paths;
    }
    #endregion

    #region Corridor checks
    private int checkCorridorPath(int rotation, Vector2 left, Vector2 right, Vector2 up, Vector2 down)
    {
        int paths = 0;
        if (map.ContainsKey(left) && rotation == 0)
        {
            Transform totheLeft = map[left];
            if ((totheLeft.tag == "Corridor" && totheLeft.rotation.y == 0) ||
                (totheLeft.tag == "T Junction" && totheLeft.rotation.y != 270) ||
                (totheLeft.tag == "Dead End" && totheLeft.rotation.y == 0) ||
                (totheLeft.tag == "Turn Right" && (totheLeft.rotation.y == 90 || totheLeft.rotation.y == 180)))
            {
                paths++;
            }
        }
        if (map.ContainsKey(right) && rotation == 0)
        {
            Transform totheLeft = map[right];
            if ((totheLeft.tag == "Corridor" && totheLeft.rotation.y == 0) ||
                (totheLeft.tag == "T Junction" && totheLeft.rotation.y != 90) ||
                (totheLeft.tag == "Dead End" && totheLeft.rotation.y == 180) ||
                (totheLeft.tag == "Turn Right" && (totheLeft.rotation.y == 0 || totheLeft.rotation.y == 270)))
            {
                paths++;
            }
        }
        if (map.ContainsKey(up) && rotation == 90)
        {
            Transform totheLeft = map[up];
            if ((totheLeft.tag == "Corridor" && totheLeft.rotation.y == 0) ||
                (totheLeft.tag == "T Junction" && totheLeft.rotation.y != 270) ||
                (totheLeft.tag == "Dead End" && totheLeft.rotation.y == 0) ||
                (totheLeft.tag == "Turn Right" && (totheLeft.rotation.y == 90 || totheLeft.rotation.y == 180)))
            {
                paths++;
            }
        }
        if (map.ContainsKey(down) && rotation == 90)
        {
            Transform totheLeft = map[down];
            if ((totheLeft.tag == "Corridor" && totheLeft.rotation.y == 0) ||
                (totheLeft.tag == "T Junction" && totheLeft.rotation.y != 270) ||
                (totheLeft.tag == "Dead End" && totheLeft.rotation.y == 0) ||
                (totheLeft.tag == "Turn Right" && (totheLeft.rotation.y == 90 || totheLeft.rotation.y == 180)))
            {
                paths++;
            }
        }
        return paths;
    }
    #endregion

    #region Dead end Checks
    private int deadEndChecks(Transform piece, Vector2 left, Vector2 right, Vector2 up, Vector2 down)
    {
        int paths = 0;
        if (map.ContainsKey(left) && piece.rotation.y == 180)
        {
            Transform totheLeft = map[left];
            if ((totheLeft.tag == "Corridor" && totheLeft.rotation.y == 0) ||
                (totheLeft.tag == "T Junction" && totheLeft.rotation.y != 270) ||
                (totheLeft.tag == "Turn Right" && (totheLeft.rotation.y == 90 || totheLeft.rotation.y == 180)))
            {
                paths++;
            }
        }
        if (map.ContainsKey(right) && piece.rotation.y == 0)
        {
            Transform totheLeft = map[right];
            if ((totheLeft.tag == "Corridor" && totheLeft.rotation.y == 0) ||
                (totheLeft.tag == "T Junction" && totheLeft.rotation.y != 90) ||
                (totheLeft.tag == "Turn Right" && (totheLeft.rotation.y == 0 || totheLeft.rotation.y == 270)))
            {
                paths++;
            }
        }
        if (map.ContainsKey(up) && piece.rotation.y == 270)
        {
            Transform totheLeft = map[up];
            if ((totheLeft.tag == "Corridor" && totheLeft.rotation.y == 90) ||
                (totheLeft.tag == "T Junction" && totheLeft.rotation.y != 180) ||
                (totheLeft.tag == "Turn Right" && (totheLeft.rotation.y == 0 || totheLeft.rotation.y == 90)))
            {
                paths++;
            }
        }
        if (map.ContainsKey(down) && piece.rotation.y == 90)
        {
            Transform totheLeft = map[down];
            if ((totheLeft.tag == "Corridor" && totheLeft.rotation.y == 90) ||
                (totheLeft.tag == "T Junction" && totheLeft.rotation.y != 0) ||
                (totheLeft.tag == "Turn Right" && (totheLeft.rotation.y == 180 || totheLeft.rotation.y == 270)))
            {
                paths++;
            }
        }
        return paths;
    }
    #endregion
}
