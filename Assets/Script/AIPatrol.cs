using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    bool mustPatrol = true;
    bool mustFlip = false;
    Rigidbody2D rgBody;
    public float speed = 4;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
   
    // Start is called before the first frame update
    void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
            Patrol();
    }
    private void FixedUpdate()
    {
        if (mustPatrol)
            mustFlip = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);

    }
    void Patrol()
    {
        if (mustFlip)
            Flip();
        rgBody.velocity = new Vector2(speed, 0);
    }
    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, 0);
        speed *= -1;
        mustPatrol = true;
    }
}
