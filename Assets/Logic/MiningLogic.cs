using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MiningLogic : MonoBehaviour

{
    public GameObject player;
    public int itemId;
    private InventoryItemSystem logic;
    private Rock mineLogic;
    private bool isReady = false;
    private float timeToMine = 2;
    public event EventHandler<IsReadyToBeMined> RockReadToMine;
    public GameObject Rock=null;
    // Start is called before the first frame  update
    void Start()
    {
       Rock=null;
    }

    // Update is called once per frame
    void Update()
    {
    
       if(Rock != null) { mineLogic = Rock.gameObject.GetComponent<Rock>();
            mineLogic.isClicked();
       }
       if (Input.GetKeyDown(KeyCode.Mouse0)) { Debug.Log("Mining..."); mine(); }

    }

    private void mine() {
        //RockReadToMine?.Invoke(this, new IsReadyToBeMined {IsReady = true }) ;
        int maxMiningDistance = 10;
        Vector2 mousePos = Input.mousePosition;
        Vector2 actual = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 currentPos = player.transform.parent.position;
        Vector2 teleportPos = (actual - currentPos).normalized;
        getNextMinableRock(teleportPos, maxMiningDistance);
    }

    private void getNextMinableRock(Vector2 direction,int MaxMiningDistance)
    {
        Vector2 start = player.transform.parent.position;

        RaycastHit2D hit = Physics2D.Raycast(start, direction, MaxMiningDistance, LayerMask.GetMask("minableObjects"));
        Debug.DrawRay(start, direction * MaxMiningDistance, Color.red, 2.0f);

        if (hit.collider != null && hit.transform.gameObject.tag == "minableObjects")
        {
            Debug.Log($"Hit: {hit.collider.name} at distance: {hit.distance}");
            Rock = hit.transform.gameObject;
        }
        else
        {
            Debug.Log("No hit detected.");
        }

    }

    public void AssignRock()
    {
        Debug.Log("Rock clicked");
        Debug.Log("Rock Found!");
        mine();
    }

}

public class IsReadyToBeMined : EventArgs
{
    public bool IsReady;

}