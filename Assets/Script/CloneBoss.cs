using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBoss : projectile
{
    // Start is called before the first frame update

    // Update is called once per frame
   
    void Update()
    {
        
        bossAnimator.Play("bossIdle");
        if (respawnTimeFire <= -1)
        {
            nextId += idChangeValue;
            //boss.Play("wizard fly forward (1)_0");

            Instantiate(fireBall, transform.position, Quaternion.identity);

            respawnTimeFire = delayTimeFire;
        }
        else
        {
            respawnTimeFire -= Time.deltaTime;
            //boss.Play("Wizard");
        }
    }
    
    private void Reset()
    {
        
    }
}
