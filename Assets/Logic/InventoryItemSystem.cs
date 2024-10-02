using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class item
{
    public int id;
    public string name;
    public int howMany;
    public int maxStack=2;
    public GameObject sprite;
    public Vector2 curentLocation;
    public Text label;
    public GameObject hotBarSprite;
    public Text hotBarLabel;
    
    public item(int id, string name,int howMany)
    {
        this.id = id;
        this.name = name;
        this.howMany = howMany;
        if (id == 3)
        {
            maxStack = 1;
        }
    }
}
public class InventoryItemSystem : MonoBehaviour
{
    
    public GameObject invetory;
    public GameObject hotbar;
    public GameObject mainCharacter;
    private Dictionary<int, item> itemInSlots = new Dictionary<int, item>
    {
        
        {1, null},
        {2, null},
        {3, null},
        {4, null},
        {5, null},
        {6, null},
        {7, null},
        {8, null},
        {9, null},
        {10, null},
        {11, null},
        {12, null},
        {13, null},
        {14, null},

        {15, null},
        {16, null},
        {17, null},
        {18, null},
        {19, null},
        {20, null},

        {21, null},
        {22, null},

        {23, null},

    };
    private Dictionary<int, Vector2> slotPositions = new Dictionary<int, Vector2> {
    {1, new Vector2(0.5f, 6.6f) },  // hotbar
    {2, new Vector2(1.4f, 6.6f) },  // hotbar
    {3, new Vector2(2.4f, 6.6f) },  // hotbar
    {4, new Vector2(3.4f, 6.6f) },  // hotbar
    {5, new Vector2(4.3f, 6.6f) },  // hotbar
    {6, new Vector2(4.3f, 5.4f) },  // Equipment
    {7, new Vector2(4.3f, 4.4f) },  // Equipment
    {8, new Vector2(4.3f, 3.4f) },  // Equipment
    {9, new Vector2(0.5f, 2.4f) },  // Inventory Row 1
    {10, new Vector2(1.4f, 2.4f) }, // Inventory Row 1
    {11, new Vector2(2.4f, 2.4f) }, // Inventory Row 1
    {12, new Vector2(3.4f, 2.4f) }, // Inventory Row 1
    {13, new Vector2(4.3f, 2.4f) }, // Inventory Row 1
    {14, new Vector2(0.5f, 1.5f) }, // Inventory Row 2
    {15, new Vector2(1.4f, 1.5f) }, // Inventory Row 2
    {16, new Vector2(2.4f, 1.5f) }, // Inventory Row 2
    {17, new Vector2(3.4f, 1.5f) }, // Inventory Row 2
    {18, new Vector2(4.3f, 1.5f) }, // Inventory Row 2
    {19, new Vector2(0.5f, 0.5f) }, // Inventory Row 3
    {20, new Vector2(1.4f, 0.5f) }, // Inventory Row 3
    {21, new Vector2(2.4f, 0.5f) }, // Inventory Row 3
    {22, new Vector2(3.4f, 0.5f) }, // Inventory Row 3
    {23, new Vector2(4.3f, 0.5f) }  // Inventory Row 3
};

