﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkBounds : MonoBehaviour {

	public float minX, maxX;

	
	// Update is called once per frame
	void Update () {
		MovementBounds();
	}

	void MovementBounds()
    {
		Vector3 temp = transform.position;

		if (temp.x < minX)
			temp.x = minX;
		if (temp.x > maxX)
			temp.x = maxX;

		transform.position = temp;
    }
}
