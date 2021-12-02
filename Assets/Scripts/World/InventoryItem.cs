using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "[Insert item name]", menuName = "ScriptableObjects/Inventory Item")]
public class InventoryItem : ScriptableObject
{
    public enum ItemType
    {
        Collectible, Weapon, KeyItem, Misc
    }

    public GameObject itemObject;
    public string itemName;
    public ItemType type;
    public string itemLore;
}
