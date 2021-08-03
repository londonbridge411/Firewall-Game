using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using bowen.UI;

public class ItemMenu : Menu
{
    public GameObject infoBox;

    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {
        //InventoryGUI.instance.ResetInfo();
        infoBox.SetActive(false);
    }

    public override void Execute()
    {
        
    }

    public static ItemMenu instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

}
