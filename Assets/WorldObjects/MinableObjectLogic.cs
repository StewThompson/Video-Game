using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Rock : MonoBehaviour
{
    /*
     * How This Works
     * Set Tag/Layer minableObjects
     * Collider2D use effector and include layers
     */
    private InventoryItemSystem logic;
    private bool isReady = false;
    private float timeToMine = 1;
    public int itemId;
    private Vector2 startSize;


    private void Start()
    {
        startSize = transform.localScale;
    }
    private void Update()
    {
        bool IsHolding =Input.GetKey(KeyCode.Mouse0);


        if (isReady && timeToMine <= 0 && IsHolding)
        {
            Debug.Log("Object Destroyed...Updating Inventory");
            logic = GameObject.Find("MainGameLogic").GetComponent<InventoryItemSystem>();
            if (logic != null) { logic.AddItem(itemId, 2, -1); }
            else { Debug.Log("Logic File not found"); }
            Destroy(gameObject);

        }
        else if (isReady&&timeToMine> 0 && IsHolding) { timeToMine -= Time.deltaTime;
            transform.localScale = new Vector2(transform.localScale.x - 0.25f * Time.deltaTime, transform.localScale.y - 0.25f * Time.deltaTime);
        }
        else if (isReady) { cancel(); }
    }

   /* private void Logic_RockReadToMine(object sender, IsReadyToBeMined e)
    {
        readyOrNot();
        if(e.IsReady && isReady) { Destroy(gameObject);
            logic.addMaterial();
        }
        
    }*/
    public void isClicked()
    {
        isReady = true;
        
    }
    public void cancel()
    {
        isReady = false;
        timeToMine = 2;
        transform.localScale = startSize;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
                
        
    }
}
