using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour {

	private LevelManager lvlMngr;
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		int numberOfBalls = GameObject.FindObjectsOfType<Ball>().Length;
		if(numberOfBalls - 1 == 0)
		{
			Brick.breakableCount = 0;
			lvlMngr.LoadLevel("Lose");
			Manager mp = GameObject.FindObjectOfType<Manager>();
			if(mp)
			{
				mp.addPoints(-500);
			}
		}
		else
		{
			Destroy (collider.gameObject);
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		
	}
	
	void Start()
	{
		lvlMngr = GameObject.FindObjectOfType<LevelManager>();
	}
}
