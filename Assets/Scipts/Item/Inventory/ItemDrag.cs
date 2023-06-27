using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public Transform originalParent;
    public int originalSlotIndex;
    public Inventorys myBag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalSlotIndex = transform.parent.GetComponent<SlotController>().slotIndex;

        transform.position = eventData.position;
        transform.SetParent(transform.parent.parent,false);

        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(transform.position);
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if(eventData.pointerCurrentRaycast.gameObject == null)
        {
            transform.SetParent(originalParent);
            transform.position = originalParent.position;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }

        Transform currentTransform = eventData.pointerCurrentRaycast.gameObject.transform;


        if (currentTransform.name.Contains("ItemImage"))
        {
            transform.SetParent(currentTransform.parent.parent);
            transform.position = currentTransform.position;


            currentTransform.parent.SetParent(originalParent);
            currentTransform.parent.position = originalParent.position;

            Items temp = myBag.itemList[originalSlotIndex];
            myBag.itemList[originalSlotIndex] = myBag.itemList[currentTransform.parent.parent.GetComponent<SlotController>().slotIndex];
            myBag.itemList[currentTransform.parent.parent.GetComponent<SlotController>().slotIndex] = temp;

        }
        else if (currentTransform.name.Contains("Slot"))
        {
            if (currentTransform == originalParent)
            {
                transform.SetParent(originalParent);
                transform.position = originalParent.position;
            }
            else
            {

                Transform childTransform = currentTransform.Find("Item");


                childTransform.SetParent(originalParent);
                childTransform.position = originalParent.position;

                transform.SetParent(currentTransform);
                transform.position = currentTransform.position;


                Items temp = myBag.itemList[originalSlotIndex];
                myBag.itemList[originalSlotIndex] = myBag.itemList[currentTransform.GetComponent<SlotController>().slotIndex];
                myBag.itemList[currentTransform.GetComponent<SlotController>().slotIndex] = temp;


            }
        }
        else
        {
            transform.SetParent(originalParent);
            transform.position = originalParent.position;
        }




        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
