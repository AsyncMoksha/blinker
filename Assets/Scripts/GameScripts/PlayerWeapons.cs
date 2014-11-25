using UnityEngine;
using System.Collections;

public class PlayerWeapons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Select the first weapon
		SelectWeapon("HookGun");
	}
	
	// Update is called once per frame
	void Update () {
		// Did the user press fire?

		if (Input.GetKeyDown("1")) {
			Debug.Log("selected machine gun");
			SelectWeapon("PortalGun");
		
		}	
		else if (Input.GetKeyDown("2")) {
			Debug.Log("selected rocket launcher gun");
			SelectWeapon("HookGun");
		}	
		else if (Input.GetKeyDown("3"))
		{
			Debug.Log("selected portal gun");
			SelectWeapon("FreezeGun");
		}

	
	}
	
	void SelectWeapon (string weaponName) {
		for (int i=0;i<transform.childCount;i++)	{
			// Activate the selected weapon
			if (transform.GetChild(i).name == weaponName)
				transform.GetChild(i).gameObject.SetActiveRecursively(true);
			
			// Deactivate all other weapons
			else
				transform.GetChild(i).gameObject.SetActiveRecursively(false);
				print(transform.GetChild(i).name);
		}
	}
}
