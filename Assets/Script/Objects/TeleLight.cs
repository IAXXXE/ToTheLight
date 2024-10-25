using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using UnityEngine;

public class TeleLight : MonoBehaviour
{
    void Start()
    {
        GameInstance.Connect("light.on",OnLightOn);
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        GameInstance.Disconnect("light.on",OnLightOn);
    }

    private void OnLightOn(IMessage msg)
    {
        Debug.Log("light on " + (string)msg.Data);
        if((string)msg.Data == "tele")
        {
            gameObject.SetActive(true);
        }
    }
}

