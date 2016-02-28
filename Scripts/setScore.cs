using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class setScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		Manager m = GameObject.FindObjectOfType<Manager>();
		Text t = gameObject.GetComponent<Text>();
		if(m)
		{
			int points = m.getScore();
			t.text = "Score: " + points.ToString();
		}
		else
		{
			t.text = "";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
