using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIGetItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string itemId;
    public GameObject getItem;
    public GameObject UiItem;

    public bool needDestroy = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameInstance.Signal("item.add", UiItem);
        GameInstance.Signal("condition1.unlock", itemId);
        GameInstance.Instance.audioManager.PlayAudio(1);
        if(itemId == "W")
            GameInstance.Signal("wlight.get");
        if(needDestroy) Destroy(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameInstance.Signal("cursor.exit", "interactive");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var scale = GetComponent<RectTransform>().localScale;
        GetComponent<RectTransform>().DOShakeScale(0.1f, 0.1f).OnComplete(() => GetComponent<RectTransform>().localScale = scale);
        GameInstance.Signal("cursor.enter", "interactive");
    }
}
