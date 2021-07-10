using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public GameObject fireballPrefab;
    public Transform attackingPoint;
    public LayerMask enemyLayers;

    public int attackDamage = 40;
    public float attackRange = 0.5f;
    public float attackRate = 5f;
    float nextAttackTime = 0f;
    public float fireForce = 20f;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = GetComponent<CharacterController2D>().isGrounded();
        //Debug.Log(isGrounded);
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.X) && isGrounded)
            {
                Attack();
                nextAttackTime = Time.time +0.2f;
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                Shoot();
                nextAttackTime = Time.time + 0.2f;
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("attacking");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackingPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void Shoot()
    {
        GameObject fireball = Instantiate(fireballPrefab, attackingPoint.position, attackingPoint.rotation);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (transform.localScale.x < 0)
            rb.AddForce(-attackingPoint.right * fireForce, ForceMode2D.Impulse);
        else
            rb.AddForce(attackingPoint.right * fireForce, ForceMode2D.Impulse);

        //Instantiate(fireballPrefab, attackingPoint.position, attackingPoint.rotation);

    }



    private void OnDrawGizmosSelected()
    {
        if (attackingPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackingPoint.position, attackRange); 
    }

}
