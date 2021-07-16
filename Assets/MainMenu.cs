using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
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
}