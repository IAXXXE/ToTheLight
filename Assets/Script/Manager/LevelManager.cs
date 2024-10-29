using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Tool.Module.Message;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int currLevel = 1;

    // 1
    public Dictionary<string, bool> condition1 = new();

    // 2
    public Dictionary<string, bool> condition2 = new();

    // 3
    public Dictionary<string, bool> condition3 = new();

    // 4
    public Dictionary<string, bool> condition4 = new();

    // 5
    public Dictionary<string, bool> condition5 = new();

    void Awake()
    {
        condition1.Add("look_telescope", false);
        condition1.Add("R", false);
        condition1.Add("G", false);
        condition1.Add("B", false);
        condition1.Add("W", false);
        condition1.Add("book", false);

        condition2.Add("W", false);

        condition3.Add("getG", false);

        condition4.Add("getB", false);
        condition4.Add("get_cfood", false);

        condition5.Add("getR", false);

        GameInstance.Connect("condition1.unlock",OnCondition1Unlock);
        GameInstance.Connect("condition2.unlock",OnCondition2Unlock);
        GameInstance.Connect("condition3.unlock",OnCondition3Unlock);
        GameInstance.Connect("condition4.unlock",OnCondition4Unlock);
        GameInstance.Connect("condition5.unlock",OnCondition5Unlock);

    }

    private void OnCondition1Unlock(IMessage msg)
    {
        if(currLevel != 1)return;
        var id = (string)msg.Data;
        Debug.Log("condition1 id : " + id);
        condition1[id] = true;
        CheckCondition(condition1);

    }

    private void OnCondition2Unlock(IMessage msg)
    {
        if(currLevel != 2)return;
        var id = (string)msg.Data;
        condition2[id] = true;
        CheckCondition(condition2);
    }

    private void OnCondition3Unlock(IMessage msg)
    {
        if(currLevel != 3)return;
        var id = (string)msg.Data;
        condition3[id] = true;
        CheckCondition(condition3);
    }

    private void OnCondition4Unlock(IMessage msg)
    {
        if(currLevel != 4)return;
        var id = (string)msg.Data;
        condition4[id] = true;
        CheckCondition(condition4);
    }

    private void OnCondition5Unlock(IMessage msg)
    {
        if(currLevel != 5)return;
        var id = (string)msg.Data;
        condition5[id] = true;
        CheckCondition(condition5);
    }

    void CheckCondition(Dictionary<string, bool> condition)
    {
        foreach(var key in condition.Keys)
        {
            if(condition[key] == false)
                return;
        }
        Debug.Log("open " + "teleport" + currLevel.ToString());
        GameInstance.Signal("teleport.show", "teleport" + currLevel.ToString());
        
        currLevel++;
        if(currLevel == 4)
        {
            GameInstance.Signal("book.add", 2);
        }
        else if(currLevel == 5)
        {
            GameInstance.Signal("book.add", 1);
        }
    }
}
