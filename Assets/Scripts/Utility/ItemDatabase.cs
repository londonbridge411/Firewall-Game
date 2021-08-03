using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    #region Singleton
    public static ItemDatabase instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        foreach (InventoryItem item in items)
        {
            strings.Add(item.itemName);
        }
    }
    #endregion

    public List<string> strings;
    public List<InventoryItem> items;

    public InventoryItem GetItem(string key)
    {
        int position = strings.FindIndex(x => x == key);
        return items[position];
    }

    public bool ContainsItem(string itemName) => strings.Contains(itemName);
}
