using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Leaf : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseEnter()
    {
        var scale = transform.localScale;
        transform.DOShakeScale(0.1f, 0.1f).OnComplete(() => transform.localScale = scale);;
        
    }


    // void DrawOutline()
    // {
    //     Debug.Log("draw");
    // }

    // void RemoveOutline()
    // {

    // }

    void OnMouseDown()
    {
        transform.DOMoveY(-10f, 5f);
        Destroy(gameObject);
    } 
}
