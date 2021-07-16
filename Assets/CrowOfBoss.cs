using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowOfBoss : MonoBehaviour
{
    // Start is called before the first frame update
    Transform main;
    Vector2 targetPos;
    public float speed = 4;
    public bool isFacingLeft = false;

    public bool firstBite = true;
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {

        // Debug.Log("CROW HEADING TARGET");
        if (firstBite)
        {
            targetPos = new Vector2(main.position.x, main.position.y);
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if (transform.position.x < main.position.x && !isFacingLeft)
        {
            Flip();
        }
        else if (transform.position.x > main.position.x && isFacingLeft)
        {
            Flip();
        }
        if (Vector2.Distance(transform.position, targetPos) == 0)
        {
            targetPos = new Vector2(main.position.x, main.position.y);
        }
        if(FindObjectOfType<projectile>().isDead())
        {
            GetComponent<Enemy>().Die();
        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFacingLeft = !isFacingLeft;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public void bited()
    {
        targetPos = new Vector2(main.position.x + 2, main.position.y + 2);
        firstBite = false;
    }
}
