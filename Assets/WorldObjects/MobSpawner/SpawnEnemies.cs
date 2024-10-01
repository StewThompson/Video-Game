using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy;
    private GameObject player;
    public float spawnDelayMin;
    public float spawnDelayMax;
    private float spawnDelay;
    private int howManyEnemies;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        createSpawnDelay();


    }

    // Update is called once per frame
    void Update()
    {
        if (spawnDelay <=0 && isPlayerClose())
        {
            createHowManyEnemies();
            for(int i = 1; i <= howManyEnemies; i++)
            {
                Instantiate(enemy,new Vector2(transform.position.x+i,transform.position.y+1f),Quaternion.identity);
            }
            createSpawnDelay();
        }
        else if (spawnDelay>0) { spawnDelay-=Time.deltaTime;}
        
    }
    private Boolean isPlayerClose()
    {
        Vector2 spawnerPos = transform.position;
        Vector2 playerPos = player.transform.position;
       float delta = Vector2.Distance(spawnerPos, playerPos);
        if(delta<10) {
            return true;
        }
        return false;

    }
    private void createSpawnDelay()
    {
        spawnDelay=Random.Range(spawnDelayMin, spawnDelayMax);
    }
    private void createHowManyEnemies()
    {
        howManyEnemies = Random.Range(1, 4);
    }
}
