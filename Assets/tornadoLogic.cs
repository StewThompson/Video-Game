using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class tornadoLogic : MonoBehaviour
{
    public float Knockback;
    private Rigidbody2D rb;
    public float tornadoDistance;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        tornadoDistance-= Time.deltaTime;
        if(tornadoDistance < 0) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hit = collision.transform.gameObject;
        bool isEnemy = hit.CompareTag("Enemy");
        bool isGround = hit.layer == 6;
            if (isEnemy)
            {
                Debug.Log("Enemy hit by nado");
                hit.GetComponent<EnemyAI>().damageEnemy(2);
                Vector2 tornadoPosition = GetComponent<Rigidbody2D>().position;
                collision.transform.gameObject.GetComponent<Rigidbody2D>().position = new Vector2(Mathf.Sign(rb.velocity.x)*Knockback+tornadoPosition.x, tornadoPosition.y);
            }
            else if (isGround) { Destroy(gameObject); }
    }
}
