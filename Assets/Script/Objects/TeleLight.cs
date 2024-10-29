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
        GameInstance.Connect("light.off",OnLightOff);
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        GameInstance.Disconnect("light.on",OnLightOn);
        GameInstance.Disconnect("light.off",OnLightOff);
    }

    private void OnLightOff(IMessage msg)
    {
        Debug.Log("close");
        if((string)msg.Data == "tele")
        {
            gameObject.SetActive(false);
        }
    }


    private void OnLightOn(IMessage msg)
    {
        if((string)msg.Data == "tele")
        {
            gameObject.SetActive(true);
        }
    }
}

