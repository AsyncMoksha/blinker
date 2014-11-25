using UnityEngine;
using System.Collections;

public class HookScript : MonoBehaviour {
	
	public Vector3 initial_position = new Vector3(0.0f, 0.0f, 0.0f); // This is the position where the targeted player will be sent to.
	public GameObject myPlayer;
	public GameObject HookGun;
	
	public GameObject HookedPlayer;
	public GameObject Player;
	public bool isDrag;
	public AudioClip hookSFX;
	
	// Use this for initialization
	void Start () {
	Player = HookGun.transform.parent.parent.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(isDrag && HookedPlayer != null && HookGun.GetComponent<HookGunScript>()._animation.isPlaying)
		{
			HookedPlayer.GetComponent<PlayerHookControl>().dragPosition(transform.position);
			print("isDraging!!!!!!!!!");
		}
		
		if(isDrag && !HookGun.GetComponent<HookGunScript>()._animation.isPlaying)
		{
			HookedPlayer = null;
			isDrag = false;
			print("Draging End!!!!!!!!!");
		}
		
		
		
	}
	
	void OnTriggerEnter(Collider c){
		if(HookGun.GetComponent<HookGunScript>()._animation.isPlaying){
		print("!!!");
		
		if(c.gameObject.name.Contains("Player")&&c.gameObject.name != myPlayer.name){
			Debug.Log("hook collided with player");
			

			Player.GetComponent<PlayerHookControl>().hookin();


			HookGun.GetComponent<HookGunScript>().isDrag = true;
			
			isDrag = true;
			HookedPlayer = c.gameObject;
				
			AudioSource.PlayClipAtPoint(hookSFX, transform.position);

			

		}
		}
	}
	
	public void releasePlayer(){
		if (HookedPlayer.tag == "Team1Player")
		{
				print("released!!!!!!!!!!!!!!!!!!!!!!!!");
				isDrag = false;
				HookedPlayer = null;
		}else if(HookedPlayer.tag == "Team2Player")
		{
				//HookedPlayer.GetComponent<Team2PlayerScript>().isDrag = false;
				//HookedPlayer.GetComponent<Team2PlayerScript>().hookhand = null;
				//HookedPlayer = null;
		}
	}

	
}
