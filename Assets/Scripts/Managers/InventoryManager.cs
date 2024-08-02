using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> items;
    public List<ItemData> inventory;
    public GameObject invenSet;
    public RectTransform gridSlot;
    public UseButton buttonSystem;
    public Button useButton;
    public TextMeshProUGUI itemDescription;

    private void Start()
    {
        items = new List<GameObject>();
        inventory = new List<ItemData>();
        invenSet.SetActive(false);
        useButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            invenSet.SetActive(!invenSet.activeSelf);
        }
    }
    
    public ItemData GetItem(ItemData item)
    {
        for(int i =0; i < inventory.Count; i++)
        {
            if(inventory[i].itemName == item.itemName)
            {
                return inventory[i];
            }
        }
        return null;
    }
    public void Add(ItemData item)
    {
        // Check if the item already exists in inventory
        for (int i = 0; i < inventory.Count; i++)
        {
            if (item.itemName == inventory[i].itemName)
            {
                inventory[i].itemCount++;
                items[i].GetComponent<Item>().UpdateUI();
                return;
            }
        }

        // If the item does not exist, create a new one
        GameObject prefab = GameManager.Instance.poolManager.Get(0);
        prefab.transform.SetParent(gridSlot, false);
        Item itemComponent = prefab.GetComponent<Item>();
        itemComponent.itemData = item;
        itemComponent.itemData.itemCount = 1;
        itemComponent.UpdateUI();

        inventory.Add(item);
        items.Add(prefab);
    }

    public bool Delete(ItemData item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (item.itemName == inventory[i].itemName)
            {
                if (inventory[i].itemCount > 1)
                {
                    inventory[i].itemCount--;
                    items[i].GetComponent<Item>().UpdateUI();
                    return true;
                }
                else
                {
                    // Deactivate and return it to the pool
                    for (int j = 0; j < items.Count; j++)
                    {
                        if (items[j].GetComponent<Item>().itemData == item)
                        {
                            items[j].SetActive(false);
                            items.RemoveAt(j);
                            break;
                        }
                    }

                    // Remove the item from inventory
                    inventory.RemoveAt(i);
                    return true;
                }
            }
        }
        return false;
    }
}
