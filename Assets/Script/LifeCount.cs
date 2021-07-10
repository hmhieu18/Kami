using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    public Image[] lives;
    public int liveRemaining;


    public void LoseLife()
    {
        liveRemaining--;
        lives[liveRemaining].enabled = false;
        if(liveRemaining == 0)
        {
            FindObjectOfType<PlayerMovement>().dead();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            LoseLife();
    }
}
