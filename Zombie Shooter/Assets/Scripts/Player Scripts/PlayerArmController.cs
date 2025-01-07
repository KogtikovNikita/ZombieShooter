using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmController : MonoBehaviour {

	public Sprite oneHandSprite;
	public Sprite twoHandSprite;

	private SpriteRenderer sr;


	void Awake () {
		sr = GetComponent<SpriteRenderer>();
	}

	public void ChangeToOneHand()
    {
		sr.sprite = oneHandSprite;
    }
	public void ChangeToTwoHand()
	{
		sr.sprite = twoHandSprite;
	}



}
