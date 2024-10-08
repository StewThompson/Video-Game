using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Input = UnityEngine.Input;


public class MainCharacter : MonoBehaviour
{
    public float lateralMoveSpeed = 5;
    public float verticalMoveSpeed = 5;
    public Rigidbody2D myRigidBody;
    public GameObject[] dashPuff;
    private GameObject lastPuff;
   
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
    public float DashDelay;
    private float DashTimer;


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
    private void CreatePuff(int direction)
    {
        lastPuff = dashPuff[direction];
        lastPuff.SetActive(true);
        if (direction == 0) { lastPuff.transform.position =  new Vector2((transform.parent.position.x + 1.5f * transform.parent.localScale.x), transform.parent.position.y); }
        else { lastPuff.transform.position = new Vector2((transform.parent.position.x  - 1.5f * transform.parent.localScale.x), transform.parent.position.y); }
    }
    void Update()
    {
        //This update function is  a mess need to seperate it hard to understand
        // Vertical movement
        var inputX = Input.GetAxisRaw("Horizontal");
        if (!isDashing) { myRigidBody.velocity = new Vector2(inputX * lateralMoveSpeed, myRigidBody.velocity.y); }
        if (DashTimer <= 0 && Input.GetKey(KeyCode.Q))
        {
            StartCoroutine(SmoothDash(-1));
            if (transform.parent.localScale.x > 0) { CreatePuff(0);}//dashPuff[0].SetActive(true); 
            else { CreatePuff(1);}//dashPuff[1].SetActive(true); 
        }
        else if (DashTimer <= 0 && Input.GetKey(KeyCode.E))
        {
            StartCoroutine(SmoothDash(1));
            if (transform.parent.localScale.x > 0) { CreatePuff(1); }//dashPuff[1].SetActive(true);
            else { CreatePuff(0);  }//dashPuff[0].SetActive(true);
        }
        else if(DashTimer<=0 && Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(SmoothDash(2));
        }
        else if (DashTimer > 0)
        {
            DashTimer -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W) && isGrounded)

        {   
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, verticalMoveSpeed);
        }
        
    
        anime.SetBool("IsWalking", inputX != 0);
        anime.SetBool("IsInAir", !isGrounded);
        anime.SetBool("isDashing", isDashing);

        
        if(TeleportTimer<=0 && Input.GetKey(KeyCode.R)) {
            transform.parent.position = new Vector2(-105f, 4f);
            TeleportTimer = 5;
        }
        else if (TeleportTimer > 0) { TeleportTimer -= Time.deltaTime; }
        
        //mana regen
        manaRegen();
        HealthRegen();
        
    }
    public float DashDuration;
    public float AccelerationTime;
    public float DashSpeed;
    public float UpDashRatio;
    private bool isDashing;
    private IEnumerator SmoothDash(int direction)
    {
        isDashing = true;
        float timeElapsed = 0f;
        float currentSpeed = 0f;
        DashTimer =DashDelay;

        // Accelerate
        while (timeElapsed < AccelerationTime)
        {
            timeElapsed += Time.deltaTime;
            currentSpeed = Mathf.Lerp(0, DashSpeed, timeElapsed / AccelerationTime); // Smoothly increase speed
            if (direction < 2) { myRigidBody.velocity = new Vector2(direction * currentSpeed, myRigidBody.velocity.y); }
            else { myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, direction * currentSpeed/UpDashRatio); }
            yield return null;
        }

        // Maintain dash speed for the rest of the dash duration
        timeElapsed = 0f;
        while (timeElapsed < DashDuration - AccelerationTime)
        {
            timeElapsed += Time.deltaTime;
            if (direction < 2) { myRigidBody.velocity = new Vector2(direction * DashSpeed, myRigidBody.velocity.y); }
            else { myRigidBody.velocity = new Vector2(myRigidBody.velocity.x ,direction * DashSpeed / UpDashRatio); }
            yield return null;
        }

        // Stop dashing and return control
        //dashPuff[0].SetActive(false);
        //dashPuff[1].SetActive(false);
        lastPuff.SetActive(false);
        isDashing = false;
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
        Debug.Log("Player hit HP: "+ health);
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            
            isGrounded = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (Mathf.Abs(myRigidBody.velocity.y) > 1f && collision.gameObject.layer==6)
        {
            isGrounded = false;
        }
    }
}
