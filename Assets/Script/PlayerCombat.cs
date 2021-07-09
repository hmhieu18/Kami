using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public Transform attackingPoint;
    public LayerMask enemyLayers;

    public int attackDamage = 40;
    public float attackRange = 0.5f;
    public float attackRate = 5f;
    float nextAttackTime = 0f;

    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = GetComponent<CharacterController2D>().isGrounded();
        //Debug.Log(isGrounded);
        if (Time.time >= nextAttackTime && isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Attack();
                nextAttackTime = Time.time +0.2f;
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

    private void OnDrawGizmosSelected()
    {
        if (attackingPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackingPoint.position, attackRange); 
    }

}
