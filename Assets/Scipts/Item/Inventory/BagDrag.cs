using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BagDrag : MonoBehaviour,IDragHandler
{
    RectTransform rect;

    private void OnEnable()
    {
        rect = gameObject.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0,0);
    }
    private void Start()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta;
    }



}
