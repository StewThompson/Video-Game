using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    public MainCharacter _player;
    public Transform player;
    public float enenmySpeed = 5;
    public float jumpHeight = 10;
    public Rigidbody2D enemy;
    private float attackDelay = 0;
    private int health = 10;
    public MainGameLogic logic;
    private Animator animator;
   


    void Start()
    { 
        animator = GetComponent<Animator>();
        logic = GameObject.Find("MainGameLogic").GetComponent<MainGameLogic>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacter>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (damageAnimationTimer > 0)
        {
            damageAnimationTimer -= Time.deltaTime;
        }
        else if (damageAnimationTimer <= 0) { animator.SetBool("IsDamaged", false); }
        if (damageDelay > 0)
        {
            damageDelay -= Time.deltaTime;
        }

        if (checkPlayerDistance() > 2 && checkPlayerDistance()<=10)
        {

            if (player.position.x - transform.position.x > 0) { enemy.velocity = new Vector2(enenmySpeed, enemy.velocity.y); }
            else { enemy.velocity = new Vector2(-enenmySpeed, enemy.velocity.y); }
            if (Mathf.Approximately(transform.position.x, transform.position.x) && enemy.velocity.y == 0)
            {

                enemy.velocity = new Vector2(enemy.velocity.x, jumpHeight);
            }
        }
        else if (attackDelay < 0 && checkPlayerDistance() < 2)
        {
            _player.takeDamage(2);
            attackDelay = 2;
        }
        attackDelay -= Time.deltaTime;




    }
    private float checkPlayerDistance()
    {
        Vector2 playerPos = player.position;
        Vector2 enenemyPos = enemy.position;
        return Vector2.Distance(playerPos, enenemyPos);
    }
    
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Fireball"))
        {
            Debug.Log("Enemy Hit");
            damageEnemy(5);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == 3 && damageDelay<=0)
        {
            Debug.Log("Enemy Hit");
            damageEnemy(5);
            damageDelay = 1; 
        }
    }
    */
    private float damageAnimationTimer;
    private float damageDelay; 

    public void damageEnemy(int damage)
    {
        int HealthRemaining = health - damage;
        if (damageDelay<=0 && HealthRemaining > 0)
        {
            
            health = HealthRemaining;
            animator.SetBool("IsDamaged", true);
            damageAnimationTimer = 0.8f;
            damageDelay = 1;
        }
        else if(damageDelay>0) { damageDelay -=Time.deltaTime; }
        else
        {
            
            logic.scoreIncrease(1);
            Destroy(gameObject.transform.parent.gameObject); 
            
        }
    }
    
}

