using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Paddle paddle;
	private bool hasStarted = false;
	private Vector3 paddleToBall;
	
	private float entryTime;
	private bool isChanged = false;
	public float timeOfChange = 3f;
	private float multiplier = 1.0f;

	private float fps = 0.0f;
	private Vector3 lastPosition;
	private float timesInPos = 0.0f;
	
	// Use this for initialization
	void Start () {
		paddle = GameObject.FindObjectOfType<Paddle>();
		paddleToBall = this.transform.position - paddle.transform.position;
		lastPosition = this.transform.position;
	}
	// Update is called once per frame
	void Update () {

		if(fps == 0)
		{
			fps = 1.0f/ Time.deltaTime;
			print (fps);
		}
		if(!hasStarted)
		{
			this.transform.position = paddle.transform.position + paddleToBall;
			if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
			{
				hasStarted = true;
				this.rigidbody2D.velocity = new Vector2(2f, 10f);
			}
		}
		else
		{
			if( lastPosition == this.transform.position)
			{
				if(timesInPos > (fps*5.0f))
				{
					//Something happend and the ball it's stuck, try to get out!
					Debug.Log("Stuck Ball");
					Vector3 offset = new Vector3(0.2f, 0.2f, 0.0f);
					this.transform.position = lastPosition + offset;
					this.rigidbody2D.velocity = new Vector2(2f, 10f);
					timesInPos = 0.0f;
				}
				else
				{
					timesInPos++;
				}
			}
			else
			{
				lastPosition = this.transform.position;
			}
		}

		if(isChanged)
		{
			if((Time.time - entryTime )> timeOfChange)
			{
				isChanged = false;
				if(multiplier > 1.0f)
				{
					multiplier = 1.0f/multiplier;
				}
				else
				{
					multiplier = 2.0f;
				}
				Vector2 curVel = this.rigidbody2D.velocity;
				curVel.x *= multiplier;
				curVel.y *= multiplier;
				this.rigidbody2D.velocity = curVel;
				multiplier = 1.0f;
			}
		}
	}
	
	public void AutoStart()
	{
		if(!hasStarted)
		{
			hasStarted = true;
			this.rigidbody2D.velocity = new Vector2(2f, 10f);
		}
	}
	
	
	public void setHasStarted(bool start)
	{
		hasStarted = start;
	}
	
	public bool getHasStarted()
	{
		return hasStarted;
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		Vector2 tweakVector = new Vector2(Random.Range (0f, 0.2f), Random.Range(0f, 0.2f));
		if(hasStarted)
		{
			audio.Play();
			rigidbody2D.velocity += tweakVector;
		}
	}
	
	//Increase the ball velocity
	public void IncreaseVelocity(float changeTime)
	{
		if(!isChanged)
		{
			timeOfChange = changeTime;
			Vector2 curVel = this.rigidbody2D.velocity;
			multiplier = 0.5f;
			curVel.x *= multiplier;
			curVel.y *= multiplier;
			this.rigidbody2D.velocity = curVel;
			isChanged = true;
			entryTime = Time.time;
		}
	}
	
	//Decrease the ball velocity
	public void DecreaseVelocity(float changeTime)
	{
		if(!isChanged)
		{
			timeOfChange = changeTime;
			Vector2 curVel = this.rigidbody2D.velocity;
			multiplier = 1.5f;
			curVel.x *= multiplier;
			curVel.y *= multiplier;
			this.rigidbody2D.velocity = curVel;
			isChanged = true;
			entryTime = Time.time;
		}
	}
}
