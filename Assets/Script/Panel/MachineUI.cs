using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;

public class MachineUI : MonoBehaviour
{
    public GameObject rMouth;
    public GameObject gMouth;
    public GameObject bMouth;

    public RectTransform rWater;
    public RectTransform gWater;
    public RectTransform bWater;
    private Vector2 rPos;
    private Vector2 gPos;
    private Vector2 bPos;

    public RectTransform W1;
    public RectTransform W2;
    public RectTransform W3;

    private bool isFirst = true;


    void Start()
    {
        rPos = rWater.position;
        gPos = gWater.position;
        bPos = gWater.position;
    }

    void OnEnable()
    {
        GameInstance.Connect("mouth.eat", OnMouthEat);
    }

    void OnDisable()
    {
        GameInstance.Disconnect("mouth.eat", OnMouthEat);
    }

    private void OnMouthEat(IMessage msg)
    {
        var id = (string)msg.Data;
        Debug.Log("enter + " +id);
        var obj = transform.Find("_" + id + "Mouth").gameObject;
        if(!gameObject.activeSelf) return;
        GameInstance.Instance.audioManager.PlayAudio(1);
        transform.Find("_" + id + "Mouth").gameObject.SetActive(false);
        Transform tf = transform.parent.Find("_Water/_" + id);
        if(isFirst)
        {
            tf.GetComponent<RectTransform>().DOAnchorPos(new Vector2(W2.position.x - 1053f, W2.position.y - 538.5f),0.75f);
            tf.DOScale(0.13f,0.75f);
            tf.gameObject.SetActive(true);
        }
        else
        {
            tf.GetComponent<RectTransform>().DOAnchorPos(new Vector2(W3.position.x - 1053f, W3.position.y - 538.5f),0.75f);
            tf.DOScale(0.22f,0.75f);
            tf.gameObject.SetActive(true);
        }
        
        CheckFusion();
    }

    private void CheckFusion()
    {
        foreach(Transform mouth in transform)
        {
            if(mouth.gameObject.activeSelf) return;
        }
        GameInstance.CallLater(1f, () => {
            rMouth.SetActive(true);
            gMouth.SetActive(true);
            bMouth.SetActive(true);
            GameInstance.Signal("condition2.unlock", "W");
            GameInstance.Signal("wlight.get");

            if(isFirst)
            {
                W2.gameObject.SetActive(true);
                W2.localScale = new Vector3(7f,7f,7f);
                W2.DOScale(1f, 2f).OnComplete(() => GameInstance.Instance.audioManager.PlayAudio(0));
                GameInstance.Instance.audioManager.PlayAudio(0);
            }
            else
            {
                W3.gameObject.SetActive(true);
                W3.localScale = new Vector3(7f,7f,7f);
                W3.DOScale(1.6f, 2f).OnComplete(() => GameInstance.Instance.audioManager.PlayAudio(0));;
                GameInstance.Instance.audioManager.PlayAudio(0);
            }

            rWater.gameObject.SetActive(false);
            rWater.localScale = new Vector3(0.1f,0.1f,0.1f);
            rWater.position = rPos;
            gWater.gameObject.SetActive(false);
            gWater.localScale = new Vector3(0.1f,0.1f,0.1f);
            gWater.position = gPos;
            bWater.gameObject.SetActive(false);
            bWater.localScale = new Vector3(0.1f,0.1f,0.1f);
            bWater.position = bPos;

            isFirst = false;
        });
    }
}
