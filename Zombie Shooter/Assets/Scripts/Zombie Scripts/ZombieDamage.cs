﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamage : MonoBehaviour {

	public LayerMask collisionLayer;
	public float radius = 1f;

	public int damage = 3;

	// Update is called once per frame
	void Update () {
		if(GameplayController.instance.zombieGoal == ZombieGoal.PLAYER)
			AttackPlayer();
		if(GameplayController.instance.zombieGoal == ZombieGoal.FENCE)
			AttackFence();
	}

	void AttackPlayer()
    {
		if (GameplayController.instance.playerAlive)
        {
			Collider2D target = Physics2D.OverlapCircle(transform.position, radius, collisionLayer);

			if(target && target.tag == TagManager.PLAYER_HEALTH_TAG)
			{
				target.GetComponent<PlayerHealth>().DealDamage(damage);
				gameObject.transform.root.GetComponent<ZombieController>().DeactivateDamagePoint();
				
			}
        }

    }


	void AttackFence()
    {
		if(!GameplayController.instance.fenceDestroyed)
        {
			Collider2D target = Physics2D.OverlapCircle(transform.position, radius, collisionLayer);
			if(target.tag == TagManager.FENCE_TAG)
			{
				target.GetComponent<FenceHealth>().DealDamage(damage);
				gameObject.transform.root.GetComponent<ZombieController>().DeactivateDamagePoint();
			}
        }


    }
}
