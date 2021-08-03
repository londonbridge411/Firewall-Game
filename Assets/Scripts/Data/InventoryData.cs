using System;
using System.Collections;
using System.Collections.Generic;
using bowen.Saving;
using UnityEngine;

public class InventoryData : MonoBehaviour, ISaveable
{
    #region Singleton
    public static InventoryData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    static List<string> inventoryString = new List<string>();
    public List<string> inventoryStrings = new List<string>();

    public void AddStrings()
    {
        inventoryString.Clear();
        foreach (InventoryItem item in InventoryManager.instance.inventory)
        {
            inventoryString.Add(item.itemName);
        }
    }

    void LoadInventory()
    {

    }

    private void Update()
    {
        inventoryStrings = inventoryString;
    }

    public object CaptureState()
    {
        AddStrings();
        return new SaveData
        {
            inventoryString = inventoryString
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        inventoryString = saveData.inventoryString;
        InventoryManager.instance.inventory.Clear();
        InventoryGUI.instance.Clear();
        foreach (string s in inventoryString)
        {
            InventoryManager.instance.inventory.Add(ItemDatabase.instance.GetItem(s));
        }
    }

    [Serializable]
    private struct SaveData
    {
        public List<string> inventoryString;
    }
}
