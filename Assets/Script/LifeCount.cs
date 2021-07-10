using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    //public Image[] lives;
    public int liveRemaining;
    public Animator animator;
    private void Start() {
        UIManager.UpdateLivesUI(liveRemaining);
    }

    public void LoseLife()
    {
        animator.SetTrigger("death");
        liveRemaining--;
        UIManager.UpdateLivesUI(liveRemaining);
        //lives[liveRemaining].enabled = false;
        if(liveRemaining == 0)
        {
            GameManager.PlayerDied();
            // FindObjectOfType<PlayerMovement>().dead();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            LoseLife();
    }
}
