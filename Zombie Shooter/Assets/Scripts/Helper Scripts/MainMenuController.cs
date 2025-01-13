using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public GameObject characterSelectPanel;

	
	public void StartMissionOne()
    {
		SceneManager.LoadScene(TagManager.LEVEL_1_NAME);
    }

	public void StartMissionTwo()
	{
		SceneManager.LoadScene(TagManager.LEVEL_2_NAME);
	}

	public void StartMissionThree()
	{
		SceneManager.LoadScene(TagManager.LEVEL_3_NAME);
	}

	public void StartMissionFour()
	{
		SceneManager.LoadScene(TagManager.LEVEL_4_NAME);
	}

	public void OpenCharacterSelectPanel()
    {
		characterSelectPanel.SetActive(true);
    }

	public void CloseCharacterSelectPanel()
	{
		characterSelectPanel.SetActive(false);
	}
}
