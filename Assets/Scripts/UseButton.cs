using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseButton : MonoBehaviour
{
    public ItemData itemData;

    public void Use()
    {
        GameManager.Instance.inventoryManager.Delete(itemData);
        GameManager.Instance.inventoryManager.itemDescription.text = "";
        GameManager.Instance.inventoryManager.useButton.gameObject.SetActive(false);
    }
}
