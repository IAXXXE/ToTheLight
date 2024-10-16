using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
using Tool.Module.Message;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.iOS;

public class PlayerAction
{
    public Vector3 pos;
    public string animId;
    public Action callAction;
}

public class Player : MonoBehaviour
{
    public float moveSpeed = 20f;

    private string stat;
    private List<string> statList = new();

    private Seeker seeker;
    private List<Vector3> pathPointList;
    private int currIndex = 0;

    private float itemOffsetY = 1f;

    void Start()
    {
        seeker = GetComponent<Seeker>();

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
        // var target = (Vector3)msg.Data;
        // target.z = 0;

        // CreatePath(target);
        // stat = "move";
        var data = (PlayerAction)msg.Data;

        if(data.pos != null)
        {
            statList.Add("move");
            var target = data.pos;
            target.z = 0;
            CreatePath(target);
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

        stat = statList[0];

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

    public void GetItem(GameObject gameObject)
    {
        Debug.Log("Get!");
        stat = "get";

        var item = Instantiate(gameObject, transform);

        item.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + itemOffsetY, transform.position.z));

        GameInstance.CallLater(1.5f, () => Destroy(item));
    }


    private void SetStat(string id)
    {
        stat = id;
    }

    private void NextStat()
    {
        Debug.Log( "Curr Stat : " + stat);
        if(statList.Count <= 1)
        {
            stat = "standby";
            statList.Clear();
            return;
        }

        statList.RemoveAt(0);
        stat = statList[0];

        Debug.Log("Next Stat : " + stat);
    }

    private void OnPlayerMove(IMessage msg)
    {
        var target = (Vector3)msg.Data;
        target.z = 0;

        CreatePath(target);
        stat = "move";
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



}
