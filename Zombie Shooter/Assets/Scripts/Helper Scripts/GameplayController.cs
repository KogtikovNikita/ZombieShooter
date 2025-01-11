using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieGoal
{
    PLAYER,
    FENCE
}


public class GameplayController : MonoBehaviour {

    public static GameplayController instance;


    [HideInInspector]
    public bool bulletAndbulletCreated, rockerBulletCreated;

    [HideInInspector]
    public bool playerAlive, fenceDestroyed;

    public ZombieGoal zombieGoal = ZombieGoal.PLAYER;

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
