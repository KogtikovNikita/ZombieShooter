using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeaponController : WeaponController {


	public Transform spawnPoint;
	public GameObject bulletPrefab;
	public ParticleSystem fx_Shot;
	public GameObject fx_BulletFall;

	private Collider2D fireCollider;
	private WaitForSeconds wait_Time = new WaitForSeconds(0.02f);
	private WaitForSeconds fire_ColliderWait = new WaitForSeconds(0.02f);


	void Start () {

		if (!GameplayController.instance.bulletAndbulletCreated)
        {
			GameplayController.instance.bulletAndbulletCreated = true;
			if(nameWp != NameWeapon.FIRE && nameWp != NameWeapon.ROCKET)
			{
				SmartPool.instance.CreateBulletAndBulletFall(bulletPrefab, fx_BulletFall, 100);
			}

        }

		if(!GameplayController.instance.rockerBulletCreated)
        {
			if(nameWp == NameWeapon.ROCKET)
            {
				GameplayController.instance.rockerBulletCreated = true;
				SmartPool.instance.CreateRocket(bulletPrefab, 100);
            }
        }

		if(nameWp == NameWeapon.FIRE)
        {
			fireCollider = spawnPoint.GetComponent<BoxCollider2D>();
        }




	}

    public override void ProcessAttack()
    {
        switch (nameWp)
        {
			case NameWeapon.PISTOL:
				AudioManager.instance.GunSound(0);
				break;
			case NameWeapon.MP5:
				AudioManager.instance.GunSound(1);
				break;
			case NameWeapon.M3:
				AudioManager.instance.GunSound(2);
				break;
			case NameWeapon.AK:
				AudioManager.instance.GunSound(3);
				break;
			case NameWeapon.AWP:
				AudioManager.instance.GunSound(4);
				break;
			case NameWeapon.FIRE:
				AudioManager.instance.GunSound(5);
				break;
			case NameWeapon.ROCKET:
				AudioManager.instance.GunSound(6);
				break;

		}

		// SPAWN BULLET
		if((transform != null) && (nameWp != NameWeapon.FIRE))
        {
			if(nameWp != NameWeapon.ROCKET)
            {
				GameObject bulletFallFx = SmartPool.instance.SpawnBulletFallFx(spawnPoint.transform.position,
					Quaternion.identity);

				bulletFallFx.transform.localScale = (transform.root.eulerAngles.y > 1.0f) ? new Vector3(-1f, 1f, 1f) :
					new Vector3(1f, 1f, 1f);


				StartCoroutine(WaitForShootEffect());
            }
			SmartPool.instance.SpawnBullet(spawnPoint.transform.position, 
				new Vector3(-transform.root.localScale.x, 0f, 0f),
				spawnPoint.rotation, nameWp);
        }
		else
        {
			StartCoroutine(ActivateFireCollider());
        }

    } 

	IEnumerator WaitForShootEffect()
    {
		yield return wait_Time;
		fx_Shot.Play();
    }

	IEnumerator ActivateFireCollider()
    {
		fireCollider.enabled = true;

		fx_Shot.Play();

		yield return fire_ColliderWait;
		fireCollider.enabled = false;
    }

}
