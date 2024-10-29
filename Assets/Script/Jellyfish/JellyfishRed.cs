using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using DG.Tweening;
using Tool.Module.Message;
using UnityEngine;

public class JellyfishRed : MonoBehaviour
{
    // cold hunger light powerful 
    public string stat = "cold";

    public GameObject fire;
    public Transform player;
    public float followSpeed = 2f;
    public Vector3 offset;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 1f;
    public Transform particle;
    public Transform movePos;

    public GameObject getItem;
    public GameObject UiItem;

    public Animator animator;

    protected Transform InteractionPoint;

    protected event Action CallBack;

    void Awake()
    {
        player = GameInstance.Instance.player.transform;
        particle = transform.Find("_Particle");
        InteractionPoint = transform.Find("_InteractionPoint");

        GameInstance.Connect("fire.on", OnHeating);
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
        ExecuteAction(); 
    }

    protected virtual void ExecuteAction()
    {
        if(stat == "hunger")
        {
            transform.DOShakeScale(0.5f, 1.2f);
            GameInstance.Signal("item.use","c_food");
            transform.Find("_hun").gameObject.SetActive(false);
            particle.gameObject.SetActive(true);

            stat = "light";
        }
        else if(stat == "light")
        {
            particle.gameObject.SetActive(false);
            // GameInstance.Signal("condition3.unlock", "getG");
            GameInstance.Instance.player.GetItem(getItem);
            GameInstance.CallLater(1f, () => GameInstance.Signal("item.add", UiItem));
            GameInstance.Signal("item.use","laba");
            stat = "powerful";
            GameInstance.Instance.audioManager.PlayAudio(3);

        }
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(stat == "hunger" && collider.CompareTag("Item") && collider.GetComponent<ItemBase>().itemId.Contains("c_food"))
        {
            TriggerAction();
        }
        else if(stat == "light" && collider.CompareTag("Laba"))
        {
            TriggerAction();
        }
    }

    protected virtual void TriggerAction()
    {
        var playerAction = GenerateAction();
        GameInstance.Signal("player.interact", playerAction);

    }

    private void Update()
    {
        if(stat == "float")
        {
            // Vector3 targetPosition = player.position + offset;
            player.position = transform.position + offset;
            // float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

            // transform.position = Vector3.Lerp(transform.position, new Vector3(targetPosition.x, targetPosition.y + yOffset, targetPosition.z), followSpeed * Time.deltaTime);
        }
    }

    public void SetStat(string statId)
    {
        stat = statId;
        if(stat == "wait")
        {
            particle.gameObject.SetActive(true);
        }
        if(stat == "follow")
        {
            transform.GetComponent<BoxCollider2D>().isTrigger = true;
        }

    }

    protected virtual void OnMouseDown()
    {
        if(stat == "powerful")
        {
            transform.DOShakeScale(0.5f, 1.5f);
            transform.Find("_Sprited").GetComponent<SpriteRenderer>().sortingOrder = 2;
            GameInstance.Signal("player.move", movePos.position);

            GameInstance.CallLater(1f, () =>
            {
                animator.enabled = true;
                GameInstance.Signal("camera.follow");
                stat = "float";
            });
            // GameInstance.Instance.player.transform.DOMove(new Vector3(-18, -3f, -9f),2f);
            // GameInstance.Signal("camera.move", new Vector3(-18f, 2.75f, -10));
            GameInstance.Signal("teleport.show", "teleport5");
            GameInstance.Signal("teleport.hide", "teleport2");
        }
    }

    public void OnAnimEnd()
    {
        animator.enabled = false;
        stat = "wait";
        GameInstance.Signal("camera.unfollow");
        GameInstance.Signal("camera.move", new Vector3(-18f,2.75f,-10));
        GameInstance.Signal("player.move", new Vector3(-20f, -3f, 0));
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

    public void OnHeating(IMessage msg)
    {
        GameInstance.CallLater(1f, () => {
            stat = "hunger";
            transform.Find("_cold").gameObject.SetActive(false);
            transform.Find("_hun").gameObject.SetActive(true);
            transform.Find("_Light").gameObject.SetActive(true);
            transform.DOShakeScale(1f, 1.1f);
        });
    }

    public void OnMouseEnter()
    {
        if(stat == "cold")
        {
            transform.Find("_cold").gameObject.SetActive(true);
        }
        if(stat == "hunger")
        {
            transform.Find("_hun").gameObject.SetActive(true);
        }

    }

    public void OnMouseExit()
    {
        if(stat == "cold")
        {
            transform.Find("_cold").gameObject.SetActive(false);
        }
        if(stat == "hunger")
        {
            transform.Find("_hun").gameObject.SetActive(false);
        }
    }

}
