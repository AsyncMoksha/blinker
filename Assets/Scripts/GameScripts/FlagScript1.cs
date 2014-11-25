using UnityEngine;
using System.Collections;

public class FlagScript1 : Photon.MonoBehaviour {
	
	public GameObject GM;
	GameObject OnPlayer;
	public bool isOnPlayer;
	public bool isInBase;
	public bool isOnGround;
	public Vector3 correctFlagPos = new Vector3(131.3083F,2.554122F,80.71619F);
	public Quaternion correctFlagRot  = Quaternion.identity;
	private Vector3 OriginPosition;
	public AudioClip flagSFX;
	public AudioClip scoreSFX;
	
	
	// Use this for initialization
	void Start () {
	
		isOnPlayer=false;	
		isInBase = true;
		isOnGround = false;	
		OriginPosition = transform.position;
		
		photonView.RPC("findOwner",PhotonTargets.All);
	}
	
	// Update is called once per frame
	void Update () {
		
		if(photonView.isMine)
		{
			if(isOnPlayer){
			
				transform.position = OnPlayer.transform.Find("FlagPosition").position;
				//print("x:"+ OnPlayer.transform.position.x+ " y:" + OnPlayer.transform.position.y+ " z:" + OnPlayer.transform.position.z);
			}
		}else{
			transform.position = Vector3.Lerp(transform.position,correctFlagPos,Time.deltaTime * 5);
			transform.rotation = Quaternion.Lerp(transform.rotation, correctFlagRot, Time.deltaTime*5);
		}
	
	}
	
	public void resetFlag(){
		if(photonView.isMine)
		{
		if(isOnPlayer){
			OnPlayer.GetComponent<PlayerScript>().setHasFlag(false);
			isOnPlayer = false;
		}
		
		if(isOnGround)
		{
		isOnGround = false;
		}
		
		isInBase = true;
		GM.GetComponent<ScoreScript>().setTeam1FlagInBase(true);
		transform.position = OriginPosition;
		AudioSource.PlayClipAtPoint(scoreSFX,OnPlayer.transform.position,1.0f);
		}
	}
	
	
	
	
		void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
	{
		if(stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);	
		}
		else
		{
			correctFlagPos = (Vector3)stream.ReceiveNext();
			correctFlagRot = (Quaternion)stream.ReceiveNext();
		}
	}
	

	
	void OnTriggerEnter(Collider other){
	
		if(other.tag == "team1Player" || other.tag == "team2Player")
		{
			print("Player Enter");
		}
		if(photonView.isMine){
		if(other.tag == "Team2Player" && !isOnPlayer)
		{
		//Destroy(transform.parent.gameObject);
			
			print("Flag");
				
			//Play Sound
			AudioSource.PlayClipAtPoint(flagSFX, transform.position);
			
			OnPlayer = other.gameObject;
			GM.GetComponent<ScoreScript>().setTeam1FlagInBase(false);
			isOnPlayer = true;
			other.GetComponent<PlayerScript>().setHasFlag(true);
			isInBase = false;
		}else if(other.tag == "Team1Player"&&!isInBase)
		{
			resetFlag();

		}
	
		}
	}
	
	[RPC]
	void setOnPlayer(bool a)
	{
		isOnPlayer = a;
	}
	
	[RPC]
	void setInBase(bool a)
	{
		isInBase = a;
	}
	
	[RPC]
	void setOnGround(bool a)
	{
		isOnGround = a;
	}
	
	[RPC]
	void findOwner()
	{
		if(photonView.isMine)
		{
		photonView.RPC("setPosSync",PhotonTargets.All,transform.position,isOnPlayer,isInBase,isOnGround);
		}
	}
	
	
	[RPC]
	void setPosSync(Vector3 pos,bool onplayer, bool inbase, bool onground)
	{
		transform.position = pos;
		isOnPlayer = onplayer;
		isInBase = inbase;
		isOnGround = onground;
	}
	
	
	
}