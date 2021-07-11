using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Animator anim;          //Reference to the Animator component
    int deadParameterID;	//The ID of the animator parameter that opens the door
    public int maxHealth = 100;
    protected int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        deadParameterID = Animator.StringToHash("dead");
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        Debug.Log("Enemy die!");
        //GetComponent<SpriteRenderer>().sprite = null;
        GetComponent<Collider2D>().enabled = false;
        anim.SetTrigger(deadParameterID);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        this.enabled = false;
        Destroy(gameObject);
    }
    public bool isDead()
    {
        if (currentHealth <= 0)
            return true;
        return false;
    }
    public void reduceBar()
    {

    }
}
