using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	private static Manager mpInstance = null;
	private static int lastLevel = 1;
	private static int inputEntry = 0;
	private static int points = 0;
	public static bool autoPlay = false;
	// Use this for initialization
	
	void Awake()
	{
		if( mpInstance != null)
		{
			Destroy (gameObject);
		}
		else
		{
			mpInstance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel(Application.loadedLevel);
		}
		else if(Input.GetKeyDown(KeyCode.T))
		{
			autoPlay = !autoPlay;
		}
	}
	
	public int getLastLevel()
	{
		return lastLevel;
	}
	
	public void setLastLevel(int level)
	{
		lastLevel = level;
	}
	
	public int getInputEntry()
	{
		return inputEntry;
	}
	
	public void setInputEntry(int input)
	{
		inputEntry = input;
	}
	
	public void addPoints(int newPoints)
	{
		points += newPoints;
		Text[] txt = GameObject.FindObjectsOfType<Text>() as Text[];
		foreach( Text t in txt)
		{
			if(t.gameObject.name == "PointsText")
			{
				t.text = points.ToString();
			}
		}
	}
	
	public int getScore()
	{
		return points;
	}
}
