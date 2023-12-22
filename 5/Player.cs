//Player.cs
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 3f;
    public float DashSpeed = 6f;
    public float JumpForce = 15f;

    public LayerMask GroundLayer;

    private Rigidbody2D rb;

    private bool isDashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Player Movement
        if (!isDashing)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * MoveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * DashSpeed, rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }

        if (Input.GetButton("Dash"))
        {
            isDashing = true;
        }
        else
        {
            isDashing = false;
        }

        //Sprite Flip
        if(rb.velocity.x > 0)
        {
            GetComponent<Animator>().SetBool("flip", false);
        }
        else if (rb.velocity.x < 0)
        {
            GetComponent<Animator>().SetBool("flip", true);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        //Run
        if (rb.velocity.x != 0)
        {
            if (!isDashing)
            {
                GetComponent<Animator>().SetInteger("state", 1);
            }
            else
            {
                GetComponent<Animator>().SetInteger("state", 4);
            }
        }
        else
        {
            GetComponent<Animator>().SetInteger("state", 0);
        }

        //Jump / Fall
        if(rb.velocity.y > 0.1f)
        {
            GetComponent<Animator>().SetInteger("state", 2);
        }
        else if(rb.velocity.y < -0.1f)
        {
            GetComponent<Animator>().SetInteger("state", 3);
        }
    }

    private bool isGrounded()
    {
        BoxCollider2D c = GetComponent<BoxCollider2D>();
        return Physics2D.BoxCast(c.bounds.center, c.bounds.size, 0f, Vector2.down, .1f, GroundLayer);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InstaDeath"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}