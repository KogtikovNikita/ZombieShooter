using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour {


	private WeaponManager weaponManager;

	[HideInInspector]
	public bool canShoot;

	private bool isHoldAttack;

	
	void Start () {
		weaponManager = GetComponent<WeaponManager>();
		canShoot = true;
	}
	
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q))
        {
			weaponManager.SwitchWeapon();
        }
	}
}
