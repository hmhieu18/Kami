using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    // Start is called before the first frame update
    Transform main;
    bool isMovable;
    Vector2 targetPos;
    public float speed = 4;
    public bool isFacingLeft = false;

    public bool firstBite = true;
    void Start()
    {
        isMovable = false;
        main = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        //moveToTarget(3);
        if (isMovable)
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
    public void mainReached()
    {
        Debug.Log("CROW MOVING");
        isMovable = true;
    }
    public void bited()
    {
        targetPos = new Vector2(main.position.x + 2, main.position.y + 2);
        firstBite = false;
    }
}
