using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour
{
    public GameObject Lightning;
    private GameObject[] LightningAll;
    public float LightningSpeed;
    public int LightningMaxDistance;
    private int LightningDistTraveled = 0;
    private float delay; 
    private float Seperation = 0;
    private float direction;
    // Start is called before the first frame update
    void Start()
    {
        LightningAll = new GameObject[LightningMaxDistance];
        LightningAll[0] = Instantiate(Lightning, new Vector2(transform.position.x + Seperation * direction, transform.position.y), Quaternion.identity);
        LightningDistTraveled++;
        if ((Input.mousePosition.x > 960))
        {

            direction = 1;
        }
        else
        {
            direction = -1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (delay <=0 && LightningDistTraveled != LightningMaxDistance)
        {
            LightningAll[LightningDistTraveled] = Instantiate(Lightning, new Vector2(transform.position.x + Seperation * direction, transform.position.y), Quaternion.identity);
            delay = LightningSpeed;
            LightningDistTraveled++;
            Seperation += 2;
        }
        else if (delay > 0) { delay -= Time.deltaTime; }
        else { foreach (GameObject l in LightningAll) { Destroy(l); } }
        /*if (delay<=0 && LightningDistance>0)
        {

            if (LastLight != null) { Destroy(LastLight); }
            LastLight = Instantiate(Lightning, new Vector2(transform.position.x + Seperation*direction, transform.position.y), Quaternion.identity);
            delay = LightningSpeed;
            LightningDistance--;
            Seperation += 2;
        }
        else if(delay>0) { delay -= Time.deltaTime; }
        else { Destroy(LastLight); LastLight = null; } */
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAI>().damageEnemy(2);
        }
    }
}
