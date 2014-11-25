using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public GUIText health;
	
	// Use this for initialization
	void Start () {
		health.material.color = new Color(200.0f,0,0,1.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Draws the Ammo Count depending on the Weapon
	void OnGUI()
	{
		health.text = "HP: " + "100";
	}
}
