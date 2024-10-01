using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class fire : MonoBehaviour
{
    public int FireballDamage;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject Hit = collision.gameObject;
        if (Hit.CompareTag("Ground"))
        {
            Destroy(collision.gameObject);
        }
        else if (Hit.CompareTag("Enemy"))
        {
            Hit.GetComponent<EnemyAI>().damageEnemy(FireballDamage);
        }
    }
}