    private Dictionary<int, int> totalNumberOfItems = new Dictionary<int, int> { {1,0},{ 2, 0 },{ 3, 0 } };
    private Dictionary<int, String> idToName = new Dictionary<int, string> {
        { 1, "Stone" },
        { 2, "Wood" },
        {3,"Sword" }
    }; //this should all be one dictionary idToStorageData w/ Array
    private Dictionary<int, float> idToScale = new Dictionary<int, float> {
        { 1, 1 },
        { 2, 1 },
        {3, 0.08f}
    };
    private Dictionary<int, float> idtoRotation = new Dictionary<int, float> {
        { 1, 0 },
        { 2, 0},
        {3, 135}
    };
    private Dictionary<int, Vector2> HotBarSlots = new Dictionary<int, Vector2> {
        { 1, new Vector2(-1.9f,-0.2f)},
        { 2, new Vector2(-.9f,-0.2f)},
        { 3, new Vector2(0f,-0.2f)},
        { 4, new Vector2(.94f,-0.2f)},
        { 5, new Vector2(1.9f,-0.2f)},

    };
    private Dictionary<int, Text> itemAmountTags = new Dictionary<int, Text>
    {
        
        {1, null},
        {2, null},
        {3, null},
        {4, null},
        {5, null},
        {6, null},
        {7, null},
        {8, null},
        {9, null},
        {10, null},
        {11, null},
        {12, null},
        {13, null},
        {14, null},

        {15, null},
        {16, null},
        {17, null},
        {18, null},
        {19, null},
        {20, null},

        {21, null},
        {22, null},
        {23, null},
    };
    private Dictionary<int, Text> hotBarTags = new Dictionary<int, Text>
    {

        {1, null},
        {2, null},
        {3, null},
        {4, null},
        {5, null},
       
    };
    public Transform[] pixelPosition /*= new Dictionary<int, Vector2> {
    {1, new Vector2((float)-2.7, (float)-0.1-3)},  // hotbar 55 0.5
    {2, new Vector2((float)-1.7,(float)-0.1-3) },  // hotbar
    {3, new Vector2((float).7, (float)-0.1-3) },  // hotbar
    {4, new Vector2((float)-1.7, (float)-0.1-3) },  // hotbar
    {5, new Vector2((float)-2.7, (float)-0.1-3) },  // hotbar
    {6, new Vector2((float)-2.7, (float)-2.15-3) },  // Equipment
    {7, new Vector2((float)-2.7, (float)-2.15-3) },  // Equipment
    {8, new Vector2((float)-2.15, 430) },  // Equipment
    {9, new Vector2(715, 306) },  // Inventory Row 1
    {10, new Vector2(812, 306) }, // Inventory Row 1
    {11, new Vector2(908, 306) }, // Inventory Row 1
    {12, new Vector2(1005, 306) }, // Inventory Row 1
    {13, new Vector2(1100, 306) }, // Inventory Row 1
    {14, new Vector2(715, 215) },  // Inventory Row 2
    {15, new Vector2(812, 215) }, // Inventory Row 2
    {16, new Vector2(908, 215) }, // Inventory Row 2
    {17, new Vector2(1005, 215) }, // Inventory Row 2
    {18, new Vector2(1100, 215) }, // Inventory Row 2
    {19, new Vector2(715, 120) },  // Inventory Row 3
    {20, new Vector2(812, 120) }, // Inventory Row 3
    {21, new Vector2(908, 120) }, // Inventory Row 3
    {22, new Vector2(1005, 120) }, // Inventory Row 3
    {23, new Vector2(1100, 120) }, // Inventory Row 3
        

    }*/;

    private MainGameLogic mainGameLogic; 
    private Resolution ScreenResolution;
    private GameObject grabbedItem;
    private int pickedUpObjectID;
    private GameObject player;

