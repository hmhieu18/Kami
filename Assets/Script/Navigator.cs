using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigator : MonoBehaviour
{
    public SceneFader sceneFader;
    public void ExitButton() { 
        Application.Quit();
        Debug.Log("Game closed");
    }

    public void ShopButton() {
        StartCoroutine(AnimationFadeOutAndLoadScene("Store"));
    }

    public void MapButton() { 
        StartCoroutine(AnimationFadeOutAndLoadScene("Level Map"));
    }

    public void ToMenu() { 
        StartCoroutine(AnimationFadeOutAndLoadScene("Menu"));
    }

    public void ToLevel1() {
        StartCoroutine(AnimationFadeOutAndLoadScene("Level1"));
    }

    public void ToLevel2() { 
        StartCoroutine(AnimationFadeOutAndLoadScene("Level2"));
    }

    public void ToLevelBoss() { 
        StartCoroutine(AnimationFadeOutAndLoadScene("LevelBoss"));
    }
    // private void Start() {
    //  sceneFader.FadeSceneOut();   
    // }
    public IEnumerator AnimationFadeOutAndLoadScene(string sceneName)
    {
        sceneFader.FadeSceneOut();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
}