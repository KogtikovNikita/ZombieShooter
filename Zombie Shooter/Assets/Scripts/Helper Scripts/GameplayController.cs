using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour {

    public static GameplayController instance;


    [HideInInspector]
    public bool bulletAndbulletCreated, rockerBulletCreated;

    [HideInInspector]
    public bool playerAlive;

    private void Awake()
    {
        Makeinstance();
    }

    void Start()
    {
        playerAlive = true;
    }

    void  OnDisable()
    {
        instance = null; 
    }

    void Makeinstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

}
