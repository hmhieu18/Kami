using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;


    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool isDead = false;
    bool canMove = true;
    float nextAttackTime = 0f;
    bool isGrounded;



    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            nextAttackTime = Time.time + 0.2f;
            //animator.SetTrigger("attacking");
        }
        isGrounded = GetComponent<CharacterController2D>().isGrounded();

        if (Time.time < nextAttackTime && isGrounded)
            horizontalMove = 0;

    }


    public void OnLanding()
    {
        // Debug.Log("Landed");
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
    public void dead()
    {
        isDead = true;
        FindObjectOfType<LevelSetup>().Restart();
    }
    public void StepAudio()
	{
		//Tell the Audio Manager to play a footstep sound
		AudioManager.PlayFootstepAudio();
	}

    public void HurtAudio()
	{
		//Tell the Audio Manager to play a hurt sound
		AudioManager.PlayDeathAudio();
	}
}