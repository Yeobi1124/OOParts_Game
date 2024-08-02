using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public ItemData[] items;
    private void Start()
    {
        for(int i =0; i < items.Length; i++)
        {
            GameManager.Instance.inventoryManager.Add(items[i]);
        }
    }
}
