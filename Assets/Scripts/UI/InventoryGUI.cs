using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryGUI : MonoBehaviour
{
    public static InventoryGUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Transform content;
    public GameObject emptyItemPrefab;

    public GameObject description;
    public GameObject itemName;
    public GameObject itemObject;

    public GameObject infoBox;
    public GameObject noItems;

    private void Update()
    {
        if (content.childCount != InventoryManager.instance.inventory.Count)
        {
            InventoryManager.instance.ReloadInventory();
        }

        if (content.transform.childCount == 0)
            noItems.SetActive(true);
        else
            noItems.SetActive(false);
    }

    public void AddItem(InventoryItem item)
    {
        GameObject newItem = Instantiate(emptyItemPrefab, content);
        newItem.GetComponent<TextMeshProUGUI>().text = item.itemName;
        newItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.type.ToString();
    }

    public void Clear()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    public void UpdateQuantity()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            content.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = InventoryManager.instance.inventory[i].type.ToString();
        }
    }

    public void ShowItem()
    {
        GameObject obj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        ResetInfo();


        description.GetComponent<TextMeshProUGUI>().text = ItemDatabase.instance.GetItem(obj.GetComponent<TextMeshProUGUI>().text).itemLore;
        itemName.GetComponent<TextMeshProUGUI>().text = obj.GetComponent<TextMeshProUGUI>().text;
        if (itemObject.transform.childCount < 1)
        {
            Instantiate(ItemDatabase.instance.GetItem(obj.GetComponent<TextMeshProUGUI>().text).itemObject, itemObject.transform);
        }
        infoBox.SetActive(true);
    }

    public void ResetInfo()
    {
        GameObject obj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        if (itemObject.transform.childCount == 1) // if ui object is shown
            Destroy(itemObject.transform.GetChild(0).gameObject);

        Instantiate(ItemDatabase.instance.GetItem(obj.GetComponent<TextMeshProUGUI>().text).itemObject, itemObject.transform);
    }

    public void CloseInfo()
    {
        description.GetComponent<TextMeshProUGUI>().text = string.Empty;
        itemName.GetComponent<TextMeshProUGUI>().text = string.Empty;
        if (itemObject.transform.childCount == 1)
            Destroy(itemObject.transform.GetChild(0).gameObject);
        infoBox.SetActive(false);
    }
}
