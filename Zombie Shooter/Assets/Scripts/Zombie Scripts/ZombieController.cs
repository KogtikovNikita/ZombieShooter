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
		targetTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
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
                }
            }
        }
    }


	void OnTriggerEnter2D(Collider2D target)
    {
		if(target.tag == TagManager.PLAYER_HEALTH_TAG || target.tag == TagManager.PLAYER_TAG ||
			target.tag == TagManager.FENCE_TAG)
        {
			canAttack = true;
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
