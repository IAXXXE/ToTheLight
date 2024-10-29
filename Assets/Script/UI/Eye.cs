using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Eye : MonoBehaviour
{
    public Image image;

    // Update is called once per frame
    void Start()
    {
        image.DOFade(0f, 2f).SetLoops(10);
    }
}
