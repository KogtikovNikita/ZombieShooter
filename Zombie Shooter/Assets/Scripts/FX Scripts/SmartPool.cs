using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPool : MonoBehaviour {

	public static SmartPool instance;

	private List<GameObject> bulletFallFx = new List<GameObject>();
	private List<GameObject> bulletPrefabs = new List<GameObject>();
	private List<GameObject> rocketBulletPrefabs = new List<GameObject>();

	public GameObject[] zombies;
	private float ySpawnPosMin = -3.7f, ySpawnPosMax = -0.36f;

	private Camera mainCamera;

	void Start()
    {
		mainCamera = Camera.main;
		InvokeRepeating("StartSpawningZombies", 1f, Random.Range(1f, 5f));

    }

	void Awake () {
		MakeInstance();
	}
	
	void OnDisable()
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
					SetBulletDamage(weaponName, bulletPrefabs[i]);

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
					SetBulletDamage(weaponName, rocketBulletPrefabs[i]);

					break;
				}
			}
        }

    }


	void SetBulletDamage(NameWeapon weaponName, GameObject bullet)
    {
        switch (weaponName)
        {
			case NameWeapon.PISTOL:
				bullet.GetComponent<BulletController>().damage = 2;
				break;
			case NameWeapon.MP5:
				bullet.GetComponent<BulletController>().damage = 3;
				break;
			case NameWeapon.M3:
				bullet.GetComponent<BulletController>().damage = 4;
				break;
			case NameWeapon.AK:
				bullet.GetComponent<BulletController>().damage = 5;
				break;
			case NameWeapon.AWP:
				bullet.GetComponent<BulletController>().damage = 10;
				break;
			case NameWeapon.ROCKET:
				bullet.GetComponent<BulletController>().damage = 10;
				break;
		}

    }

	void StartSpawningZombies()
    {
		if (GameplayController.instance.gameGoal == GameGoal.DEFEND_FENCE)
        {
			float xPos = mainCamera.transform.position.x;
			xPos += 15f;

			float yPos = Random.Range(ySpawnPosMin, ySpawnPosMax);

			Instantiate(zombies[Random.Range(0, zombies.Length)], new Vector3(xPos, yPos, 0f), Quaternion.identity);

        }
		if (GameplayController.instance.gameGoal == GameGoal.KILL_ZOMBIES || 
			GameplayController.instance.gameGoal == GameGoal.TIMER_COUNTDOWN ||
			GameplayController.instance.gameGoal == GameGoal.WALK_TO_GOAL_STEPS)
        {
			float xPos = mainCamera.transform.position.x;

			if(Random.Range(0,2) > 0)
            {
				xPos += Random.Range(10f, 15f);
            }
			else
            {
				xPos -= Random.Range(10f, 15f);
            }

			float yPos = Random.Range(ySpawnPosMin, ySpawnPosMax);

			Instantiate(zombies[Random.Range(0, zombies.Length)], new Vector3(xPos, yPos, 0f), Quaternion.identity);
		}
		if(GameplayController.instance.gameGoal == GameGoal.GAME_OVER)
        {
			CancelInvoke("StartSpawningZombies");
        }
    }
}
