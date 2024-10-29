using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Finish : MonoBehaviour
{
    protected event Action CallBack;

    public Transform InteractionPoint;

    public SpriteRenderer sp;

    private bool isDone;

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
    }

    protected virtual void ExecuteAction()
    {
        var player = GameInstance.Instance.player;

        foreach(Transform white in player.transform.Find("_Sprite/_White"))
        {
            if(!white.gameObject.activeSelf)
            {
                GameInstance.Signal("player.say", "white");
                return;
            }
        }

        if(!gameObject.activeSelf) return;
        GameInstance.Signal("game.win");
        gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        var playerAction = GenerateAction();
        GameInstance.Signal("player.interact", playerAction);
       
    }

    void OnMouseEnter()
    {
        sp.color = new Color(1,1,1,0.2f);
    }

    void OnMouseExit()
    {
        sp.color = new Color(1,1,1,0.1f);
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
