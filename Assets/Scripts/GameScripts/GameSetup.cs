

using UnityEngine;
using System.Collections;

public class GameSetup : Photon.MonoBehaviour {
	
	public bool ChosenSide;
	int TeamNum;
	public Transform player1Pref;
	public Transform player2Pref;
	public bool isPaused;
	public UIBistateInteractivePanel InGameMenu;
	public UIBistateInteractivePanel ChooseTeam;
	public AudioClip bgm;
	public ScoreScript scoreScript;
	
	void Awake()
	{
		scoreScript = GameObject.Find("GM").GetComponent<ScoreScript>();

		ChosenSide = false;
		isPaused = false;
		
		if(!PhotonNetwork.connected)
		{
			
			
			//Application.LoadLevel("MainMenu");
			PhotonNetwork.offlineMode = true;
			
			return;
			
			
		}
		
		PhotonNetwork.isMessageQueueRunning = true;
				

			
	}
	
	
	// Use this for initialization
	void Start () {

		//ChooseTeam.Reveal();

	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(ChosenSide)
		{
			
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			if(!isPaused)
			{
				InGameMenu.Reveal();
				Screen.lockCursor = false;
				
			
			}else
			{
				InGameMenu.Hide();
				Screen.lockCursor = true;
			}
			
				isPaused = !isPaused;
		}	
			
		}
		
		
		
	}
	

	
	void resumeGame()
	{
		InGameMenu.Hide();
		Screen.lockCursor = true;
		isPaused = false;
	}
		
	void quitRoom()
	{
		if(PhotonNetwork.offlineMode == false)
		{
        PhotonNetwork.LeaveRoom();
		}
        Application.LoadLevel("MainMenu");
	}
	
	
	
	
	
	void Team1Selected()
	{
		scoreScript.setTeamSelected();
		ChooseTeam.Hide();
		ChosenSide = true;
		TeamNum = 1;
		setupScene();
		/*
		audio.loop = true;
		AudioSource.PlayClipAtPoint(bgm, transform.position);
		*/
		audio.Play();
		
		
		
	}
	
	
	void Team2Selected()
	{
		scoreScript.setTeamSelected();
		ChooseTeam.Hide();
		ChosenSide = true;
		TeamNum = 2;
		setupScene();
		audio.Play();
		
		
	}
	
	void ReSelectTeam()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
	
	
	
	
	
	
	
	
	
	
	void setupScene()
	{
		if(TeamNum == 1)
		{
			Transform spawnpoint = GameObject.FindGameObjectWithTag("Team1Spawn").transform;
			PhotonNetwork.Instantiate(player1Pref,spawnpoint.position,spawnpoint.rotation,0);
			
			Screen.lockCursor =true;	
		
			
			
			
		foreach(GameObject a in GameObject.FindGameObjectsWithTag("EnterPortal"))
		{
			Destroy(a);
		}
		
		foreach(GameObject a in GameObject.FindGameObjectsWithTag("ExitPortal"))
		{
			Destroy(a);
		}
			
			
			
		}else if(TeamNum == 2)
		{
			
			
			Transform spawnpoint = GameObject.FindGameObjectWithTag("Team2Spawn").transform;
			PhotonNetwork.Instantiate(player2Pref,spawnpoint.position,spawnpoint.rotation,0);
			Screen.lockCursor =true;	
		
			
			
			
			
			
					foreach(GameObject a in GameObject.FindGameObjectsWithTag("EnterPortal"))
		{
			Destroy(a);
		}
		
		foreach(GameObject a in GameObject.FindGameObjectsWithTag("ExitPortal"))
		{
			Destroy(a);
		}	
			
			
			
			
			
		}
		
		GameObject.Find("GUI_Control").GetComponent<Crosshair>().enabled = true;
		
		
	}
	
	
	
	
	
	
}

	
	
	
	
	