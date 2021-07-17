using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public List<GameObject> levelList;
    void Start()
    {
        if (GlobalGameManager.thisUser.curLevel == 1)
        {
            levelList[0].SetActive(true);
            levelList[1].SetActive(false);
            levelList[2].SetActive(false);

        }
        else if (GlobalGameManager.thisUser.curLevel == 2)
        {
            levelList[0].SetActive(true);
            levelList[1].SetActive(true);
            levelList[2].SetActive(false);
        }
        else
        {
            levelList[0].SetActive(true);
            levelList[1].SetActive(true);
            levelList[2].SetActive(true);
        }
    }
}
