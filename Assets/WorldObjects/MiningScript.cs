using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningScript : MonoBehaviour
{
    private InventoryItemSystem logic;
    private bool isReady = false;
    private float timeToMine = 2;
    //private Animator animator;


    private void Start()
    {

    }
    private void Update()
    {
        if (isReady && timeToMine <= 0)
        {
            Debug.Log("Object Destroyed...Updating Inventory");
            logic = GameObject.Find("MainGameLogic").GetComponent<InventoryItemSystem>();
            if (logic != null) { logic.AddItem(2, 2, -1); }
            else { Debug.Log("Logic File not found"); }
            Destroy(gameObject);

        }
        else if (isReady && timeToMine > 0) {
            transform.localScale = new Vector2(transform.localScale.x - 0.25f*Time.deltaTime, transform.localScale.y - 0.25f* Time.deltaTime);
            timeToMine -= Time.deltaTime; }
    }

    public void readyOrNot()
    {
        logic = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryItemSystem>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Transform playerPositin = player.transform;
        if (Vector3.Distance(playerPositin.position, transform.position) < 5)
            isReady = true;

    }
    public void isClicked()
    {

        readyOrNot();
        timeToMine = 2;
       // animator = GetComponent<Animator>();
       // animator.SetBool("Isbreaking", true);
    }
    public void cancel()
    {
        isReady = false;
      //  animator.SetBool("Isbreaking", false);
    }
}
