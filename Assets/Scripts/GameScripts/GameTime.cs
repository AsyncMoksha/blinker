using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
	
	public GUIText time;
	public int maxtime = 60;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int time_remaining = maxtime - (int)UnityEngine.Time.time;	
		time.text = time_remaining.ToString();
	}
	
	void OnGUI()
	{
		
	}
}
