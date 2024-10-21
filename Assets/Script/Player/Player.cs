using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
using Tool.Module.Message;
using System;
using System.Collections;
using Unity.VisualScripting;
using System.Drawing.Printing;

public class PlayerAction
{
    public Vector3 pos;
    public string animId;
    public Action callAction;
}

public class Player : MonoBehaviour
{
    public float moveSpeed;

    private string stat;
    private List<string> statList = new();

    private Seeker seeker;
    private List<Vector3> pathPointList;
    private int currIndex = 0;
    private bool isRight = true;

    private Animator animator;

    private float itemOffsetY = 1.5f;

    void Awake()
    {
        seeker = GetComponent<Seeker>();

        animator = transform.Find("_Sprite").GetComponent<Animator>();

        GameInstance.Connect("player.move", OnPlayerMove);
        GameInstance.Connect("player.interact", OnPlayerInteract);
    }

    void OnDestroy()
    {
        GameInstance.Disconnect("player.move", OnPlayerMove);
        GameInstance.Disconnect("player.interact", OnPlayerInteract);
    }

    void Update()
    {
        if(stat == "move")
            Move();
    }

    private void OnPlayerInteract(IMessage msg)
    {
        var data = (PlayerAction)msg.Data;

        if(data.pos != null)
        {
            statList.Add("move");
            var target = data.pos;
            PreMove(target);
        }
        if(data.animId != null)
        {
            statList.Add("anim");
            StartCoroutine(WaitToAnim(data.animId));
        }
        if(data.callAction != null)
        {
            statList.Add("action");
            StartCoroutine(WaitToAction(data.callAction));
        }

        SetStat(statList[0]);

    }

    private IEnumerator WaitToAnim(string animId)
    {
        while(stat != "anim")
        {
            yield return null;
        }

        // 
        NextStat();
    }

    private IEnumerator WaitToAction(Action callAction)
    {
        while(stat != "action")
        {
            yield return null;
        }

        callAction?.Invoke();
        NextStat();
    }

    public void GetItem(GameObject obj)
    {
        Debug.Log("Get!");
        SetStat("get");

        var item = Instantiate(obj, transform);
        item.GetComponent<BoxCollider2D>().enabled = false;
        item.transform.position = new Vector3(transform.position.x, transform.position.y + itemOffsetY, transform.position.z);

        GameInstance.CallLater(1f, () => 
        {
            Destroy(item);
        });
    }


    private void SetStat(string statId, string animId = null)
    {
        stat = statId;

        switch(statId)
        {
            case "move":
                animator.SetTrigger("walk");
                break;
            case "get":
            case "standby":
                animator.SetTrigger("idle");
                break;
        }
    }

    private void NextStat()
    {
        // Debug.Log( "Curr Stat : " + stat);
        if(statList.Count <= 1)
        {
            SetStat("standby");
            statList.Clear();
            return;
        }

        statList.RemoveAt(0);
        SetStat(statList[0]);

        // Debug.Log("Next Stat : " + stat);
    }

    private void OnPlayerMove(IMessage msg)
    {
        var target = (Vector3)msg.Data;

        PreMove(target);
        SetStat("move");
    }

    private void PreMove(Vector3 target)
    {
        target.z = 0;

        // flip
        var right = target.x > transform.position.x;
        transform.localScale = new Vector3(transform.localScale.x * ((isRight == right) ? 1 : -1), transform.localScale.y , transform.localScale.z);
        isRight = right;
        
        CreatePath(target);
    }

    private void Move()
    {
        if(pathPointList == null || pathPointList.Count <= 0)
        {
            // stat = "stand";
            return;
        }

        if(Vector2.Distance(transform.position, pathPointList[currIndex]) > 0.1f)
        {
            Vector3 dir = (pathPointList[currIndex] - transform.position).normalized;
            transform.position += dir * Time.deltaTime * moveSpeed;
        }
        else
        {
            if(currIndex == pathPointList.Count - 1)
            {
                NextStat();
                return;
            }
            currIndex++;
        }
    }

    private void CreatePath(Vector3 target)
    {
        currIndex = 0;
        seeker.StartPath(transform.position, target, path =>
        {
            pathPointList = path.vectorPath;
        });
    }

    private void OnMouseDown()
    {
        GameInstance.Signal("inventory.show", transform.position);
    }


}
