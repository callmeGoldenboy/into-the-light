/*README
 * Playermovement script is taking care of player movements and there are 2 functions, run and jump
 * we call them in update as our frames get updated
 * when integrating you should change other scripts to communicate with PlayerMovement such as FollowMouse.cs (for facing)
 * the change i (Omid) made here is that i removed our previous parameters for animation and set all of them to boolean
 * 
 * VARIABLES TO CHANGE OR TWEAK in Stickman Script
 * 
 * Jump Force > the desired force for jumping
 * Speed > the desired speed
 * isJumping > are booleans which gets updated as the figure gets inputs in order for animations to react
 * isGrounded > are booleans which gets updated as the figure gets inputs in order for animations to react
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D player;
    private Animator animator;
    public float jumpForce;
    public float speed;
    public bool isJumping;
    public bool isGrounded;
    public bool AlmostGrounded;
    private bool facingLeft;
    public bool GetFacingLeft() { return facingLeft; }
    public void SetFacingLeft(bool b) { this.facingLeft = b; }

    private void Start()
    {
        GetPlayer();
    }

    private void GetPlayer()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Jump();
        Run();
        if (AlmostGrounded)
        {
            animator.SetBool("isLanding", true);
        }
        else
        {
            animator.SetBool("isLanding", false);
        }
        // updating the player velocity for better and faster movement down to the platform using gravity
        if (player.velocity.y < 0.2f)
        {   
            if(player.gravityScale < 5f) { player.gravityScale = player.gravityScale + 1; }
        }
        else
        {
            player.gravityScale = 2f; //default
        }

    }
    private void Jump()
    {
        animator.SetBool("isGrounded", isGrounded);
        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            gameObject.GetComponentInChildren<playerSoundManager>().Jumpb = true;
            animator.SetBool("isJumping", true);
            isJumping = true;
            player.velocity = Vector2.up * jumpForce;
        }
       else if (isJumping && player.velocity.y < 0)
        {
            isJumping = false;

        }
       
        animator.SetBool("isJumping", isJumping);
    }

    private void Run()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(moveInput * speed, player.velocity.y);

        if (Mathf.Abs(moveInput) > 0 && isGrounded == true)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isGrounded", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isGrounded", false);
        }
    }

}
