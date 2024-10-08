using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class BossOneAI : MonoBehaviour
{
    public GameObject bossMinion;
    public GameObject bossAxe;
    private MeleeWeaponScript WeaponScript;
    private EnemyAI EnemyScript;
    private GameObject player;
    public float spawnDelayMin;
    public float spawnDelayMax;
    private float spawnDelay;
    private int howManyEnemies;

    
    // Start is called before the first frame update
    void Start()
    {
        AttackDelay = AttackDelayMax;
        WeaponScript = bossAxe.GetComponent<MeleeWeaponScript>();
        EnemyScript = GetComponent<EnemyAI>();
        player = GameObject.FindWithTag("Player");
        createSpawnDelay();
    }
    public int BossMeleeDamage;
    // Update is called once per frame
    void Update()
    {
        BossSummonMinion();
        BossAttack();
        DamagePlayer();
        CheckHealth();

    }
    public void CheckHealth()
    {
        if(EnemyScript.health <= 0)
        {
            SceneManager.LoadScene("End Screen");
        }
    }
    public float BossAttackRange; 
    public float BossAttackRangeMax; 
    private void BossSummonMinion()
    {
        if (spawnDelay <= 0 && isPlayerClose(BossAttackRangeMax))
        {
            createHowManyEnemies();
            for (int i = 1; i <= howManyEnemies; i++)
            {
                Instantiate(bossMinion, new Vector2(transform.position.x + i, transform.position.y + 1f), Quaternion.identity);
            }
            createSpawnDelay();
        }
        else if (spawnDelay > 0) { spawnDelay -= Time.deltaTime; }
    }
    private void BossAttack() {
        if (isPlayerClose(BossAttackRangeMax) && WeaponScript.CanAttack)
        {
            WeaponScript.StartSwing();
        }
    }
    private float AttackDelay;
    public float AttackDelayMax;
    private void DamagePlayer()
    {
        if (AttackDelay <= 0 && isPlayerClose(BossAttackRange) && WeaponScript.IsSwingingDown)
        {
            Debug.Log("Boss hit player");
            player.GetComponent<MainCharacter>().takeDamage(BossMeleeDamage);
            AttackDelay=AttackDelayMax;
        }
        else if (AttackDelay > 0)
        {
            AttackDelay-= Time.deltaTime;
        }
    }
    private Boolean isPlayerClose(float distance)
    {
        Vector2 spawnerPos = transform.position;
        Vector2 playerPos = player.transform.position;
        float delta = Vector2.Distance(spawnerPos, playerPos);
        if (delta < distance)
        {
            return true;
        }
        return false;

    }
    private void createSpawnDelay()
    {
        spawnDelay = Random.Range(spawnDelayMin, spawnDelayMax);
    }
    private void createHowManyEnemies()
    {
        howManyEnemies = Random.Range(1, 4);
    }
}
