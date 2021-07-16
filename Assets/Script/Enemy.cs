using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    protected Animator anim;          //Reference to the Animator component
    protected int deadParameterID;	//The ID of the animator parameter that opens the door
    public float maxHealth = 100.0f;
    protected float currentHealth;

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
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy die!");
        //GetComponent<SpriteRenderer>().sprite = null;
        // if(anim==null)
        // Debug.Log("NULL ANIMATION");
        anim.SetTrigger(deadParameterID);        
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 1f);
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
    public void PlayRiseSound(){
        AudioManager.PlaySkeletonRiseAudio();
    }
}
