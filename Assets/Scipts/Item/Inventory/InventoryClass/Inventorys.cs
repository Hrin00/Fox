using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory",menuName = "Inventory/New Inventory")]
public class Inventorys : ScriptableObject
{
    public List<Items> itemList = new List<Items>();
}
