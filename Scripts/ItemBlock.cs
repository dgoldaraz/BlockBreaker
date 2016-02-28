using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemBlock : MonoBehaviour {

	public enum ItemType{TwoBalls, SpeedIncrease, StopBall, SpeedDecrease, RandomItem};
	
	public float speed = 10.0f;
	public ItemType type; 
	
	private ItemType auxType;
	public GameObject newBallObj;

	// Use this for initialization
	void Start () {
		type = (ItemType)Random.Range (0,System.Enum.GetValues(typeof(ItemType)).Length);
		if(type != ItemType.RandomItem)
		{
			GetComponent<SpriteRenderer>().color = getColorFromType(type);
		}
		else
		{
			auxType = (ItemType)Random.Range (0,System.Enum.GetValues(typeof(ItemType)).Length - 1);
			GetComponent<SpriteRenderer>().color = getColorFromType(auxType);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = this.transform.position;
		newPos.y =  newPos.y - (speed * Time.deltaTime);
		this.transform.position = newPos;
		
		if(type == ItemType.RandomItem)
		{
			auxType = (ItemType)Random.Range (0,System.Enum.GetValues(typeof(ItemType)).Length - 1);
			GetComponent<SpriteRenderer>().color = getColorFromType(auxType);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Paddle")
		{
			if(type == ItemType.RandomItem)
			{
				type = auxType;
			}
			Paddle paddleObject = collision.gameObject.GetComponent<Paddle>();
			paddleObject.changeColour(getColorFromType(type), 3);
			
			CreateItemEffect();
			Destroy (this.gameObject);
			Text[] txt = GameObject.FindObjectsOfType<Text>() as Text[];
			foreach( Text t in txt)
			{
				if(t.gameObject.name == "ItemText")
				{
					t.text = type.ToString();
				}
			}
		}
	}
	
	void CreateItemEffect()
	{
		switch(type)
		{
		case ItemType.TwoBalls:
			TwoBalls();
			break;
		case ItemType.SpeedDecrease:
			SpeedDecrease();
			break;
		case ItemType.SpeedIncrease:
			SpeedIncrease();
			break;
		case ItemType.StopBall:
			StopBall();
			break;
		default:
			return;
		}
	}

	void TwoBalls()
	{
		GameObject newBall = GameObject.Instantiate(newBallObj,gameObject.transform.position, Quaternion.identity) as GameObject;
		Ball ballObject = newBall.GetComponent<Ball>();
		ballObject.setHasStarted(true);
		newBall.rigidbody2D.velocity = new Vector2(2f, 10f);
	}
	
	void SpeedDecrease()
	{
		Ball[] objs = GameObject.FindObjectsOfType<Ball>() as Ball[];
		foreach(Ball ballObject in objs)
		{
			ballObject.IncreaseVelocity(3);
		}
	}
	
	void SpeedIncrease()
	{
		Ball[] objs = GameObject.FindObjectsOfType<Ball>() as Ball[];
		foreach(Ball ballObject in objs)
		{
			ballObject.DecreaseVelocity(3);
		}
	}
	
	void StopBall()
	{
		Paddle paddle = GameObject.FindObjectOfType<Paddle>();
		paddle.setSticky();
	}
	
	public Color getColorFromType(ItemType type)
	{
		switch(type)
		{
		case ItemType.TwoBalls:
			return Color.Lerp(Color.blue,Color.green, 0.4f);
		case ItemType.SpeedDecrease:
			return Color.Lerp(Color.gray,Color.white, 0.3f);
		case ItemType.SpeedIncrease:
			return Color.Lerp(Color.magenta,Color.red, 0.5f);
		case ItemType.StopBall:
			return Color.Lerp(Color.green,Color.yellow, 0.7f);
		default:
			return Color.white;
		}
	}
}
