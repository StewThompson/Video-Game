using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class MainCharacter : MonoBehaviour
{
    public float lateralMoveSpeed = 5;
    public float verticalMoveSpeed = 5;
    public Rigidbody2D myRigidBody;
   
    private int mana = 10;
    private float manaTimer;
    
    public event EventHandler<ManaStatus> ManaEvent;
    public event EventHandler<HealthStatus> HealthEvent;
    private Animator anime;
    public GroundSpawn grdspawner;
    private bool isGrounded;
    private int health = 10;
    private int totalHealth = 10;
    private float HealthRegenTimer = 5;
    private float ManaRegenTimer = 5;
    public GameObject EndScreen;
    public bool CanTakeDamage = true; 


    public class ManaStatus : EventArgs
    {
        public int ManaCount;
        
    }
    public int GetMana()
    {
        return mana;
    }

    public class HealthStatus : EventArgs
    {
        public int HealthCount;
        public int TotalHealth;
        public int CurrentHealth;
    }


    private void Start()
    {
        anime = GetComponent<Animator>();
    }
    void Update()
    {
        
       
        // Vertical movement
        var inputX = Input.GetAxisRaw("Horizontal");
        myRigidBody.velocity = new Vector2(inputX * lateralMoveSpeed, myRigidBody.velocity.y);
        
        if (Input.GetKey(KeyCode.W) && isGrounded)

        {
            
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, verticalMoveSpeed);
        }

        // Lateral movement
        if (inputX !=0)
        {
            

            transform.localScale = new Vector3(Mathf.Sign(inputX),1,1);
        }
    
        anime.SetBool("IsWalking", inputX != 0);
        anime.SetBool("IsInAir", !isGrounded);

        //attack
        
        if(TeleportTimer<=0 && Input.GetKey(KeyCode.R)) {
            transform.parent.position = new Vector2(-105f, 4f);
            TeleportTimer = 5;
        }
        else if (TeleportTimer > 0) { TeleportTimer -= Time.deltaTime; }
        
        //mana regen
        manaRegen();
        HealthRegen();
        
    }
    private float TeleportTimer; 
    private void HealthRegen()
    {
        if (health < 10 && HealthRegenTimer > 0)
        {
            HealthRegenTimer -= Time.deltaTime;

        }
        else if (health < 10 && HealthRegenTimer <= 0)
        {
            health++;
            Debug.Log("Health Regend to " + health);
            HealthEvent?.Invoke(this, new HealthStatus { HealthCount = 1, CurrentHealth=health,TotalHealth=totalHealth });
            HealthRegenTimer = 5;
        }
    }

    private void manaRegen()
    {
        if (mana < 10 && ManaRegenTimer > 0)
        {
            ManaRegenTimer -= Time.deltaTime;

        }
        else if (mana < 10 && ManaRegenTimer <= 0)
        {
            mana++;
            Debug.Log("Mana Regend to " + mana);
            ManaEvent?.Invoke(this, new ManaStatus { ManaCount = 1 });
            ManaRegenTimer = 5;
        }
    }
    public void useMana(int mana)
    {
        this.mana -= mana;
        ManaEvent?.Invoke(this, new ManaStatus { ManaCount = -mana });
    }
    public int getMana() { return mana; }
    
    
    public void takeDamage(int damage)
    {
        if(health!=0 && CanTakeDamage)
        {
            int healthRemaining = health - damage;
            HealthEvent?.Invoke(this, new HealthStatus { HealthCount = -damage,TotalHealth=totalHealth,CurrentHealth = health });
            if (healthRemaining > 0)
            {
                health = healthRemaining;
            }
            else
            {

                EndScreen.SetActive(true);
                gameObject.SetActive(false);

            }
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            isGrounded = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
