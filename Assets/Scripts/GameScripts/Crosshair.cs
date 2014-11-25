using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {
	
	public Texture2D crosshair;
	public Rect position;
	
	// Use this for initialization
	void Start () {
		position = new Rect(
		                (Screen.width - crosshair.width) / 2,
		                (Screen.height - crosshair.height) / 2,
		                crosshair.width,
		                crosshair.height);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// Draws the crosshair
	void OnGUI()
	{
		GUI.DrawTexture(position, crosshair);
	}
}
