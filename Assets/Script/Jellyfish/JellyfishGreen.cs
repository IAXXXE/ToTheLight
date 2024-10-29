using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using DG.Tweening;
using Tool.Module.Message;
using UnityEngine;

public class JellyfishGreen : MonoBehaviour
{
    //  wait follow eat make_fire
    public string stat;

    public Transform player;
    public float followSpeed = 5f;
    public Vector3 offset;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 2f;
    public Transform particle;

    public GameObject getItem;
    public GameObject UiItem;

    public Transform makeFirePos;

    protected Transform InteractionPoint;

    protected event Action CallBack;

    void Awake()
    {
        player = GameInstance.Instance.player.transform;
        particle = transform.Find("_Particle");
        InteractionPoint = transform.Find("_InteractionPoint");

        GameInstance.Connect("green.eat", OnEat);
        GameInstance.Connect("green.follow", OnFollow);
        GameInstance.Connect("fire.make", OnFireMake);
    }

    private void OnFireMake(IMessage msg)
    {
        transform.DOMove(makeFirePos.position, 1f);
        makeFirePos.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnFollow(IMessage msg)
    {
        stat = "follow";
        gameObject.SetActive(true);
    }

    private void OnEat(IMessage msg)
    {
        var pos = (Vector3)msg.Data;
        stat = "eat";
        transform.DOMove(pos,1f);
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
        particle.gameObject.SetActive(false);
        SetStat("follow");
        GameInstance.Signal("condition3.unlock", "getG");
        GameInstance.Instance.player.GetItem(getItem);
        GameInstance.CallLater(1f, () => GameInstance.Signal("item.add", UiItem)); 
        GameInstance.Instance.audioManager.PlayAudio(3);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(stat != "wait") return;
        if(collider.CompareTag("Laba"))
        {
            GameInstance.Signal("item.use","laba");
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
        if(stat == "follow")
        {
            Vector3 targetPosition = player.position + offset;

            float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

            transform.position = Vector3.Lerp(transform.position, new Vector3(targetPosition.x, targetPosition.y + yOffset, targetPosition.z), followSpeed * Time.deltaTime);
        }
        else if(stat == "wait")
        {
            float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = Vector3.Lerp(transform.position, new(transform.position.x, transform.position.y + yOffset), transform.position.z);
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
        // if(!interactable || !GameInstance.Instance.cursor.gameObject.activeSelf) return;
        // if(stat != "wait") return;
        // var playerAction = GenerateAction();
        // GameInstance.Signal("player.interact", playerAction);
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
