using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Tool.Module.Message;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera TVcamera;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        GameInstance.Connect("camera.move", OnCameraMove);
    }

    void OnDestroy()
    {
        GameInstance.Disconnect("camera.move", OnCameraMove);
    }

    private void OnCameraMove(IMessage msg)
    {
        Vector3 pos = (Vector3)msg.Data;

        mainCamera.transform.DOMove(pos, 1f);
        TVcamera.transform.DOMove(pos, 1f);

        //  mainCamera.transform.position = Vector3.SmoothDamp (mainCamera.transform.position, pos, ref velocity, 1f);
        //  TVcamera.transform.position = Vector3.SmoothDamp (mainCamera.transform.position, pos, ref velocity, 1f);
    }
}
