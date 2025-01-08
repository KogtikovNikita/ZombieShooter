using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPool : MonoBehaviour {

	public static SmartPool instance;

	private List<GameObject> bulletFallFx = new List<GameObject>();
	private List<GameObject> bulletPrefabs = new List<GameObject>();
	private List<GameObject> rocketBulletPrefabs = new List<GameObject>();



	void Start () {
		MakeInstance();
	}
	
	void Disable()
    {
		instance = null;
    }

	void MakeInstance()
    {
		if (instance == null)
			instance = this;
    }

	public void CreateBulletAndBulletFall(GameObject bullet, GameObject bulletFall, int count)
    {
		for (int i = 0; i < count; i++)
        {
			GameObject tempBullet = Instantiate(bullet);
			GameObject tempBulletFall = Instantiate(bulletFall);

			bulletPrefabs.Add(tempBullet);
			bulletFallFx.Add(tempBulletFall);

			bulletPrefabs[i].SetActive(false);
			bulletFallFx[i].SetActive(false);
        }
    }

	public void CreateRocket(GameObject rocket, int count)
    {
		for (int i = 0; i < count; i++)
        {
			GameObject tempRocketBullet = Instantiate(rocket);

			rocketBulletPrefabs.Add(tempRocketBullet);

			rocketBulletPrefabs[i].SetActive(false);
        }
    }


	public GameObject SpawnBulletFallFx(Vector3 position, Quaternion rotation)
    {
		for (int i = 0; i < bulletFallFx.Count; i++)
        {
			if(!bulletFallFx[i].activeInHierarchy)
            {
				bulletFallFx[i].SetActive(true);
				bulletFallFx[i].transform.position = position;
				bulletFallFx[i].transform.rotation = rotation;

				return bulletFallFx[i];
            }
        }


		return null;
    }

	public void SpawnBullet(Vector3 position, Vector3 direction, Quaternion rotation, NameWeapon weaponName)
    {
		if (weaponName != NameWeapon.ROCKET)
        {
			for (int i = 0; i<bulletPrefabs.Count; i++)
            {
				if(!bulletPrefabs[i].activeInHierarchy)
                {
					bulletPrefabs[i].SetActive(true);
					bulletPrefabs[i].transform.position = position;
					bulletPrefabs[i].transform.rotation = rotation;


					// GET THE BULLET SCRIPT
					bulletPrefabs[i].GetComponent<BulletController>().SetDirection(direction);

					// SET BULLET DAMAGE

					break;
                }
            }
        }
        else
        {
			for (int i = 0; i<rocketBulletPrefabs.Count; i++)
            {
				if(!rocketBulletPrefabs[i].activeInHierarchy)
                {
					rocketBulletPrefabs[i].SetActive(true);
					rocketBulletPrefabs[i].transform.position = position;
					rocketBulletPrefabs[i].transform.rotation = rotation;


					// GET THE BULLET SCRIPT
					rocketBulletPrefabs[i].GetComponent<BulletController>().SetDirection(direction);


					// SET BULLET DAMAGE

					break;
				}
			}
        }

    }
}
