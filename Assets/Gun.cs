using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform attackingPoint;
    public float fireForce = 20f;
    private float respawnTimeFire;
    public float delayTimeFire;
    public int numberBall;
    public float speed = 3;
    void Shoot()
    {
        GameObject fireball = Instantiate(fireballPrefab, attackingPoint.position, attackingPoint.rotation);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (transform.localScale.x < 0)
            rb.AddForce(-attackingPoint.right * fireForce, ForceMode2D.Impulse);
        else
            rb.AddForce(attackingPoint.right * fireForce, ForceMode2D.Impulse);
    
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (respawnTimeFire <= 0)
        {
            Shoot();
            respawnTimeFire = delayTimeFire;
        }
        else
        {
            respawnTimeFire -= Time.deltaTime;
        }
    }
}
