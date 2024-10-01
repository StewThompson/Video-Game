using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] grass; 
    public GameObject spawner;
    public GameObject ground;
    public GameObject Tree;
    public GameObject bord;
    public GameObject earth;
    public GameObject enemy;
    public GameObject rock;
    private float last;
    private float outPutVal;
    private GameObject groundSpawner;
    private GameObject earthSpawner;
    public float groundLevel = -5;
    private int EnemySpawnChance;
    public bool SpawnEnemies;
    private ArrayList groundLevelAtPoint = new();
    public int BossSpawnChance;
    private int CurrentMapLength;



    public float ReturnGroundLevel(int level)
    {
        if (level < groundLevelAtPoint.Count) { return (float)(groundLevelAtPoint[level]); }
        return -999;
    }
    void Start()
    {
        for(int i = 0; i <= transform.position.x + 15; i++)
        {
            groundSpawner = Instantiate(ground, new Vector3(i - 10, groundLevel,transform.position.z),transform.rotation);
            groundSpawner.transform.parent = transform;
            groundLevelAtPoint.Add(groundLevel);
            for (int x = -10; x < groundLevel; x++)
            {
                earthSpawner = Instantiate(earth, new Vector3(i, x, transform.position.z), transform.rotation);
                earthSpawner.transform.parent=transform;
            }
        }
        last = transform.position.x;
        


    }
    private int lastTreeSpawned=0;
    private int lastSpawnerSpawned=0;
    // Update is called once per frame
    void Update()
    {
        CurrentMapLength = groundLevelAtPoint.Count;
        if (!SpawnedArenaOne) { FirstBossSpawner();}
        if (last - bord.transform.position.x < 15){ SpawnNextChunk();}

    }
    private void SpawnNextChunk()
    {
        int sinOrCosOrFlat = Random.Range(0, 3);
        for (float i = 0; i <= 25; i++)
        {
            int RandomGrass = Random.Range(0, grass.Length);
            if (sinOrCosOrFlat == 0)
            {
                outPutVal = Mathf.Round(1 * Mathf.Sin(i / 16));
                groundLevel += outPutVal;
            }
            else if (sinOrCosOrFlat == 1)
            {
                outPutVal = Mathf.Round(1 * Mathf.Cos(i / 16) - 1);
                groundLevel += outPutVal;
            }
            //else { outPutVal = i; }
            groundSpawner = Instantiate(ground, new Vector3(i + last, groundLevel, transform.position.z), transform.rotation);
            groundSpawner.transform.parent = transform;
            groundLevelAtPoint.Add(groundLevel);
            groundSpawner = Instantiate(ground, new Vector3(i + last, groundLevel + 12, transform.position.z), transform.rotation);
            groundSpawner.transform.parent = transform;


            groundSpawner = Instantiate(grass[RandomGrass], new Vector3(i + last, groundLevel + 0.5f, transform.position.z), transform.rotation);
            groundSpawner.transform.parent = transform;


            EnemySpawnChance = Random.Range(0, 21);
            //if(EnemySpawnChance == 0 && SpawnEnemies)
            //{
            //  Instantiate(enemy, new Vector3(i+last, groundLevel + 5, 0), transform.rotation);
            //}

            if (EnemySpawnChance == 9)
            {
                Instantiate(rock, new Vector3(i + last, groundLevel + 0.5f, 0), transform.rotation);
            }
            else if (EnemySpawnChance <= 8 && lastTreeSpawned == 0)
            {
                GameObject tree = Instantiate(Tree, new Vector3(i + last, groundLevel + 0.5f, 0), transform.rotation);
                float treeSize = Random.Range(1f, 3f);
                tree.transform.SetParent(transform);
                Vector3 treeVect = tree.transform.localScale;
                treeVect *= treeSize;
                tree.transform.localScale = treeVect;
                lastTreeSpawned = 3;
            }
            else if (lastTreeSpawned > 0) { lastTreeSpawned--; }
            if (EnemySpawnChance == 1 && lastSpawnerSpawned == 0 && SpawnEnemies)
            {
                Instantiate(spawner, new Vector3(i + last, groundLevel + 0.5f, 0), transform.rotation);
                lastSpawnerSpawned = 20;
            }
            else if (lastSpawnerSpawned > 0) { lastSpawnerSpawned--; }

            groundSpawner.transform.parent = transform;
            for (float x = groundLevel - 15; x < groundLevel; x++)
            {
                earthSpawner = Instantiate(earth, new Vector3(i + last, x + 27, transform.position.z), transform.rotation);
                earthSpawner.transform.parent = transform;
                earthSpawner = Instantiate(earth, new Vector3(i + last, x, transform.position.z), transform.rotation);
                earthSpawner.transform.parent = transform;
            }

        }

        last = last + 25;
    }
    public int MinimumFirstBossLoc;
    public int MaximumFirstBossLoc;
    public int ArenaOneSize;
    private bool SpawnedArenaOne;
    private void FirstBossSpawner()
    {
        if (CurrentMapLength > MinimumFirstBossLoc)
        {
            if (Random.Range(0, BossSpawnChance + 1) == BossSpawnChance) { SpawnArenaPlatform(); SpawnedArenaOne = true;  } 
        }
        else if(CurrentMapLength > MaximumFirstBossLoc) { SpawnArenaPlatform(); }
    }
    private void SpawnArenaPlatform()
    {
        Debug.Log("Spawning Arena Platform...");
        for (float i = 0; i <= ArenaOneSize; i++) {
            groundSpawner = Instantiate(ground, new Vector3(i + last, groundLevel, transform.position.z), transform.rotation);
            groundSpawner.transform.parent = transform;
            groundLevelAtPoint.Add(groundLevel);
            groundSpawner = Instantiate(ground, new Vector3(i + last, groundLevel + 12, transform.position.z), transform.rotation);
            groundSpawner.transform.parent = transform;
        }

        last = last + ArenaOneSize;
    }


}
