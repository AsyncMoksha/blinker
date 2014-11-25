using UnityEngine;
using System.Collections;

public class PlayerHookControl : Photon.MonoBehaviour {
	
	public GameObject hookgun; 
	public GameObject portalgun;
	public Animation hookAnimation;
	public bool isDrag;
	
	
	// Use this for initialization
	void Start () {
	
		hookAnimation = hookgun.transform.FindChild("Hook").GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
	
		
	}
	
	
	public void dragPosition(Vector3 dragposition)
	{
		photonView.RPC("DragPosition",PhotonTargets.All,dragposition);
	}
		
	public void hookout()
	{
		photonView.RPC("HookOut",PhotonTargets.All);
	}
	
	public void hookin()
	{
		photonView.RPC("HookIn",PhotonTargets.All);
	}
	
	[RPC]
	void DragPosition(Vector3 dragposition)
	{
		transform.position = dragposition;
	}
	

	
	[RPC]
	void HookOut()
	{
		hookAnimation.CrossFade("Hook");
		print("hook");
	}
	
	
	[RPC]
	void HookIn()
	{
		hookAnimation.CrossFade("In");
		print("in");
	}
	
}