    private void Start()

    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainGameLogic=GetComponent<MainGameLogic>();
        for(int i = 1; i < 23; i++)
        {
            slotPositions[i] = new Vector2(slotPositions[i].x-2.4f, slotPositions[i].y-3.7f);
        }
        ScreenResolution = Screen.currentResolution;
        
    }
    private void Update()
    {

        if (!itemInAir && mainGameLogic.isInventoryOpen() && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Finding Item to pickup...");
            pickedUpObjectID = pickUpItem();
            if(pickedUpObjectID != -1) { grabbedItem = itemInSlots[pickedUpObjectID].sprite;
                Debug.Log($"{pickedUpObjectID}");
            }
          
        }
      else if(itemInAir && Input.GetMouseButtonDown(0))
        {
            itemInAir = false;
            int findNearestSlot = placeItem(pickedUpObjectID);
            if(findNearestSlot != -1 && findNearestSlot!=pickedUpObjectID) {
                AddItem(itemInSlots[pickedUpObjectID].id, itemInSlots[pickedUpObjectID].howMany, findNearestSlot);
                Destroy(itemInSlots[pickedUpObjectID].sprite);
                Destroy(itemInSlots[pickedUpObjectID].label);
                Destroy(itemInSlots[pickedUpObjectID].hotBarSprite);
                Destroy(itemInSlots[pickedUpObjectID].hotBarLabel);
                itemInSlots[pickedUpObjectID] = null; }
            else { itemInSlots[pickedUpObjectID].sprite.transform.localPosition = itemInSlots[pickedUpObjectID].curentLocation; }
        }
        if (itemInAir && grabbedItem != null)
        {
            grabbedItem.transform.localPosition = followMouse(grabbedItem);
        }
        else { itemInAir = false; }
    }

    //instead of this mousebullshit just find position of rock dum dum 
    private int placeItem(int id)
    {
        bool searchSucess=false;
        Vector2 nearestSlot = new Vector2(999,999);
        float shortestDistance = 9999;
        int slotPos = -1;
        for(int i = 1; i <= 23; i++)
        {
            if (itemInSlots[i] == null || itemInSlots[i].id == itemInSlots[id].id)
            {
                float dist = Vector2.Distance(slotPositions[i], itemInSlots[id].sprite.transform.localPosition);
                if(dist < shortestDistance) { nearestSlot = slotPositions[i]; slotPos = i; shortestDistance = dist; }
                searchSucess = true;
            }
           

        }
        if(!searchSucess)
        {
            Debug.Log("Search not a sucess");
            itemInSlots[id].sprite.transform.localPosition = itemInSlots[id].curentLocation;
        }
        Debug.Log("Placing item at" + slotPos);

        return slotPos;
    }
    private Vector2 followMouse(GameObject grabbedItem)
    {
        
        Vector2 mousePos = Input.mousePosition;
        /*float mouseActualX = 1920 * mousePos.x / ScreenResolution.width;
        float mouseActualY = 1080 * mousePos.y / ScreenResolution.height;
        float translateXToReal = (mouseActualX - 960) / 110;
        float translateYToReal = (mouseActualY - 540) / 90;*/
        Vector2 actual = Camera.main.ScreenToWorldPoint(mousePos);



        return actual;
       // return new Vector2(translateXToReal,translateYToReal+1);
    } 

    private bool itemInAir;
    private int pickUpItem()
    {
       Vector3 mousePosition = Input.mousePosition;
        int slot = findSlot(mousePosition);
        if (slot != -1 && itemInSlots[slot]!=null && itemInSlots[slot].sprite != null) { 
            itemInAir = true;
            return slot;
        }
        return -1;

    }
    public Canvas MyCanvas;
    public GameObject mainCamera;
    private int findSlot(Vector3 mousePosition)
    {
        //Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
        RectTransform canvasRect = MyCanvas.GetComponent<RectTransform>();

        // Convert the mouse position to local canvas space
        Vector3 worldPoint;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvasRect,
            mousePosition,
            Camera.main, // Pass the camera rendering the UI (if using world space canvas)
            out worldPoint
        );
        Debug.Log("WorldMousePos" + worldPoint);  
        float fixedDelta = 1;
        
        for (int i = 0; i <= 22; i++)
        {
            if (pixelPosition[i] != null)
            {
                
                Vector3 slotLocalPos = pixelPosition[i].localPosition; // Assuming it's a RectTransform
                float xDiff = slotLocalPos.x+mainCamera.transform.position.x - worldPoint.x;
                float yDiff = slotLocalPos.y + mainCamera.transform.position.y - worldPoint.y;
                if (i == 0) { Debug.Log("For i: " + i + " Xdiff: " + xDiff + " Ydiff: " + yDiff + " Pixel Position.x " + pixelPosition[i].localPosition.x); }
                else if (i == 1) { Debug.Log("For i: " + i + " Xdiff: " + xDiff + " Ydiff: " + yDiff + " Pixel Position.x " + pixelPosition[i].localPosition.x); }
                if ((-xDiff <= fixedDelta && xDiff > 0 ) && (yDiff <= fixedDelta && yDiff > 0))
                {
                    return i;
                }
            }
        }
        return -1;
    }
    public void AddItem(int id,int amount,int slot)
    {
       
        int firstSlot = firstAvailableSlot(id);
        if (slot != -1) { firstSlot = slot; }
        if (firstSlot == -1) { Debug.Log("Inventory Full"); }
        else
        {
            totalNumberOfItems[id] += amount;
            
            if (itemInSlots[firstSlot] == null)
            {
               
                itemInSlots[firstSlot] = new item(id, idToName[id], amount);

            }
            else { itemInSlots[firstSlot].howMany += amount; }
            Visualize(firstSlot);
               
        }
    }
    private void Visualize(int Slot)
    {
        String itemName = idToName[itemInSlots[Slot].id];
        Vector2 slotVector = slotPositions[Slot];
        float scale = idToScale[itemInSlots[Slot].id];
       
        GenerateSprite(slotVector, Slot, itemName,scale);
      
    }
    
    private void GenerateSprite(Vector2 slotVector, int Slot, String itemName,float scale)
    {
        if (itemInSlots[Slot].sprite == null) {
          
           
            GameObject sprite = (AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Items/" + itemName + ".prefab"));
            GameObject itemCreated = Instantiate(sprite);
            itemCreated.transform.SetParent(invetory.transform);
            itemCreated.transform.localPosition = slotVector;
            itemCreated.transform.localScale = new Vector3(scale,scale,scale);
            float Rotation = idtoRotation[itemInSlots[Slot].id];
            itemInSlots[Slot].sprite = itemCreated;
            itemInSlots[Slot].curentLocation = slotVector;
            GenerateLabel(slotVector, Slot, invetory);
            if (Slot <= 5)
            {
                Vector2 hotbarLocation = HotBarSlots[Slot];
                GameObject newItem = new GameObject();
                newItem = Instantiate(sprite);

                newItem.transform.SetParent(hotbar.transform);
                newItem.transform.localPosition = hotbarLocation;
                newItem.transform.localScale = new Vector3(scale,scale,scale);
                itemInSlots[Slot].hotBarSprite = newItem;
                

                GenerateLabel(hotbarLocation, Slot, hotbar);
            }

        }
        
    }
    private void GenerateLabel(Vector2 slotVector,int Slot,GameObject target)
    {
        if (itemAmountTags[Slot] == null)
        {
            GameObject amountItem = (AssetDatabase.LoadAssetAtPath<GameObject>("Assets/UI/AmountItem.prefab"));
            GameObject label = Instantiate(amountItem) as GameObject;
            label.transform.SetParent(target.transform);
            label.transform.localPosition = new Vector2(slotVector.x, slotVector.y - 0.15f);
            label.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            label.GetComponent<Text>().text = itemInSlots[Slot].howMany.ToString();
            itemAmountTags[Slot] = label.GetComponent<Text>();
            itemInSlots[Slot].label = label.GetComponent<Text>();
            if (Slot <= 5)
            {
                GameObject hotbarLabel = Instantiate(amountItem);
                hotbarLabel.transform.SetParent(hotbar.transform);
                hotbarLabel.transform.localPosition = new Vector2(HotBarSlots[Slot].x, HotBarSlots[Slot].y - 0.15f);
                hotbarLabel.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                hotbarLabel.GetComponent<Text>().text = itemInSlots[Slot].howMany.ToString();
                hotBarTags[Slot] = hotbarLabel.GetComponent<Text>();
                itemInSlots[Slot].hotBarLabel = hotbarLabel.GetComponent<Text>();



            }
        }
        else if(target==invetory)
        {
            Text label = itemAmountTags[Slot];
            label.text = itemInSlots[Slot].howMany.ToString();
        }
        else if(target == hotbar)
        {
            Text label = hotBarTags[Slot];
            label.text = itemInSlots[Slot].howMany.ToString();
        }
        
       
    }
    private int firstAvailableSlot(int id)
    {
        for(int i = 1; i <= 23; i++)
        {
            if (itemInSlots[i] == null || (itemInSlots[i].id == id && itemInSlots[i].howMany < itemInSlots[i].maxStack)) 
            {
                return i;
            }
        }
        return -1;
        
    }
    public int checkAmount(int id)
    {
        return totalNumberOfItems[id];
    }
    public item checkAmountInSlot(int slot)
    {
        return itemInSlots[slot] ;
    }
    public void removeItem(int id,int count) {
        if (totalNumberOfItems[id] >= count)
        {
            totalNumberOfItems[id] -= count;
            for(int i = 1; i < 23; i++)
            {
                if (itemInSlots[i]!=null && itemInSlots[i].id == id && itemInSlots[i].howMany>count)
                {
                    itemInSlots[i].howMany -= count;
                    itemInSlots[i].hotBarLabel.text = itemInSlots[i].howMany.ToString();
                    break;
                }
                else if (itemInSlots[i] != null && itemInSlots[i].id == id && itemInSlots[i].howMany <= count)
                {
                    Destroy(itemInSlots[i].sprite);
                    Destroy(itemInSlots[i].label);
                    if (itemInSlots[i].hotBarLabel != null)
                    {
                        Destroy(itemInSlots[i].hotBarLabel);
                        Destroy(itemInSlots[i].hotBarSprite);
                    }
                    count -= itemInSlots[i].howMany;
                    itemInSlots[i] = null;
                }
            }
        }
        else { Debug.Log("Not enough items to remove"); }
    }

    /*public void GenerateHotbar()
    {
        for(int i=1; i<=5;i++)
        {
            if (itemInSlots[i] != null)
            {
                Transform itemTransform = itemInSlots[i].sprite.transform;
                itemTransform.parent = hotbar.transform;
                itemTransform.localPosition = HotBarSlots[i];

                Transform labelTransform = itemInSlots[i].label.transform;
                labelTransform.parent = hotbar.transform;
                labelTransform.localPosition = new Vector2(HotBarSlots[i].x, HotBarSlots[i].y - 0.15f);
            }

        }
    }
    public void GenerateFirstFiveInventory()
    {
        for (int i = 1; i <= 5; i++)
        {
            if (itemInSlots[i] != null)
            {
                Transform itemTransform = itemInSlots[i].sprite.transform;
                itemTransform.parent = invetory.transform;
                itemTransform.localPosition = slotPositions[i];

                Transform labelTransform = itemInSlots[i].label.transform;
                labelTransform.parent = invetory.transform;
                itemTransform.localPosition = new Vector2(slotPositions[i].x, slotPositions[i].y - 0.15f);
            }
        }
    }*/
}
