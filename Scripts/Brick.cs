using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	
	public Sprite[] hitSprites;
	public static int breakableCount = 0;
	public AudioClip crackSound;
	public GameObject smoke;
	
	private bool isBreakable;
    private int timesHits;
	private LevelManager lvlMng;
	public GameObject itemBlock;
    
	// Use this for initialization
	void Start () {
		timesHits = 0;
		lvlMng = GameObject.FindObjectOfType<LevelManager>();
		isBreakable = this.tag == "Breakable";
		if(isBreakable)
		{
			breakableCount++;
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(isBreakable && collision.gameObject.tag != "Item")
		{
			AudioSource.PlayClipAtPoint(crackSound, transform.position,0.25f);
			HandleHits();
		}
	}
	
	
	void HandleHits()
	{
		timesHits++;
		int maxHits = hitSprites.Length + 1;
		if(timesHits >= maxHits)
		{
			breakableCount--;
			lvlMng.BrickDestroyed();
			PuffSmoke();

			if(breakableCount > 1)
			{
				int randomNumber = Random.Range(0,100);
				if(randomNumber < 25)
				{
					//Make a 25% possible that an item block appears
					Instantiate(itemBlock, this.transform.position, Quaternion.identity);
				}
			}

			Destroy (gameObject);
			Manager mp = GameObject.FindObjectOfType<Manager>();
			if(mp)
			{
				mp.addPoints(100 * maxHits);
			}
			
		}
		else
		{
			LoadSprites();
		}
	}
	
	void PuffSmoke()
	{
		GameObject smokePuf = GameObject.Instantiate(smoke,gameObject.transform.position, Quaternion.identity) as GameObject;
		smokePuf.particleSystem.startColor = this.GetComponent<SpriteRenderer>().color;
	}
	
	void LoadSprites()
	{
		int spriteIndex = timesHits - 1;
		if(hitSprites[spriteIndex])
		{
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
		else
		{
			Debug.LogError("No Sprite for Brick");
		}
	}
}
