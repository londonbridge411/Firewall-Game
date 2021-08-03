using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "[Insert item name]", menuName = "ScriptableObjects/Inventory Item")]
public class InventoryItem : ScriptableObject
{
    public GameObject itemObject;
    public string itemName;
    public int itemQuantity;
    public string itemLore;
}
