using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public int health = 100;

	public GameObject[] bloodFx;

	private PlayerAnimations playerAnim;




	void Awake () {
		playerAnim = GetComponentInParent<PlayerAnimations>();
	}
	
	public void DealDamage (int damage)
    {
		health -= damage;

		GameplayController.instance.PlayerLifeCounter(health);

		//playerAnim.Hurt();

		if (health <= 0)
        {

			GameplayController.instance.playerAlive = false;

			GetComponent<Collider2D>().enabled = false;

			playerAnim.Dead();
			bloodFx[Random.Range(0, bloodFx.Length)].SetActive(true);

        }
    }
}
