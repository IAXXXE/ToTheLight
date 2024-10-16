using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    void OnEnable()
    {
        GameInstance.Connect("panel.show", OnPanelShow);
        GameInstance.Connect("panel.hide", OnPanelHide);
    }

    void OnDisEnable()
    {
        GameInstance.Disconnect("panel.show", OnPanelShow);
        GameInstance.Disconnect("panel.hide", OnPanelHide);
    }

    private void OnPanelShow(IMessage msg)
    {
        var id = (string)msg.Data;
        transform.Find("_" + id + "Panel").gameObject.SetActive(true);
    }

    private void OnPanelHide(IMessage msg)
    {
        var id = (string)msg.Data;
        transform.Find("_" + id + "Panel").gameObject.SetActive(false);
    }
}
