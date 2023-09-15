using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public static bool canDrag = true;
    


    public Transform defaultParent;
    public void OnBeginDrag(PointerEventData eventData)
    {
        CardController card = GetComponent<CardController>();
        canDrag = true;
        if (card.model.canUse == false) // マナコストより少ないカードは動かせない
        {
            canDrag = false;
        }

        if (canDrag == false)
        {
            return;
        }

        defaultParent = transform.parent;
        transform.SetParent(defaultParent.parent,false);
        GetComponent<CanvasGroup>().blocksRaycasts = false;


    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag == false)
        {
            return;
        }

        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canDrag == false)
        {
            return;
        }

        transform.SetParent(defaultParent,false);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }

}
