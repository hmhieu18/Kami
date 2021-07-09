using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject hitEffect;
    public int damage = 10;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.1f);
        Destroy(gameObject);
        Enemy hittedEnnemy = collision.GetComponent<Enemy>();
        if (hittedEnnemy != null)
        {
            hittedEnnemy.TakeDamage(damage);
        }

    }
}
