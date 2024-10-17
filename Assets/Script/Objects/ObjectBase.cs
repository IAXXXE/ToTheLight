using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectBase : MonoBehaviour
{
    protected bool interactable = true;
    protected bool mouseIn = false;

    protected GameObject sprite;
    protected GameObject sprited;
    protected Transform InteractionPoint;

    protected event Action CallBack;

    protected void Awake()
    {
        sprite = transform.Find("_Sprite").gameObject;
        sprited = transform.Find("_Sprited").gameObject;
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

    protected void TriggerEvent()
    {
        Debug.Log("call back!");

        ExecuteAction();
        
    }

    protected virtual void ExecuteAction()
    {
        
    }

    protected void OnMouseEnter()
    {
        mouseIn = true;
        GameInstance.Signal("cursor.enter", "interactive");
    } 

    protected void OnMouseExit()
    {
        mouseIn = false;
        GameInstance.Signal("cursor.exit", "interactive");
    }

    protected void OnMouseDown()
    {
        if(!interactable) return;

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
