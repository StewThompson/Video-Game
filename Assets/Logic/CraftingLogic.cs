using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingLogic : MonoBehaviour
{
    InventoryItemSystem inventoryItemSystem;
    private void Start()
    {
        inventoryItemSystem = GetComponent<InventoryItemSystem>();
    }
    public void craftSword()
    {
        if (inventoryItemSystem.checkAmount(1) >= 2)
        {
            inventoryItemSystem.removeItem(1, 2);
            inventoryItemSystem.AddItem(3, 1,-1);
;        }
    }
}
