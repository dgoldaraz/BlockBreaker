using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	private Manager mPlayer;
	
	void Start()
	{
		mPlayer = GameObject.FindObjectOfType<Manager>();
	}	
	
	public void LoadLevel(string name)
	{
		if(mPlayer)
		{
			mPlayer.setLastLevel( Application.loadedLevel);
		}
		Debug.Log ("Load this level: " + name);
		Application.LoadLevel(name);
	}
	
	public void QuitRequest()
	{
		Debug.Log ("Quit Game Requested");
		Application.Quit ();
	}
	
	public void LoadNextLevel()
	{
		if(mPlayer)
		{
			mPlayer.setLastLevel( Application.loadedLevel);
		}
		print ( Application.loadedLevel);
		Application.LoadLevel(Application.loadedLevel + 1);
	}
	
	public void BrickDestroyed()
	{
		if(Brick.breakableCount <= 0)
		{
			LoadNextLevel();
		}
	}
	
	public void LoadLastLevel()
	{
		if(mPlayer)
		{
			int lastLevel = mPlayer.getLastLevel();
			print (lastLevel);
			Application.LoadLevel(lastLevel);
		}
	}
}
