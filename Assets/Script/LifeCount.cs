using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    //public Image[] lives;
    public int liveRemaining;
    public Animator animator;

    public void LoseLife()
    {
        animator.SetTrigger("death");
        liveRemaining--;
        //lives[liveRemaining].enabled = false;
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
