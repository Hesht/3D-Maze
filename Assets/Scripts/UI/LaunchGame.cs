using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class LaunchGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void launchGame()
	{
		SceneManager.LoadScene ("Maze");
	}

	public void setWidth(string width)
	{
		int w = 0;
		int.TryParse (width, out w);
		if (w > 0)
		{
			PlayerPrefs.SetInt ("Width", Convert.ToInt32 (width));
		}
	}
	public void setLength(string length)
	{
		int l = 0;
		int.TryParse (length, out l);
		if (l > 0)
		{
			PlayerPrefs.SetInt ("Length", Convert.ToInt32 (length));
		}
	}
}
