using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderDamageScript : MonoBehaviour
{
    public int BoulderDamage;
    public float BoulderDamageDelay;
    private float BoulderDamageTimer;
    private void Update()
    {
        if (BoulderDamageTimer > 0) { BoulderDamageTimer -= Time.deltaTime; }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject.transform.gameObject;
        if (hit.CompareTag("Player") && BoulderDamageTimer <= 0)
        {
            BoulderDamageTimer = BoulderDamageDelay;
            Debug.Log("Player Hit By Boulder!");
            hit.GetComponent<MainCharacter>().takeDamage(BoulderDamage);
        }
    }
}
