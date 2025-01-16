using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ZombieGoal
{
    PLAYER,
    FENCE
}


public enum GameGoal
{
    KILL_ZOMBIES,
    WALK_TO_GOAL_STEPS,
    DEFEND_FENCE,
    TIMER_COUNTDOWN,
    GAME_OVER

}



public class GameplayController : MonoBehaviour {

    public static GameplayController instance;


    [HideInInspector]
    public bool bulletAndbulletCreated, rockerBulletCreated;

    [HideInInspector]
    public bool playerAlive, fenceDestroyed;

    public ZombieGoal zombieGoal = ZombieGoal.PLAYER;

    public GameGoal gameGoal = GameGoal.DEFEND_FENCE;

    public int zombieCount = 20;
    
    public int timerCount = 100;

    private Transform playerTarget;
    private Vector3 playerPrevousPosition;

    public int stepCount = 100;
    private int initialStepCount;

    private Text zombieCounterText, timerText, stepCounterText;
    private Image playerLife;

    [HideInInspector]
    public int coinCount;



    public GameObject pausePanel, gameOverPanel;
    private void Awake()
    {
        Makeinstance();
    }

    void Start()
    {
        playerAlive = true;
        if(gameGoal == GameGoal.WALK_TO_GOAL_STEPS)
        {
            playerTarget = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
            playerPrevousPosition = playerTarget.position;
            initialStepCount = stepCount;
            stepCounterText = GameObject.Find("Step Counter").GetComponent<Text>();
            stepCounterText.text = stepCount.ToString();

        }

        if(gameGoal == GameGoal.TIMER_COUNTDOWN || gameGoal == GameGoal.DEFEND_FENCE)
        {
            timerText = GameObject.Find("Timer Counter").GetComponent<Text>();
            timerText.text = timerCount.ToString();

            InvokeRepeating("TimerCountDown", 0f, 1f);


        }

        if (gameGoal == GameGoal.KILL_ZOMBIES)
        {
            zombieCounterText = GameObject.Find("Zombie Counter").GetComponent<Text>();
            zombieCounterText.text = zombieCount.ToString();
        }

        playerLife = GameObject.Find("Life Full").GetComponent<Image>();
    }

    void  OnDisable()
    {
        instance = null; 
    }

    void Update()
    {
        if(gameGoal == GameGoal.WALK_TO_GOAL_STEPS)
        {
            CountPlayerMovement();
        }
    }

    void CountPlayerMovement()
    {
        Vector3 playerCurrentMovement = playerTarget.position;
        float dist = Vector3.Distance(new Vector3(playerCurrentMovement.x, 0f, 0f),
            new Vector3(playerPrevousPosition.x, 0f, 0f));

        if (playerCurrentMovement.x > playerPrevousPosition.x)
        {
            if (dist > 1)
            {
                stepCount--;

                if (stepCount <=0)
                    GameOver();
                
                playerPrevousPosition = playerTarget.position;
                
            }
            
        } 
        else if (playerCurrentMovement.x < playerPrevousPosition.x )
        {
            if (dist > 0.8f)
            {
                stepCount++;

                if (stepCount >= initialStepCount)
                    stepCount = initialStepCount;

                playerPrevousPosition = playerTarget.position;
            }
        }


        stepCounterText.text = stepCount.ToString();
    }

    void Makeinstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void TimerCountDown()
    {
        timerCount--;
        timerText.text = timerCount.ToString();

        if(timerCount <= 0)
        {
           
            CancelInvoke("TimerCountDown"); 
            GameOver();
        }
    }

    public void ZombieDied()
    {
        zombieCount--;
        if(gameGoal == GameGoal.KILL_ZOMBIES)
            zombieCounterText.text = zombieCount.ToString();

        if(zombieCount <= 0)
        {
            GameOver();
        }
    }

    public void PlayerLifeCounter(float fillPercentage)
    {
        fillPercentage /= 100f;
        playerLife.fillAmount = fillPercentage;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(TagManager.MAIN_MENU_NAME);
    }


}
