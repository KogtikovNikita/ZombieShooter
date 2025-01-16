using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

	private ZombieMovement zombieMovement;
	private ZombieAnimation zombieAnimation;

	private Transform targetTransform;
	private bool canAttack;
	private bool zombieAlive;

	public GameObject damageCollider;
	public int zombieHealth = 10;

	public GameObject[] fxDead;

	private float timerAttack;
	private int fireDamage = 10;

	public GameObject coinCollectable;

	// Use this for initialization
	void Start () {
		zombieMovement = GetComponent<ZombieMovement>();
		zombieAnimation = GetComponent<ZombieAnimation>();

		zombieAlive = true;
		if (GameplayController.instance.zombieGoal == ZombieGoal.PLAYER)
		{
			targetTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
		}
		else if(GameplayController.instance.zombieGoal == ZombieGoal.FENCE)
		{
			GameObject[] fences = GameObject.FindGameObjectsWithTag(TagManager.FENCE_TAG);
			targetTransform = fences[Random.Range(0, fences.Length)].transform;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(zombieAlive)
        {
			CheckDistance();
        }
	}

	void CheckDistance()
    {
		if (targetTransform)
        {
			if(Vector3.Distance(targetTransform.position, transform.position) > 1.5f)
            {
				zombieMovement.Move(targetTransform);
            }
            else
            {
				if(canAttack)
                {
					zombieAnimation.Attack();


					timerAttack += Time.deltaTime;
					if(timerAttack > 0.45f)
                    {
						timerAttack = 0;
						AudioManager.instance.ZombieAttackSound();
                    }
					
                }
            }
        }
    }

	public void ActivateDeadEffect(int index)
    {
		fxDead[index].SetActive(true);

		if (fxDead[index].GetComponent<ParticleSystem>())
        {
			fxDead[index].GetComponent<ParticleSystem>().Play();

		}
    }


	IEnumerator DeactivateZombie()
    {
		AudioManager.instance.ZombieDieSound();

		yield return new WaitForSeconds(5f);

		GameplayController.instance.ZombieDied();


		if (Random.Range(0, 10) > 6)
			Instantiate(coinCollectable, transform.position, Quaternion.identity);
        

		

		gameObject.SetActive(false);
    }

	public void DealDamage(int damage)
    {
		zombieAnimation.Hurt();

		zombieHealth -= damage;

		if (zombieHealth <=0)
        {
			zombieAlive = false;
			zombieAnimation.Dead();

			StartCoroutine(DeactivateZombie());
		}

    }

	public void ActivateDamagePoint()
    {
		damageCollider.SetActive(true);
    }

	public void DeactivateDamagePoint()
    {
		damageCollider.SetActive(false);
    }

	void OnTriggerEnter2D(Collider2D target)
    {
		if(target.tag == TagManager.PLAYER_HEALTH_TAG || target.tag == TagManager.PLAYER_TAG ||
			target.tag == TagManager.FENCE_TAG)
        {
			canAttack = true;
        }

		if (target.tag == TagManager.BULLET_TAG || target.tag == TagManager.ROCKET_MISSILE_TAG)
        {
			zombieAnimation.Hurt();
			zombieHealth -= target.gameObject.GetComponent<BulletController>().damage;

			if (target.tag == TagManager.ROCKET_MISSILE_TAG)
            {
				target.gameObject.GetComponent<BulletController>().ExplosionFX();
            }

			if (zombieHealth <= 0)
            {
				zombieAlive = false;
				zombieAnimation.Dead();

				StartCoroutine(DeactivateZombie());
            }

			

			target.gameObject.SetActive(false);
        }
		if(target.tag == TagManager.FIRE_BULLET_TAG)
        {
			zombieAnimation.Hurt();

			zombieHealth -= fireDamage;

			if (zombieHealth <= 0)
            {
				zombieAlive = false;

				zombieAnimation.Dead();
				StartCoroutine(DeactivateZombie());

			}
        }
    }

	void OnTriggerExit2D(Collider2D target)
	{
		if (target.tag == TagManager.PLAYER_HEALTH_TAG || target.tag == TagManager.PLAYER_TAG ||
			target.tag == TagManager.FENCE_TAG)
		{
			canAttack = false;
		}
	}
}
