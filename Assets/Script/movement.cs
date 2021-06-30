using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed = 1;
    public float jumpForce = 1;
    SpriteRenderer sprite;
    bool isGrounded = false;
    bool isDead = false;
    public Transform groundCheck;
    // Start is called before the first frame update

    private Rigidbody2D _rigidBody;
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
            isGrounded = false;

        
        if(Input.GetKey("a") || Input.GetKey("left"))
        {
            _rigidBody.velocity = new Vector2(-speed, _rigidBody.velocity.y);
            sprite.flipX = true;
        }
        else if(Input.GetKey("d") || Input.GetKey("right"))
        {
            _rigidBody.velocity = new Vector2(speed, _rigidBody.velocity.y);
            sprite.flipX = false;
        }
        if(Input.GetKey("space") && isGrounded)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);
        }
    }

    public void dead()
    {
        isDead = true;
        FindObjectOfType<LevelSetup>().Restart();
    }
}
