using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAI : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] enemies;
    private Rigidbody2D dummy; 
    private GameObject TargetEnemy;
    public float dummySpeed = 1;
    public float AttackDelay=0;
    public float jumpHeight = 1;
    private bool isLockedon = false;
    void Start()
    {
        dummy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AttackDelay > 0) { AttackDelay -= Time.deltaTime; }
        if (!isLockedon) { enemies = GameObject.FindGameObjectsWithTag("Enemy"); }
        if (enemies != null) { FindNearestEnemy(); }
        if (isLockedon) { MoveTowardsEnemy(); }
        //maybe just make dummy run to right if no enemies found  but then might fall off map dunno 
        
    }
    private void MoveTowardsEnemy()
    {
        if(TargetEnemy != null)
        {
            if (TargetEnemy.transform.position.x - transform.position.x > 0) { dummy.velocity = new Vector2(dummySpeed, dummy.velocity.y); } 
            else { dummy.velocity = new Vector2(-dummySpeed, dummy.velocity.y); }
            if (Mathf.Approximately(transform.position.x, transform.position.x) && dummy.velocity.y == 0)
            {

                dummy.velocity = new Vector2(dummy.velocity.x, jumpHeight);
            }
        }
        else { isLockedon = false; }
    }
    private void FindNearestEnemy()
    {
        foreach (var enemy in enemies)
        {
            if (WithinRange(enemy))
            {
                TargetEnemy = enemy;
                isLockedon = true;
                break;
            }
        }
    }
    private bool WithinRange(GameObject target)
    {
        if (target == null) return false;
        float LockOnDistance = 10;
        float distance = Vector2.Distance(target.transform.position, transform.position);
        return distance <= LockOnDistance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")&&AttackDelay<=0)
        {
            collision.gameObject.GetComponent<EnemyAI>().damageEnemy(5);
            AttackDelay = 2; 
            //Computer boutta die but get enemy gameobject and deal damage with delay
        }
    }
}
