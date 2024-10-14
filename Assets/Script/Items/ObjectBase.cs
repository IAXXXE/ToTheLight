using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{
    private Transform InteractionPoint;
    public event Action CallBack;

    protected void Awake()
    {
        InteractionPoint = transform.Find("_InteractionPoint");

        CallBack += TriggerEvent;
    }

    protected void OnDestroy()
    {
        CallBack -= TriggerEvent;
    }

    protected void TriggerEvent()
    {
        Debug.Log("call back ok!");
    }

    protected void OnMouseEnter()
    {
        GameInstance.Signal("cursor.enter", "interactive");
    } 

    protected void OnMouseExit()
    {
        GameInstance.Signal("cursor.exit", "interactive");
    }

    protected void OnMouseDown()
    {
        // GameInstance.Signal("player.do", InteractionPoint.position);
        var playerAction = new PlayerAction
        {
            pos = InteractionPoint.position,
            animId = "",
            callAction = CallBack
        };
        GameInstance.Signal("player.interact", playerAction);
    }
}
