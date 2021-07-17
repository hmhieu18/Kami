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
    public float laserLength = 25;
    public int laserDamage = 1;
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
        RaycastHit2D[] hitObjects;
        hitObjects = Physics2D.RaycastAll(firePoint.position, firePoint.right, laserLength);
        bool shootLaser = false;
        foreach (RaycastHit2D hitObject in hitObjects)
        {
            GameObject collisonObject = hitObject.transform.gameObject;
            Debug.Log(collisonObject.name);
            if (collisonObject.tag == "Collectable" || collisonObject.layer == LayerMask.NameToLayer("Controller"))
                continue;
            else
            {
                Enemy enemy = hitObject.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(laserDamage);
                }

                GameObject effect = Instantiate(impactEffect, hitObject.point, Quaternion.identity);
                Destroy(effect, 0.1f);

                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, hitObject.point);
                shootLaser = true;
                break;
            }
        }
        if (!shootLaser)
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
