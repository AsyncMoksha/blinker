using UnityEngine;
using System.Collections;

public class RocketLauncher : MonoBehaviour {
	
	public Rigidbody projectile;
	public float initialSpeed = 20.0f;
	public float reloadTime = 0.5f;
	public int ammoCount = 20;
	private float lastShot = -10.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Fire() {
		// Did the time exceed the reload time?
		if (UnityEngine.Time.time > reloadTime + lastShot && ammoCount > 0) {
			// create a new projectile, use the same position and rotation as the Launcher.
			Rigidbody instantiatedProjectile = (Rigidbody) Instantiate (projectile, transform.position, transform.rotation);
				
			// Give it an initial forward velocity. The direction is along the z-axis of the missile launcher's transform.
			instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, initialSpeed));
	
			// Ignore collisions between the missile and the character controller
			Physics.IgnoreCollision(instantiatedProjectile.collider, transform.root.collider);
			
			lastShot = UnityEngine.Time.time;
			ammoCount--;
		}
	}
	
	public int getAmmoCount()
	{
		return ammoCount;	
	}
}
