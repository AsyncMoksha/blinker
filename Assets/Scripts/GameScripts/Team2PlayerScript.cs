using UnityEngine;
using System.Collections;




public class Team2PlayerScript : Photon.MonoBehaviour {
	
	public GameObject GM;
	public bool hasFlag;
	private Vector3 correctPlayerPos = Vector3.zero;
	private Quaternion correctPlayerRot = Quaternion.identity;	
	
	
	public AnimationClip angryIdle;
	public AnimationClip angryLeft;
	public AnimationClip angryRight;
	public AnimationClip angryForward;
	public AnimationClip angryBack;
	public AnimationClip angryJump;
		
	public float walkAnimationSpeed = 1.0f;
	public PlayerState _characterState;
	public Animation _animation;
	private Vector3 lastPos;
	public Vector3 velocity = Vector3.zero;
	
/*	
	void OnNetworkInstantiate (NetworkMessageInfo msg)
	{
		
		GM = GameObject.Find("GM").gameObject;
		
		if(!networkView.isMine)
		{
			transform.Find("Main Camera").gameObject.active = false;			
			
			
			transform.Find("Main Camera").gameObject.GetComponent<MouseLook>().enabled = false;

		//	transform.Find("MachineGun").gameObject.GetComponent<MachineGun>().enabled = false;
		//	transform.Find("PortalGun").gameObject.GetComponent<PortalLauncher>().enabled = false;
		//	transform.Find("RocketLauncher").gameObject.GetComponent<RocketLauncher>().enabled = false;

			
			transform.GetComponent<CharacterController>().enabled = false;
			transform.GetComponent<MouseLook>().enabled = false;
			transform.GetComponent<CharacterMotor>().enabled = false;
			transform.GetComponent<FPSInputController>().enabled = false;
			//transform.GetComponent<>().enabled = false;
			//transform.GetComponent<PortalLauncher>().enabled = false;
			
			

		}
		
		
		
		
		
	}
	
	
	*/
	
