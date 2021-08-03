using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bowen.Interactable
{
    public class Item : Interactable
    {
        public InventoryItem itemData;

        void Start()
        {
            //Checks to see if item is in database
            if (!ItemDatabase.instance.ContainsItem(itemData.itemName))
            {
                Debug.LogError(itemData.itemName + " not in database");
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }

        public override void Interact()
        {
            print("Added " + itemData.itemName);
            InventoryManager.instance.AddInventoryItem(itemData); //Sends the item to the inventory manager.
            GetComponentInParent<ObjectManager>().DisableObject();
        }

        // Update is called once per frame
        /*void Update()
        {
            if (byInteractable)
            {
                if (Input.GetButtonDown("Interact"))
                    Interact();
            }
        }*/
    }
}
