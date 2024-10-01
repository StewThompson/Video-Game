using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LightningChain : MonoBehaviour
{
    public float selfDestructTimer = 1;
    public float LightningDistance;
    public GameObject lightning;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        selfDestructTimer-=Time.deltaTime;
        if(selfDestructTimer < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* if (collision.CompareTag("Enemy"))
         {
             Destroy(collision.GetComponent<GameObject>().gameObject);
             collision.gameObject.GetComponent<EnemyAI>().damageEnemy(2);
             GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
             foreach(GameObject enemy in enemies)
             {
                 if (withinRange(enemy))
                 {
                     Instantiate(lightning, new Vector2(enemy.transform.position.x + 1f, transform.position.y), Quaternion.identity);
                     break;
                 }

             }


         }
         else { Destroy(gameObject); } */
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAI>().damageEnemy(2);
        }

    }
    private bool withinRange(GameObject enemy)
    {
        float distance = Vector2.Distance(enemy.transform.position,transform.position);
        return distance <= 2;
    }
}
