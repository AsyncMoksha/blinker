using UnityEngine;
using System.Collections;

public enum GameState {playing, Team1Win, Team2Win };



public class ScoreScript : Photon.MonoBehaviour {
	
	public int Team1Point;
	public int Team2Point;
	public GameObject Flag1;
	public GameObject Flag2;
	
	public bool Team1FlagInBase;
	public bool Team2FlagInBase;
	public GameState gameState;
	
	public GUIText Score;
	public GUIText GameResult;
	//public GUITexture progressBarEmpty;
	//public GUITexture progressBarFull;
	
	public float portalProgress = 0;
	public Texture2D portalProgressBarEmpty;
 	public Texture2D portalProgressBarFull;
	Rect rect1 = new Rect(20, 10, 60, 20);
	Rect rect2 = new Rect(20, 10, 60, 20);
	bool isPortalReloading = false;
	bool teamSelected = false;
	
	public float hookProgress = 0;
	public Texture2D hookProgressBarEmpty;
	public Texture2D hookProgressBarFull;
	Rect hookRect1 = new Rect(20, 40, 60, 20);
	Rect hookRect2 = new Rect(20, 40, 60, 20);
	bool isHookReloading = false;
	
	
	// Use this for initialization
	void Start () {
	
		Team1Point=0;
		Team2Point=0;
		Team1FlagInBase = true;
		Team2FlagInBase = true;
		
		gameState = GameState.playing;
		
		photonView.RPC("setScoreSync",PhotonTargets.All);
		
		
		
	}
	
	// Update is called once per frame
	void Update () {		
		checkWin();
		Score.text = Team1Point + " : " + Team2Point;
		
		if(isPortalReloading){
			portalProgress +=  0.01f;
		} 
		
		if(isHookReloading) {
			hookProgress += 0.01f;
		}
		
	}
	
	public void resetPortalReloadBar() {
		//GUI.DrawTexture(rect1,  progressBarEmpty);
		rect2 = new Rect(20, 10, 0, 20); 
		portalProgress = 0.0f;
		//Destroy(rect2);
		//isPortalReloading = false;
	}
	
	public void resetHookReloadBar() {
		hookRect2 = new Rect(20, 40, 0, 20);
		hookProgress = 0.0f;
	}
	

	void checkWin(){
		
		if(Team1Point >= 3){
			gameState = GameState.Team1Win;
			
			
		}else if(Team2Point >=3){
			gameState = GameState.Team2Win;
		}
	}
	
	public void runPortalReloadBar() {
		isPortalReloading = true;
	}
	
	public void runHookReloadBar() {
		isHookReloading = true;
	}
	
	
	
	
	public void GetPoint1(){
		photonView.RPC("getPoint1",PhotonTargets.AllBuffered);
		print("GetPoint");
	}
	
	
	public void GetPoint2(){
		
		photonView.RPC("getPoint2",PhotonTargets.AllBuffered);
		print("GetPoint");

	}
	
	
	
	[RPC]
	void getPoint1(){
		Team1Point += 1;
		Flag2.GetComponent<FlagScript2>().resetFlag();
	}
	
	[RPC]
	void getPoint2(){
		Team2Point += 1;
		Flag1.GetComponent<FlagScript1>().resetFlag();
	}
	
	
	[RPC]
	void setScoreSync()
	{
		if(photonView.isMine)
		{
			photonView.RPC("setScoreSyncRPC", PhotonTargets.All,Team1Point,Team2Point,Team1FlagInBase,Team2FlagInBase);
		}
	}
	
	[RPC]
	void setScoreSyncRPC(int team1Point,int team2Point,bool team1InBase, bool team2InBase)
	{
		
		Team1Point = team1Point;
		Team2Point = team2Point;
		team1InBase = Team1FlagInBase;
		team2InBase = Team2FlagInBase;
	}
	
	public void setTeamSelected() {
		teamSelected = true;
	}
	
	void OnGUI(){
		
		
		if(teamSelected){ 
			//Portal Reload Bar
			GUI.DrawTexture(rect1,  portalProgressBarFull);
			
			if(isPortalReloading){
				GUI.DrawTexture(rect1,  portalProgressBarEmpty);
				rect2 = new Rect(20, 10, 60 * Mathf.Clamp01(portalProgress), 20); 
		     	GUI.DrawTexture(rect2,  portalProgressBarFull);
			} 
			
			//Hook Reload Bar
			GUI.DrawTexture(hookRect1,  hookProgressBarEmpty);
			
			if(isHookReloading){
				hookRect2 = new Rect(20, 40, 60 * Mathf.Clamp01(hookProgress), 20); 
		     	GUI.DrawTexture(hookRect2,  hookProgressBarFull);
			} 
		}
		//Rect(pos.x, pos.y, size.x * , size.y)
		
		if(gameState == GameState.Team1Win){
		
			
			GameResult.gameObject.active = true;
			GameResult.text = "Team1 Win!";
            if(GUILayout.Button("Restart") ){
                Application.LoadLevel(Application.loadedLevel);
            }
			
		}
		if(gameState == GameState.Team2Win){
			
			
			GameResult.gameObject.active = true;
			GameResult.text = "Team2 Win!";
			
	
            if(GUILayout.Button("Restart") ){
                Application.LoadLevel(Application.loadedLevel);
            }
		}
		
		
	}
	
	
	public void setTeam1FlagInBase(bool a)
	{
		photonView.RPC("setTeam1FlagInBaseRPC",PhotonTargets.All,a);
	}
	
	
	
	
	[RPC] void setTeam1FlagInBaseRPC(bool a)
	{
		Team1FlagInBase = a;
	}
	
	
	
		public void setTeam2FlagInBase(bool a)
	{
		photonView.RPC("setTeam2FlagInBaseRPC",PhotonTargets.All,a);
	}
	
	
	
	[RPC] void setTeam2FlagInBaseRPC(bool a)
	{
		Team2FlagInBase = a;
	}
	
	

	
}
