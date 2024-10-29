using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Tool.Module.Message;
using UnityEngine;

public class End : MonoBehaviour
{
    public SpriteRenderer blackSprite;
    public GameObject eye;
    // Start is called before the first frame update
    void Start()
    {
        GameInstance.Connect("game.win", OnGameWin);
        gameObject.SetActive(false);
    }

    private void OnGameWin(IMessage msg)
    {
        gameObject.SetActive(true);
        blackSprite.DOFade(1f,1.5f).OnComplete(() =>{
            eye.gameObject.SetActive(true);
            transform.GetComponent<SpriteRenderer>().enabled = true;
            blackSprite.DOFade(0f,1.5f);
        });
        
    }
}
