using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreboard;
    public Image exp;
    private int currentScore = 0;
    public GameObject spawnEnemy;
    public GameObject player;
    public GameObject hotBar;
    public GameObject Inventory;
    public GameObject Sword;
    private InventoryItemSystem inventoryItemSystem;
    private int hotBarSelected = 1;
    public event EventHandler<LevelStats> LevelUp;

    public class LevelStats : EventArgs
    {
        public int level;
    }
    void Start()
    {

        inventoryItemSystem = GetComponent<InventoryItemSystem>();

    }

    public void scoreIncrease(int score)
    {
        float AdjustedScore = (float)score / 2;
        
        if(exp.fillAmount + AdjustedScore >= 1) 
        { currentScore++; 
            LevelUp?.Invoke(this, new LevelStats { level=currentScore}); 
            exp.fillAmount = (exp.fillAmount + AdjustedScore) -1; }
        else { exp.fillAmount += AdjustedScore; }
        scoreboard.text = currentScore.ToString();
        
    }

    public void restartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    
    private bool inventoryOpened;
    private float swingDelay = 1;

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            hotBarSelected = 1; 
        }
        else if (Input.GetKey(KeyCode.Alpha2)) { hotBarSelected = 2;}
        else if(Input.GetKey(KeyCode.Alpha3)) { hotBarSelected = 3; }
        else if(Input.GetKey(KeyCode.Alpha4)) { hotBarSelected = 4;}
        else if (Input.GetKey(KeyCode.Alpha5)) { hotBarSelected = 5;}
     
       if(Input.GetKey(KeyCode.I)) {
            
            CloseOpenInventory(false);

        }
        if (inventoryOpened && (Input.GetKey(KeyCode.Escape))) { 
            CloseOpenInventory(true);

        }
        if (inventoryItemSystem.checkAmountInSlot(hotBarSelected)!= null && inventoryItemSystem.checkAmountInSlot(hotBarSelected).id == 3 
            &&!inventoryOpened && Input.GetMouseButtonDown(0) && swingDelay<0) 
        {
            Transform transform = player.GetComponent<Transform>();
            GameObject swordPrefab = (AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Sword.prefab"));
            GameObject SwingingSword = Instantiate(swordPrefab,new Vector3(6.12f, -2.36f, 0f), Quaternion.identity);
            SwingingSword.transform.SetParent(transform); 
            SwordLogic swordlogic = SwingingSword.GetComponent<SwordLogic>();
            swordlogic.Swing();
            swingDelay = 1;

        }
        else if(swingDelay>0) { swingDelay -= Time.deltaTime; }
    }

    private void CloseOpenInventory(bool interact)
    {
        hotBar.SetActive(interact);
        Inventory.SetActive(!(interact));
        inventoryOpened = !(interact);
        if(inventoryOpened)
        {
            //inventoryItemSystem.GenerateFirstFiveInventory();
        }
        else
        {
            //inventoryItemSystem.GenerateHotbar();
        }
    }
    public bool isInventoryOpen()
    {
        return inventoryOpened;
    }

    
}
