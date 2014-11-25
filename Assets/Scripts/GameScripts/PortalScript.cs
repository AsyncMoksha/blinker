using UnityEngine;
using System.Collections;

public class PortalScript : Photon.MonoBehaviour {
	
	public GameObject otherPortal;
	public bool isOn = false;
	
	
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, 0, 5));
	}
	
	
	
	public void TurnOn()
	{
		photonView.RPC("turnOn",PhotonTargets.All);
		
		
	}
	
	public void setOtherPortal()
	{
		photonView.RPC("setOtherPortalRPC",PhotonTargets.All);
		
	}
	
	
	public void DestroyPortals(){		
		PhotonNetwork.Destroy(gameObject);
	}
	
	
	
	
	[RPC]
	void setOtherPortalRPC()
	{
		print("RPC set OtherPortalRPC");
		
		if(gameObject.tag == "EnterPortal")
		{
			foreach(GameObject portal in GameObject.FindGameObjectsWithTag("ExitPortal"))
			{
				if(GetComponent<PhotonView>().owner == photonView.owner)
				{
					otherPortal = portal;
				}
			}
			
			
		}
		if(gameObject.tag == "ExitPortal")
		{
			foreach(GameObject portal in GameObject.FindGameObjectsWithTag("EnterPortal"))
			{
				if(GetComponent<PhotonView>().owner == photonView.owner)
				{
					otherPortal = portal;
				}
			}
			
		}
		
		
	}
	

	
	[RPC]
	void turnOn()
	{
		isOn = true;
		
	}
	
	[RPC]
	void DestroyPortal()
	{
		PhotonView.Destroy(gameObject);
	}
}
