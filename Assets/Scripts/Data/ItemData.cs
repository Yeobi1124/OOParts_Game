using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Use, Quest, ETC }

    public ItemType Type;
    public string itemName;
    public int itemId;
    public int itemCount;
    public string itemDesc;
}
