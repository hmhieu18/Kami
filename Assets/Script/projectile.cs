using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
   
    private float respawnTimeFire;
    public float delayTimeFire;
    public GameObject fireBall;
    public int numberBall;
    public float speed = 3;
    public GameObject waitPos;

    Transform main;
    Vector2 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("Player").transform;

        targetPos = new Vector2(main.position.x, main.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(respawnTimeFire <= 0)
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
