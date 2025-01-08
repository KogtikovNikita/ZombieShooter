using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateFx : MonoBehaviour {

	void OnEnable () {
		Invoke("DeactivateGameObject", 2f);
	}

	void DeactivateGameObject()
    {
		gameObject.SetActive(false);
    }
}
