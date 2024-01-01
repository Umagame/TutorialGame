//Player.cs
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 3f;
    public float DashSpeed = 6f;
    public float JumpForce = 17f;

    public AudioSource JumpSE;
    public AudioSource BreakSE;
    public AudioSource MoneySE;

    public LayerMask GroundLayer;

    public GameObject MenuPanel;
    public GameObject GameoverPanel;

    private Rigidbody2D rb;

    private bool isDashing = false;

    public int Money = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Time.timeScale != 0)
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
                JumpSE.Play();
            }

            if (Input.GetButton("Dash"))
            {
                isDashing = true;
            }
            else
            {
                isDashing = false;
            }
        }

        if (Input.GetButtonDown("Escape"))
        {
            if (MenuPanel.activeSelf)
            {
                MenuPanel.SetActive(false);
                Time.timeScale = 1.0f;
            }
            else
            {
                MenuPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }

        //Sprite Flip
        if (rb.velocity.x > 0)
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
        if (rb.velocity.y > 0.1f)
        {
            GetComponent<Animator>().SetInteger("state", 2);
        }
        else if (rb.velocity.y < -0.1f)
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
        GameObject obj = collision.gameObject;

        //InstaDeath
        if (obj.CompareTag("InstaDeath"))
        {
            GameoverPanel.SetActive(true);
            Time.timeScale = 0;
        }

        //Breakable
        BoxCollider2D c = GetComponent<BoxCollider2D>();
        if (obj.CompareTag("Breakable") && rb.velocity.y == 0 &&
            obj.transform.position.y - obj.GetComponent<Collider2D>().bounds.size.y / 2 >= this.transform.position.y)
        {
            GameObject.Destroy(collision.gameObject);
            BreakSE.Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        //Collectable
        if (obj.GetComponent<Collectable>() != null)
        {
            if(obj.GetComponent<Collectable>().ID == "money")
            {
                Money++;
                GameObject.Destroy(obj);
                MoneySE.Play();
            }
        }
    }
}