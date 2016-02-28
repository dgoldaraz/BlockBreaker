using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Paddle : MonoBehaviour {
	// Use this for initialization
	public bool autoplay = false;
	public bool useKeyboard = false;
	
	private bool sticky = false;
	private Ball ball;
	
	private Color startColor;
	private float entryTime;
	private bool colorChange = false;
	
	public float maxTime = 3f;
	
	void Start () 
	{
		ball = GameObject.FindObjectOfType<Ball>();
		Manager mp = GameObject.FindObjectOfType<Manager>();
		if(mp)
		{
			useKeyboard = mp.getInputEntry() == 1;
			//This call shows the points
			mp.addPoints(0);
			
			if(Manager.autoPlay)
			{
				autoplay =true;
			}
		}
		
		startColor = this.gameObject.GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!autoplay)
		{
			if(!useKeyboard)
			{
				MoveByMouse();
			}
			else
			{
				MoveByKeyboard();
			}
		}
		else
		{
			if(!ball.getHasStarted())
			{
				ball.AutoStart();
			}
			AutoPlay();
		}
		
		if(colorChange)
		{
			if((Time.time - entryTime) > maxTime && !sticky)
			{
				this.gameObject.GetComponent<SpriteRenderer>().color = startColor;
				colorChange = false;
				Text[] txt = GameObject.FindObjectsOfType<Text>() as Text[];
				foreach( Text t in txt)
				{
					if(t.gameObject.name == "ItemText")
					{
						t.text = "";
					}
				}
				
			}
		}
	}
	
	void MoveByMouse()
	{
		Vector3 paddlePos = this.transform.position;
		float mouseXBlocksPos = Mathf.Clamp((Input.mousePosition.x / Screen.width *16),1.0f, 15.0f);
		paddlePos.x = mouseXBlocksPos;
		
		this.transform.position = paddlePos;
	}
	
	void MoveByKeyboard()
	{
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) )
		{
			Vector3 paddlePos = this.transform.position;
			float mouseXBlocksPos = Mathf.Clamp((paddlePos.x + 0.3f),1.0f, 15.0f);
			paddlePos.x = mouseXBlocksPos;
			this.transform.position = paddlePos;
		}
		else if( Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			Vector3 paddlePos = this.transform.position;
			float mouseXBlocksPos = Mathf.Clamp((paddlePos.x - 0.3f),1.0f, 15.0f);
			paddlePos.x = mouseXBlocksPos;
			this.transform.position = paddlePos;
		}
	}
	
	void AutoPlay()
	{
		Vector3 currentPosition = this.transform.position;
		currentPosition.x = Mathf.Clamp(ball.transform.position.x,1.0f, 15.0f);
		this.transform.position = currentPosition;
	}
	
	void OnCollisionEnter2D(Collision2D collider)
	{
		if(collider.gameObject.tag == "Ball")
		{
			if(sticky)
			{
				Ball ball = collider.gameObject.GetComponent<Ball>();
				ball.setHasStarted(false);
				collider.gameObject.rigidbody2D.velocity = new Vector2(0f,0f);
				sticky = false;
			}
		}
	}
	
	public void changeColour(Color newColor, float time)
	{
		this.gameObject.GetComponent<SpriteRenderer>().color = newColor;
		colorChange = true;
		entryTime = Time.time;
		maxTime = time;
	}
	
	public void setSticky()
	{
		sticky = true;
	}
}
