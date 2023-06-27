using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    public Items item;

    public int slotIndex;
    
    public Image image;
    public Text sum;
    public GameObject itemInSlot;

    private void OnEnable()
    {
        image = gameObject.transform.Find("Item/ItemImage").GetComponent<Image>();
        sum = gameObject.transform.Find("Item/Sum").GetComponent<Text>();
        itemInSlot = gameObject.transform.Find("Item").gameObject;
    }


    public void SetSlot(Items item)
    {
        if(item == null)
        {
            itemInSlot.SetActive(false);
        }
        else
        {
            image.sprite = item.image;
            sum.text = item.sum.ToString();
        }
    }





}
