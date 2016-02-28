using UnityEngine;
using System.Collections;

public class BrickMovement : MonoBehaviour
{

	public Vector2 offset;
	public bool YX;
	public float velocity = 1;
	public bool move = true;
	public float xIncrement = 0.5f;
	public float yIncrement = 0.38f;
	private bool updateX;
	public bool reverse = false;
	private Vector2 currentOffset;
	// Use this for initialization
	void Start ()
	{
		currentOffset = new Vector2 (0f, 0f);
		updateX = !YX;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (move) {
			MoveBrick ();
		}
	}
	
	void MoveBrick ()
	{
		
		if (updateX) {
			MoveInX ();
		} else {
			MoveInY ();
		}
		
	}
	
	void MoveInX ()
	{
		//move in X
		if (currentOffset.x >= offset.x) 
		{
			updateX = false;
			currentOffset.x = 0.0f;
			if (YX)
			{
				reverse = !reverse;
			}
		}
		else 
		{
			Vector3 cPosition = this.transform.position;
			float increment = (xIncrement / velocity);
			currentOffset.x = (currentOffset.x + increment);
			if (reverse) 
			{
				cPosition.x = cPosition.x - increment;
			}
			else 
			{
				cPosition.x = cPosition.x + increment;
			}
			this.transform.position = cPosition;
		}	
	}
	
	void MoveInY ()
	{
		//move Y
		if (currentOffset.y >= offset.y) 
		{
			updateX = true;
			currentOffset.y = 0;
			if (!YX) 
			{
				reverse = !reverse;
			}
		} 
		else 
		{
			Vector3 cPosition = this.transform.position;
			float increment = (yIncrement / velocity);
			currentOffset.y = currentOffset.y + increment;
			if (reverse) 
			{
				cPosition.y = cPosition.y - increment;
			}
			else 
			{
				cPosition.y = cPosition.y + increment;
			}
				
			this.transform.position = cPosition;
		}
	}
}
