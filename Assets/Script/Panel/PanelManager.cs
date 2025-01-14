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
        ShowPanel(id);
    }

    private void OnPanelHide(IMessage msg)
    {
        var id = (string)msg.Data;
        HidePanel(id);
    }

    public void ShowPanel(string id)
    {
        GameInstance.Instance.cursor.isPanel = true;
        GameInstance.Signal("cursor.enter","none");
        transform.Find("_" + id + "Panel").gameObject.SetActive(true);
    }

    public void HidePanel(string id)
    {
        transform.Find("_" + id + "Panel").gameObject.SetActive(false);
        foreach(Transform child in transform)
        {
            if(child.gameObject.activeSelf) return;
        }
        GameInstance.Instance.cursor.isPanel = false;
    }
}
