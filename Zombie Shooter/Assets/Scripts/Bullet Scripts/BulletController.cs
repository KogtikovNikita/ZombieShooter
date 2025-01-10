using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	[HideInInspector]
	public int damage;

	private float speed = 60f;

	private float defaultTimeAlive = 2f;
	private WaitForSeconds waitForTimeAlive = new WaitForSeconds(2f);
	private IEnumerator coroutineDeactivate;

	private Vector3 direction;

	public GameObject rocketExplosion;
	
	void Start () {
		if(this.tag == TagManager.ROCKET_MISSILE_TAG)
        {
			speed = 8f;
        }
	}
	
	
	void Update () {
		transform.Translate(direction * speed * Time.deltaTime);
	}

	void OnEnable()
    {
		coroutineDeactivate = WaitForDiactivate();
		StartCoroutine(coroutineDeactivate);
    }

	void OnDisable()
    {
		if (coroutineDeactivate != null)
        {
			StopCoroutine(coroutineDeactivate);
        }
    }

	public void SetDirection(Vector3 dir)
    {
		direction = dir;
    }

	IEnumerator WaitForDiactivate()
    {
		yield return waitForTimeAlive;
		gameObject.SetActive(false);

    }

	public void ExplosionFX()
    {
		Instantiate(rocketExplosion, transform.position, Quaternion.identity);
    }
}
