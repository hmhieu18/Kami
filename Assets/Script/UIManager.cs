﻿// This script is a Manager that controls the UI HUD (deaths, time, and coins) for the 
// project. All HUD UI commands are issued through the static methods of this class

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    //This class holds a static reference to itself to ensure that there will only be
    //one in existence. This is often referred to as a "singleton" design pattern. Other
    //scripts access this one through its public static methods
    static UIManager current;
    public Image healthBar;

    public TextMeshProUGUI coinText;            //Text element showing number of coins
    public TextMeshProUGUI timeText;        //Text element showing amount of time
    public TextMeshProUGUI deathText;       //Text element showing number or deaths
    public TextMeshProUGUI gameOverText;    //Text element showing the Game Over message
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    [SerializeField] Image pauseIcon;
    [SerializeField] Image playIcon;
    public GameObject audio;
    public GameObject uiManager;
    public GameObject mobileInputUI;

    void Awake()
    {
        //If an UIManager exists and it is not this...
        if (current != null && current != this)
        {
            //...destroy this and exit. There can be only one UIManager
            Destroy(gameObject);
            return;
        }

        //This is the current UIManager and it should persist between scene loads
        current = this;
        // DontDestroyOnLoad(gameObject);
        
        if (GameIsPaused == false)
        {
            pauseIcon.enabled = true;
            playIcon.enabled = false;
        }
        else
        {
            pauseIcon.enabled = false;
            playIcon.enabled = true;
        }
    }

    public static void UpdateCoinUI(int coinCount)
    {
        //If there is no current UIManager, exit
        if (current == null)
            return;

        //Update the text Coin element
        current.coinText.text = coinCount.ToString();
    }

    // public static void UpdateTimeUI(float time)
    // {
    //     //If there is no current UIManager, exit
    //     if (current == null)
    //         return;

    //     //Take the time and convert it into the number of minutes and seconds
    //     int minutes = (int)(time / 60);
    //     float seconds = time % 60f;

    //     //Create the string in the appropriate format for the time
    //     current.timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    // }

    // public static void UpdateDeathUI(int deathCount)
    // {
    // 	//If there is no current UIManager, exit
    // 	if (current == null)
    // 		return;

    // 	//update the player death count element
    // 	current.deathText.text = deathCount.ToString();
    // }

    public static void UpdateLivesUI(int livesCount)
    {
        //If there is no current UIManager, exit
        if (current == null)
            return;

        //update the player death count element
        current.deathText.text = livesCount.ToString();
    }
    public static void DisplayGameOverText()
    {
        //If there is no current UIManager, exit
        if (current == null)
            return;

        //Show the game over text
        current.gameOverText.enabled = true;
    }
    public static void UpdateHealthBar(float health)
    {
        Debug.Log(health.ToString());
        current.healthBar.fillAmount = health;
    }




    public void OnButtonPress()
    {
        Debug.Log("PAUSE");
        if (GameIsPaused == false)
        {
            Pause();
        }
        else
        {
            Resume();
        }
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if (GameIsPaused == false)
        {
            pauseIcon.enabled = true;
            playIcon.enabled = false;
        }
        else
        {
            pauseIcon.enabled = false;
            playIcon.enabled = true;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ShopButton()
    {
        SceneManager.LoadScene("Store");
    }

    public void MapButton()
    {
        Resume();
        SceneManager.LoadScene("Level Map");
        Destroy(audio);
        Destroy(uiManager);
        Destroy(mobileInputUI);
    }
}
