using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {
	
	public GUIText ammo;
	
	// Use this for initialization
	void Start () {
		ammo.material.color = new Color(0,0,200.0f,1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// Draws the Ammo Count depending on the Weapon
	void OnGUI()
	{
		GameObject weapons = GameObject.FindGameObjectWithTag("Weapons");
		for(int i=0; i < weapons.transform.childCount; i++){
			if(weapons.transform.GetChild(i).gameObject.active == true){
				if(weapons.transform.GetChild(i).gameObject.name == "MachineGun"){
					MachineGun mg = weapons.transform.GetChild(i).gameObject.GetComponent<MachineGun>();
					ammo.text = mg.GetBulletsLeft().ToString();
				}
				else if(weapons.transform.GetChild(i).gameObject.name == "RocketLauncher"){
					RocketLauncher rl = weapons.transform.GetChild(i).gameObject.GetComponent<RocketLauncher>();
					ammo.text = rl.getAmmoCount().ToString();
				}
				else if(weapons.transform.GetChild(i).gameObject.name == "PortalGun"){
					//PortalLauncher pl = weapons.transform.GetChild(i).gameObject.GetComponent<PortalLauncher>();
					ammo.text = "";
				}
				else if(weapons.transform.GetChild(i).gameObject.name == "PortalGun"){
					//HookGunScript hg = weapons.transform.GetChild(i).gameObject.GetComponent<HookGunScript>();
					ammo.text = "";
				}
			}
		}
	}
}
