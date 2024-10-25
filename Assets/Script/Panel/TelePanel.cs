using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TelePanel : MonoBehaviour
{
    public RectTransform ImageTrans;
    
    void OnEnable()
    {
        ImageTrans.anchoredPosition = new Vector3(1039f, -90f, 0);
        ImageTrans.DOAnchorPos(new Vector2(1535f, -339f), 5f).SetEase(Ease.OutQuart);
    }
}
