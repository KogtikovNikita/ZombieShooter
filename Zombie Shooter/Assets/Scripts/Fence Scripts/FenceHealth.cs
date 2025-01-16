using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceHealth : MonoBehaviour {


	public int health = 100;
	public ParticleSystem woodBreakFx, woodExplodeFX;

	public void DealDamage(int damage)
    {
		health -= damage;
		woodBreakFx.Play();

		if (health <= 0)
        {
			woodExplodeFX.Play();
			AudioManager.instance.FenceExplosion();

			GameplayController.instance.fenceDestroyed = true;
			StartCoroutine(DeactivateGameObject());
        }
    }

	IEnumerator DeactivateGameObject()
    {
		yield return new WaitForSeconds(0.2f);
		gameObject.SetActive(false);
    }

}
