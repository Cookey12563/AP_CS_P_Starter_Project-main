using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    float speed = 4;
    float xVector;
    float yVector;
    float yDirection;
    float xDirection;
    bool cooldown;
    Animator animator;
    SpriteRenderer renderer;

    int coins = 0;
    int health = 3;
    String facing;
    int maxHealth = 10;

    private void Start()
    {
     animator = GetComponentInChildren<Animator>(); 
     renderer = GetComponentInChildren<SpriteRenderer>(); 
    }

    private void Update()
    {
        // get input
       xDirection = Input.GetAxisRaw("Horizontal");
       yDirection = Input.GetAxisRaw("Vertical");
       //apply movment using vector
       xVector = xDirection * speed * Time.deltaTime;
       yVector = yDirection * speed * Time.deltaTime;
       transform.position += new Vector3 (xVector, yVector, 0);
       AninationUpdate();
    }
    private void AninationUpdate()
    {
        bool ismoving = xVector != 0 || yVector != 0;
        animator.SetBool("IsWalking", ismoving);
        if (Mathf.Abs(xVector) > Mathf.Abs(yVector))
        {
            animator.SetInteger ("WalkDir",1);
        }
        if (xVector > 0)
        {
            renderer.flipX = true;
        }
        else if (xVector < 0)
        {
            renderer.flipX = false;
        }
        else if (yVector > 0)
        {
            animator.SetInteger ("WalkDir",0);
        }
        else if (yVector < 0)
        {
            animator.SetInteger ("WalkDir",2);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            ChangeCoins(1);
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Health_Potion"))
        {
            ChangeHealth(1);
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("E_Fireball"))
        {
            ChangeHealth(-1);
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Spikes"))
        {
             ChangeHealth(-1);
        }
        
    }

    void ChangeHealth(int amount)
    {
        health += amount;
        if(health<1)
        {
            Die();
        }
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        print("Health: " + health);
    }

    void ChangeCoins(int amount)
    {
        coins += amount;
        print("Coins: " + coins);
    }

    void Die()
    {
        print("you died");
    }


}