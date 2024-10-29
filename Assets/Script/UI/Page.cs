using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Page : MonoBehaviour, IPointerClickHandler
{
    public Vector2 initPos;
    public Vector2 outPos;

    public RectTransform rectTransform;

    private int clickTimes = 0;


    void OnEnable()
    {
        rectTransform.position = initPos;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clickTimes ++;
        rectTransform.DORotate(new Vector3(0,0,-5f), 0.1f).OnComplete(() => {
            rectTransform.DORotate(new Vector3(0,0,5f), 0.1f);
        });
        rectTransform.DORotate(new Vector3(0,0,0f), 0.2f).SetDelay(0.2f);

        if(clickTimes >= 10)
        {
            rectTransform.DOAnchorPos(outPos, 0.5f);
        }

        GameInstance.Signal("unlock.page");
    }
}
