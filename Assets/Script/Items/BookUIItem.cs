using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BookUIItem : ItemBaseUI, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameInstance.Signal("panel.show", "Book");
    }
}
