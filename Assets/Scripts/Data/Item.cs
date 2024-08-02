using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemCount;

    public void UpdateUI()
    {
        itemName.text = itemData.itemName;
        itemCount.text = itemData.itemCount.ToString();
    }
    public void Select()
    {
        GameManager.Instance.inventoryManager.itemDescription.text = itemData.itemDesc;
        GameManager.Instance.inventoryManager.useButton.gameObject.SetActive(itemData.Type == ItemData.ItemType.Use);
        GameManager.Instance.inventoryManager.buttonSystem.itemData = itemData;
    }

    
}