	// Use this for initialization
	void Start () {
		
		
		GM = GameObject.Find("GM").gameObject;

		_animation = transform.FindChild("Player").GetComponent<Animation>();
		
		
		photonView.RPC("setDataSync",PhotonTargets.All);	
	
		if(!PhotonNetwork.offlineMode)
		{
		if(photonView.isMine){
		setHasFlag(false);
			
			
		}else
		{			

			
			transform.FindChild("Main Camera").gameObject.active = false;
			transform.FindChild("Main Camera").GetComponent<MouseLook>().canControl = false;
			transform.FindChild("Main Camera").GetComponent<PortalLauncher>().enabled = false;
			transform.FindChild("Main Camera").GetComponent<AudioListener>().enabled = false;	
			transform.FindChild("Main Camera").GetComponent<GUILayer>().enabled = false;	
			transform.FindChild("Main Camera").GetComponent<Camera>().enabled = false;
			transform.GetComponent<MouseLook>().canControl = false;
			transform.GetComponent<CharacterMotor>().canControl = false;
			transform.GetComponent<FPSInputController>().canControl = false;
			

		}
		}else{
			setHasFlag(false);
		}
		gameObject.name = gameObject.name + photonView.viewID.ID;
	}
	
	
	void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
	{
		if(stream.isWriting)
		{
			stream.SendNext((int)_characterState);
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);	
		}
		else
		{
			_characterState = (PlayerState)(int)stream.ReceiveNext();
			correctPlayerPos = (Vector3)stream.ReceiveNext();
			correctPlayerRot = (Quaternion)stream.ReceiveNext();
		}
	}
	

	
	
	// Update is called once per frame
	void Update () {
		
		
		
		
		
		if(GM.GetComponent<GameSetup>().isPaused)
		{
			
			transform.GetComponent<MouseLook>().enabled = false;
			transform.FindChild("Main Camera").GetComponent<MouseLook>().enabled = false;
			transform.FindChild("Main Camera").GetComponent<PortalLauncher>().enabled = false;
			transform.GetComponent<CharacterMotor>().enabled = false;
			transform.GetComponent<FPSInputController>().enabled = false;
		
		}else{
			
			transform.GetComponent<MouseLook>().enabled = true;
			transform.GetComponent<CharacterMotor>().enabled = true;
			transform.FindChild("Main Camera").GetComponent<MouseLook>().enabled = true;
			transform.FindChild("Main Camera").GetComponent<PortalLauncher>().enabled = true;
			transform.GetComponent<FPSInputController>().enabled = true;
		
		}
		

		
		
	
		if(photonView.isMine)
		{
			
			velocity = (transform.position - lastPos)*25;

			
			
			if(velocity.sqrMagnitude < 0.001f)
			{
				_characterState = PlayerState.Idle;
				
			}else{
			
			
			if(Input.GetKey(KeyCode.A))
			{
				_characterState = PlayerState.Left;
			}else if(Input.GetKey(KeyCode.W))
			{
				_characterState = PlayerState.Forward;
			}else if(Input.GetKey(KeyCode.D))
			{
				_characterState = PlayerState.Right;
			}else if(Input.GetKey(KeyCode.S))
			{
				_characterState = PlayerState.Backward;
			}else if(Input.GetKey(KeyCode.Space))
			{
				_characterState = PlayerState.Jumping;
			}
		
			}
			
	
			
			lastPos = transform.position;
			
		
		}
		
		if(!photonView.isMine)
		{
			transform.position = Vector3.Lerp(transform.position,correctPlayerPos,Time.deltaTime * 20);
			transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime*20);
		}
		
		if(_animation)
		{
			if(_characterState == PlayerState.Backward)
			{
				_animation.CrossFade(angryBack.name);
			}else if(_characterState == PlayerState.Left)
			{
				_animation.CrossFade(angryLeft.name);
			}else if(_characterState == PlayerState.Right)
			{
				_animation.CrossFade(angryRight.name);
			}else if(_characterState == PlayerState.Forward)
			{
				_animation.CrossFade(angryForward.name);
			}else if(_characterState == PlayerState.Idle)
			{
				_animation.CrossFade(angryIdle.name);
			}else if(_characterState == PlayerState.Jumping)
			{
				_animation.CrossFade(angryJump.name);
			}
		}
		
	}
	
	void OnTriggerEnter(Collider other){
	

		if(photonView.isMine){
					print("hit someting!!!!!!!!!!!");
			
			
		if(gameObject.tag == "Team2Player")
		{
			
		if(other.tag == "Team2FlagBase")
		{
		
			print("Team2FlagBase");
			if(hasFlag && GM.GetComponent<ScoreScript>().Team2FlagInBase){
				GM.GetComponent<ScoreScript>().GetPoint2();
				setHasFlag(false);
				print("Gain Points");
			}
		
		}
		}else if(gameObject.tag == "Team1Player")
			{
		if(other.tag == "Team1FlagBase")
		{
		
			print("Team1FlagBase");
			if(hasFlag && GM.GetComponent<ScoreScript>().Team1FlagInBase){
				GM.GetComponent<ScoreScript>().GetPoint1();
				setHasFlag(false);
				print("Gain Points");
			}
		
		}
				
				
			}

		
		if(other.tag == "EnterPortal" && other.GetComponent<PortalScript>().isOn)
		{
			
			print("EnterPortal");
			transform.position = other.GetComponent<PortalScript>().otherPortal.transform.position;
		
			other.GetComponent<PortalScript>().otherPortal.GetComponent<PortalScript>().DestroyPortals();
			other.GetComponent<PortalScript>().DestroyPortals();
			
			
		
			
			
		}
		if(other.tag == "ExitPortal" && other.GetComponent<PortalScript>().isOn)
		{
			print("ExitPortal");
			transform.position = other.GetComponent<PortalScript>().otherPortal.transform.position;
			
			other.GetComponent<PortalScript>().otherPortal.GetComponent<PortalScript>().DestroyPortals();
			other.GetComponent<PortalScript>().DestroyPortals();


		}
		}
			
	
	}
	
	public void setHasFlag(bool a)
	{
		photonView.RPC("setHasFlagRPC",PhotonTargets.All,a);
	}
	
	[RPC]
	void setHasFlagRPC(bool a)
	{
		hasFlag = a;
	}
	
	
	
	[RPC]
	void setDataSync()
	{
		if(photonView.isMine)
		{
			setHasFlag(hasFlag);
		}
	}
	
}
