using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) => AudioManager.instance.PlayOneShot("MenuClick");

    public void OnPointerEnter(PointerEventData eventData) => AudioManager.instance.PlayOneShot("MenuHover");

    public void ShowItem() => InventoryGUI.instance.ShowItem();
}
