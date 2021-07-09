using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHeath;
    // Start is called before the first frame update
    void Start()
    {
        currentHeath = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHeath -= damage;
        if (currentHeath <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy die!");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
