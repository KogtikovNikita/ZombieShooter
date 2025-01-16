using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

    public GameObject[] characters;

    [HideInInspector]
    public int characterIndex;
    

    void Starts()
    {
        characterIndex = 0;
    }
    void Awake()
    {
        MakeSingleton();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    void MakeSingleton()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if(scene.name != TagManager.MAIN_MENU_NAME)
        {
            if (characterIndex!=0)
            {
                GameObject tommy = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG);
                Instantiate(characters[characterIndex], tommy.transform.position, Quaternion.identity);
                tommy.SetActive(false);
            }
        }
    }
}
