using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/New Item")]
public class Items : ScriptableObject
{
    public string itemName;
    public Sprite image;
    public int sum = 1;
    public bool isEnough = false;
}
