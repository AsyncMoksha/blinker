using UnityEngine;
using System.Collections;

public class HookGunScript : MonoBehaviour {
	public float reloadTime = 2000f;
	private float lastShot = -10.0f; // sets a certain amount of time before you can shoot again
	
	public bool isFrozen = false;
	public float FREEZE_TIME = 5.0f;
	
	
	public Animation _animation;
	public GameObject hook;
	public bool canControl = true;
	public bool isDrag;
	
	public GameObject Player;
	public ScoreScript scoreScript;
	
	// Use this for initialization
	void Start () {
		_animation = transform.FindChild("Hook").GetComponent<Animation>();
		_animation["In"].speed = 2f;
		_animation["Out"].speed = 2f;
		_animation["Hook"].speed = 2f;
		
		Player = transform.parent.parent.parent.gameObject;
		
	
		scoreScript = GameObject.Find("GM").GetComponent<ScoreScript>();
		
	
		
	}
	
	// Update is called once per frame
	void Update () {
		if(canControl)
		{

		if(Input.GetButtonDown("Fire1"))
		{
			
				
			//Cool down:
				scoreScript.resetHookReloadBar();
				scoreScript.runHookReloadBar();
				
					Player.GetComponent<PlayerHookControl>().hookout();
		
		}
		
		

		
		}
		
		
		
		
		
		
		
		
		
		
	}
	
	void Fire(){
		// Did the time exceed the reload time?
		if (UnityEngine.Time.time > reloadTime + lastShot && !_animation.isPlaying) {
			// create a new projectile, use the same position and rotation as the hook gun.
			//Rigidbody instantiatedProjectile = (Rigidbody) Instantiate (hookPrefab, transform.position, transform.rotation);
			
			// set the initial position of the HOOK
			//HookScript hs = instantiatedProjectile.gameObject.GetComponent(typeof(HookScript)) as HookScript;	
			//hs.initial_position = transform.position;
			
			// Give it an initial forward velocity. The direction is along the z-axis of the hook gun transform.
			//instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, initialSpeed));
	
			// Ignore collisions between the hook and the character controller
			//Physics.IgnoreCollision(instantiatedProjectile.collider, transform.root.collider);
			
			lastShot = UnityEngine.Time.time;
			//ammoCount--;
			
			print("fire");
			
			_animation.CrossFade("Hook");
		
			
			hook.GetComponent<HookScript>().initial_position = hook.transform.position;
			
		}
	}
}
