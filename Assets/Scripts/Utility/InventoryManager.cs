using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Singleton
    public static InventoryManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);
    }
    #endregion

    public List<InventoryItem> inventory;
    //public List<InventoryItem> characters; //Don't worry about just yet.
    private void Update()
    {

    }

    public void AddInventoryItem(InventoryItem item)
    {
            inventory.Add(item);
            InventoryGUI.instance.AddItem(item);
    }

    public void ReloadInventory()
    {
        foreach (InventoryItem item in inventory)
        {
            InventoryGUI.instance.AddItem(item);
        }
    }
}
