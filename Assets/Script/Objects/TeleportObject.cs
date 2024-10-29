using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TeleportObject : ObjectBase
{
    // public AssetReference sceneToGo;
    public Vector3 goPos;
    public bool isLock = true;

    protected void Start()
    {
        GameInstance.Connect("teleport.show", OnTeleportShow);
        GameInstance.Connect("teleport.hide", OnTeleportHide);
        if(isLock)
            gameObject.SetActive(false);
    }

    private void OnTeleportHide(IMessage msg)
    {
        var msgId = (string)msg.Data;
        if(base.id == msgId)
            gameObject.SetActive(false);
        isLock = false;
    }


    private void OnTeleportShow(IMessage msg)
    {
        var msgId = (string)msg.Data;
        if(base.id == msgId)
            gameObject.SetActive(true);
        isLock = true;
    }

    protected override void ExecuteAction()
    {
        base.ExecuteAction();

        // GameInstance.Signal("scene.change", sceneToGo);
        // // GameInstance.CallLater(0.5f , ()=> GameInstance.Instance.player.transform.position = goPos);
        // GameInstance.Instance.player.transform.position = goPos;

        GameInstance.Signal("camera.move", goPos);

    }
}
