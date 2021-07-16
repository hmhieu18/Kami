using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigator : MonoBehaviour
{
    public void ExitButton() { 
        Application.Quit();
        Debug.Log("Game closed");
    }

    public void ShopButton() { 
        SceneManager.LoadScene("Store");
    }

    public void MapButton() { 
        SceneManager.LoadScene("Level Map");
    }

    public void ToMenu() { 
        SceneManager.LoadScene("Menu");
    }

    public void ToLevel1() { 
        SceneManager.LoadScene("Level1");
    }

    public void ToLevel2() { 
        SceneManager.LoadScene("Level2");
    }

    public void ToLevelBoss() { 
        SceneManager.LoadScene("LevelBoss");
    }
}