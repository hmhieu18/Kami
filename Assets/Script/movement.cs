using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float damage = 3;
    public float speed = 1;
    public float jumpForce = 1;
    SpriteRenderer sprite;
    bool isGrounded = false;
    bool isDead = false;
    public Transform groundCheck;
    public Transform groundCheck1;
    public Transform groundCheck2;
    // Start is called before the first frame update

    private Rigidbody2D _rigidBody;
    private Animator animationController;
    void Start()
    {
        animationController = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isJump = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        print("groundCheck " + isJump);

        isJump |= Physics2D.Linecast(transform.position, groundCheck1.position, 1 << LayerMask.NameToLayer("Ground"));
        print("groundCheck1 " + isJump);
        isJump |= Physics2D.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground"));
        //print("groundCheck2 " + isJump);
        if (isJump)
        {
            isGrounded = true;
        }
        else
            isGrounded = false;


        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            _rigidBody.velocity = new Vector2(-speed, _rigidBody.velocity.y);
            sprite.flipX = true;
            if (isGrounded)
            {
                animationController.Play("run");
            }
        }
        else if (Input.GetKey("d") || Input.GetKey("right"))
        {
            _rigidBody.velocity = new Vector2(speed, _rigidBody.velocity.y);
            sprite.flipX = false;
            if (isGrounded)
            {
                animationController.Play("run");
            }
        }
        else
        {
            if (isGrounded)
                animationController.Play("idle");
        }
        if (Input.GetKey("space") && isGrounded)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);
            animationController.Play("jump");
        }
    }

    public void dead()
    {
        isDead = true;
        //Destroy(gameObject, 1);
        FindObjectOfType<LevelSetup>().Restart();
    }
}
