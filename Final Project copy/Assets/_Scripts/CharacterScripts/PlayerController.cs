﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    HealthSystemForDummies healthSystem;
    PlayerHealth playerHealth;
    
    private PlayerAttack playerAttack;


    public bool jumping = false;
    public float movespeed = 5f;

    private bool isMovingRight = true;
    public float regenSpeed = 10f;
    private float nextRegen;

    public static PlayerController Player;

    void Awake()
    {
        if (Player != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Player = this;
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        healthSystem = GetComponent<HealthSystemForDummies>();
        playerHealth = GetComponent<PlayerHealth>();
        regenSpeed = MainManager.Instance.regenerationSpeed;
    }

    void Regenerate(){
        playerHealth.UpdateHealth(5);
    }

    void LateUpdate()
    {
        float hMove = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(hMove, 0f, 0f);
        Vector3 movement2 = new Vector3(Input.GetAxis("Vertical"), 0f, 0f);
        transform.position += movement * Time.deltaTime * movespeed;



        // If the input is moving the player right and the player is facing left...
        if (hMove > 0 && !isMovingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (hMove < 0 && isMovingRight)
        {
            // ... flip the player.
            Flip();
        }


        if (playerHealth.currentHealth < 100f && Time.time > nextRegen)
        {
            Regenerate();
            nextRegen = Time.time + regenSpeed;
        }

        if(transform.position.y <= -7f)
        {
            transform.position = new Vector3(-5f, 1f, 10f);
            playerHealth.Respawn();
        }

    }

    //Jumping Method adds force to the rigidbody
    public void Jump()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, 9f), ForceMode2D.Impulse);
    }  

    private void Flip()
    {
        /*
        // Switch the way the player is labelled as facing.
        isMovingRight = !isMovingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        transform.Rotate(0f,180f,0f);
        */
        // Switch the way the player is labelled as facing.
        isMovingRight = !isMovingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }



    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "pressurePlate")
        {
            //Debug.Log("On pressure plate");
            
        }
        if(col.gameObject.tag == "floor")
        {
            jumping = false;
            playerAttack.StopJumping();
        }
        if(col.gameObject.tag == "fireballPrefab")
        {
            Debug.Log("test");
        }
        if (col.gameObject.tag == "movingplatform")
        {
            jumping = false;
            playerAttack.StopJumping();
        }
    
    }

    

    

    
}
