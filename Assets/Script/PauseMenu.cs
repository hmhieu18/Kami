using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    [SerializeField] Image pauseIcon;
    [SerializeField] Image playIcon;
    public GameObject audio;
    public GameObject ui;

    void Start()
    {
        if(GameIsPaused == false)
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

    public void OnButtonPress()
    {
        if(GameIsPaused == false)
        {
            Pause();
        }
        else
        {
            Resume();
        }
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon(){
        if(GameIsPaused == false)
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

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ExitButton() { 
        Application.Quit();
    }

    public void ShopButton() { 
        SceneManager.LoadScene("Store");
    }

    public void MapButton() { 
        SceneManager.LoadScene("Level Map");
        Destroy(audio);
        Destroy(ui);
    }



}
