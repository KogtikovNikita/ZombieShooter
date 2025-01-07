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
		if (Input.GetKey(KeyCode.L))
        {
			isHoldAttack = true;
        }
		else
        {
			weaponManager.ResetAttack();
			isHoldAttack = false;
        }

		if (isHoldAttack && canShoot)
        {
			weaponManager.Attack();
        }
	}
}
