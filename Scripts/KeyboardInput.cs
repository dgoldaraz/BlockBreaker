using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyboardInput : MonoBehaviour {

	public enum InputEntry{Keyboard, Mouse};
	private InputEntry currentInput;
	
	// Use this for initialization
	void Start () 
	{
		currentInput = InputEntry.Mouse;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.K))
		{
			SwitchInput();
		}
	
	}
	
	void SwitchInput()
	{
		if(currentInput == InputEntry.Mouse)
		{
			gameObject.GetComponent<Text>().text = "Keyboard";
			Manager ply = GameObject.FindObjectOfType<Manager>();
			if(ply)
			{
				ply.setInputEntry(1);
			}
			currentInput = InputEntry.Keyboard;
		}
		else
		{
			gameObject.GetComponent<Text>().text = "Mouse";
			Manager ply = GameObject.FindObjectOfType<Manager>();
			if(ply)
			{
				ply.setInputEntry(0);
			}
			currentInput = InputEntry.Mouse;
		}
	}
	
}
