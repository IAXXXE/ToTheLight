using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class ObjectBase : MonoBehaviour
{
    public string id = "object";

    protected bool interactable = true;
    protected bool mouseIn = false;

    protected GameObject sprite;
    protected GameObject sprited;
    protected Transform InteractionPoint;

    protected event Action CallBack;

    protected void Awake()
    {
        sprite = transform.Find("_Sprite")?.gameObject;
        sprited = transform.Find("_Sprited")?.gameObject;
        InteractionPoint = transform.Find("_InteractionPoint");
    }

    protected void OnEnable()
    {
        CallBack += TriggerEvent;
    }

    protected void OnDisEnable()
    {
        CallBack -= TriggerEvent;
    }

    protected void OnDestroy()
    {
        CallBack -= TriggerEvent;
    }

    protected void TriggerEvent()
    {
        if(!gameObject.activeSelf) return;
        ExecuteAction();
        GameInstance.Instance.audioManager.PlayAudio(1);
    }

    protected virtual void ExecuteAction()
    {
        // if(id == "grass") return;
        // var scale = transform.localScale;
        // transform.DOShakeScale(0.1f, 0.5f).OnComplete(() => transform.localScale = scale);
    }

    protected virtual void OnMouseEnter()
    {
        if(!interactable) return;
        mouseIn = true;
        // GameInstance.Signal("cursor.enter", "interactive");
        var scale = transform.localScale;
        transform.DOShakeScale(0.1f, 0.1f).OnComplete(() => transform.localScale = scale);;

    } 

    protected virtual void OnMouseExit()
    {
        if(!interactable) return;
        mouseIn = false;
        // GameInstance.Signal("cursor.exit", "interactive");
      
    }

    protected virtual void OnMouseDown()
    {
        if(!interactable || GameInstance.Instance.cursor.isPanel) return;

        var playerAction = GenerateAction();
        GameInstance.Signal("player.interact", playerAction);
    }

    protected virtual PlayerAction GenerateAction()
    {
        var playerAction = new PlayerAction
        {
            pos = InteractionPoint.position,
            animId = null,
            callAction = CallBack
        };

        return playerAction;
    }
}
