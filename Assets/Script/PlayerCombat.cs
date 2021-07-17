using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public GameObject fireballPrefab;
    public Transform attackingPoint;
    public Transform firePoint;
    public LayerMask enemyLayers;

    public int attackDamage = 40;
    public float attackRange = 0.5f;
    public float attackRate = 5f;
    float nextAttackTime = 0f;
    public float fireForce = 20f;
    bool isGrounded;
    public float laserLength = 40;
    public int laserDamage = 40;
    public GameObject impactEffect;
    public LineRenderer lineRenderer;


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
                nextAttackTime = Time.time + 0.2f;
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                Shoot();
                nextAttackTime = Time.time + 0.2f;
            }
            else
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    Kamehameha();
                }
                else if (Input.GetKeyUp(KeyCode.Z))
                {
                    lineRenderer.enabled = false;
                }
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("attacking");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackingPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<projectile>() != null)
            {
                Debug.Log("REDUCE BAR");
                enemy.GetComponent<projectile>().TakeDamage(attackDamage);
                // enemy.GetComponent<projectile>().reduceBar();
            }
            else
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
    }

    void Shoot()
    {
        animator.SetTrigger("shooting");
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (transform.localScale.x < 0)
            rb.AddForce(-firePoint.right * fireForce, ForceMode2D.Impulse);
        else
            rb.AddForce(firePoint.right * fireForce, ForceMode2D.Impulse);

        //Instantiate(fireballPrefab, attackingPoint.position, attackingPoint.rotation);

    }

    void Kamehameha()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);
        GameObject collisonObject = hitInfo.transform.gameObject;
        bool shootLaser = true;
        if (collisonObject.tag == "Collectable" || collisonObject.layer == LayerMask.NameToLayer("Controller"))
        {
            shootLaser = false;
        }

        if (collisonObject && shootLaser )
        {
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(laserDamage);
            }

            GameObject effect = Instantiate(impactEffect, hitInfo.point, Quaternion.identity);
            Destroy(effect, 0.1f);


            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            if (transform.localScale.x < 0)
                lineRenderer.SetPosition(1, firePoint.position - firePoint.right * laserLength);
            else
                lineRenderer.SetPosition(1, firePoint.position + firePoint.right * laserLength);

        }

        lineRenderer.enabled = true;

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
