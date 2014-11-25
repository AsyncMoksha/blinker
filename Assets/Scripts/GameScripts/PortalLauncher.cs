using UnityEngine;
using System.Collections;

public class PortalLauncher : Photon.MonoBehaviour {
	public Transform enter_portal;
	public Transform exit_portal;
	//public LineRenderer line;
	public GameObject EnterPortal;
	public GameObject ExitPortal;
	public float MAX_PORTAL_RANGE = 50.0f;
	public bool canControl = true;
	public AudioClip portalSFX;
	public ScoreScript scoreScript;
	
	
	void Start () {
		scoreScript = GameObject.Find("GM").GetComponent<ScoreScript>();
		
	}

	
	// Update is called once per frame
	void Update () {
		
		if(canControl)
		{	
			
		Vector3 fwd = transform.TransformDirection(Vector3.forward);

		RaycastHit hit = new RaycastHit();
	/*

		if(Physics.Raycast(transform.position, fwd, out hit, MAX_PORTAL_RANGE))
		{
			Debug.DrawLine(transform.position, hit.point, Color.red);
			//Vector3 temp = new Vector3();
			LineRenderer lr = GetComponent(typeof(LineRenderer)) as LineRenderer;
			lr.SetColors(Color.red, Color.red);
			lr.SetWidth(0.05f,0.05f);
			lr.SetVertexCount(2);
			//temp = hit.point;
			//temp.z -= 0.1f;
			lr.SetPosition(0, transform.position);
			lr.SetPosition(1, hit.point);
		}
		
		
		*/
		
		// If RayCast is pointing at an object AND fire button is selected, generate Portal
		//if(Physics.Raycast(transform.position, fwd, out hit, MAX_PORTAL_RANGE) && 
		if(Physics.Raycast(transform.position, fwd, out hit) && 
		   Input.GetButtonDown("Fire1"))
		{
				AudioSource.PlayClipAtPoint(portalSFX,transform.position,1.0f);
				//scoreScript.runReloadBar();
				//scoreScript.Test();
				//ScoreScript scoreScript = (ScoreScript) GM.GetComponent(typeof(ScoreScript));
				//scoreScript.Test();
				
				
				
			if(EnterPortal != null && ExitPortal != null)
			{
				
				
				DestroyPortals();

				Transform t = PhotonNetwork.Instantiate(enter_portal, hit.point + hit.normal, Quaternion.identity,0).transform;
				t.LookAt(hit.point + hit.normal + hit.normal);
	
				EnterPortal = t.gameObject;
//				EnterPortal.GetComponent<PortalScript>().portalLauncherScript = this;


				
				
				
			}
			else if(EnterPortal != null && ExitPortal == null)
			{
				//AudioSource.PlayClipAtPoint(portalSFX,hit.transform.position);
					
				Transform t = PhotonNetwork.Instantiate(exit_portal, hit.point + hit.normal, Quaternion.identity,0).transform;
				t.LookAt(hit.point + hit.normal + hit.normal);
				ExitPortal = t.gameObject;

				// Link the Enter Portal's "otherPortal" to Exit Portal
				
				EnterPortal.GetComponent<PortalScript>().setOtherPortal();
				EnterPortal.GetComponent<PortalScript>().otherPortal = ExitPortal;
				EnterPortal.GetComponent<PortalScript>().TurnOn();
				EnterPortal.GetComponent<PortalScript>().isOn = true;

				
				// Link the Exit Portal's "otherPortal" to Enter Portal
				ExitPortal.GetComponent<PortalScript>().setOtherPortal();
				ExitPortal.GetComponent<PortalScript>().otherPortal = EnterPortal;
				ExitPortal.GetComponent<PortalScript>().TurnOn();
				ExitPortal.GetComponent<PortalScript>().isOn = true;
				
				//Cool down:
				scoreScript.resetPortalReloadBar();
				scoreScript.runPortalReloadBar();
				

			}
			else if(EnterPortal == null && ExitPortal == null)
			{
				//AudioSource.PlayClipAtPoint(portalSFX,hit.transform.position);
					
				print(hit.normal);
				Transform t = PhotonNetwork.Instantiate(enter_portal, hit.point + hit.normal, Quaternion.identity,0).transform;
				t.LookAt(hit.point + hit.normal + hit.normal);
				
				
				EnterPortal = t.gameObject;
//				EnterPortal.GetComponent<PortalScript>().portalLauncherScript = this;
				
					

			}
			else
			{

			}

		}
		
		}
		
		
	}
	
	public void DestroyPortals() {
		PhotonNetwork.Destroy(EnterPortal);
		PhotonNetwork.Destroy(ExitPortal);
		EnterPortal = null;
		ExitPortal = null;
	}
	
}
