using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIGetItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerDownHandler
{
    public string itemId;
    public GameObject getItem;
    public GameObject UiItem;

    public bool needDestroy = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameInstance.Signal("item.add", UiItem);
        if(needDestroy) Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameInstance.Signal("cursor.exit", "interactive");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameInstance.Signal("cursor.enter", "interactive");
    }
}
