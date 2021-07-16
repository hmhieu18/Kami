using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBoss : Enemy
{
    public float respawnTimeFire, delayTimeFire;
    public GameObject fireBall;

    void Update()
    {

        anim.Play("idle");
        if (respawnTimeFire <= -1)
        {

            Instantiate(fireBall, transform.position, Quaternion.identity);

            respawnTimeFire = delayTimeFire;
        }
        else
        {
            respawnTimeFire -= Time.deltaTime;
        }
    }

}
