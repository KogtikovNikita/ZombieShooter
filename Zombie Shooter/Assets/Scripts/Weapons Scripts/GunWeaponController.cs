using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeaponController : WeaponController {


	public Transform spawnPoint;
	public GameObject bulletPrefab;
	public ParticleSystem fx_Shot;
	public GameObject fx_BulletFall;

	private Collider2D fireCollider;
	private WaitForSeconds wait_Time = new WaitForSeconds(0.2f);
	private WaitForSeconds fire_ColliderWait = new WaitForSeconds(0.02f);


	void Start () {
		
	}

    public override void ProcessAttack()
    {
        switch (nameWp)
        {
			case NameWeapon.PISTOL:
				print("Fired from PISTOL");
				break;
			case NameWeapon.MP5:
				print("Fired from MP5");
				break;
			case NameWeapon.M3:
				print("Fired from M3");
				break;
			case NameWeapon.AK:
				print("Fired from AK47");
				break;
			case NameWeapon.AWP:
				print("Fired from SNIPER");
				break;
			case NameWeapon.FIRE:
				print("Fired from FIRE");
				break;
			case NameWeapon.ROCKET:
				print("Fired from ROCKET LAUNCHER");
				break;

		}

		// SPAWN BULLET

    } 

	IEnumerator WaitForShootEffect()
    {
		yield return wait_Time;
		fx_Shot.Play();
    }

	IEnumerator ActivateFireCollider()
    {
		fireCollider.enabled = true;
		yield return fire_ColliderWait;
		fireCollider.enabled = false;
    }

}
