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

    public GameObject cinemachine;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        GameInstance.Connect("camera.move", OnCameraMove);
        GameInstance.Connect("camera.follow", OnCameraFollow);
        GameInstance.Connect("camera.unfollow", OnCameraUnfollow);

        GameInstance.Connect("game.win", OnGameWin);
    }

    void OnDestroy()
    {
        GameInstance.Disconnect("camera.move", OnCameraMove);
        GameInstance.Disconnect("camera.follow", OnCameraFollow);
        GameInstance.Disconnect("camera.unfollow", OnCameraUnfollow);

        GameInstance.Disconnect("game.win", OnGameWin);
    }

    private void OnGameWin(IMessage msg)
    {
        GameInstance.CallLater(1.5f, () => {

            TVcamera.gameObject.SetActive(false);
            mainCamera.orthographicSize = 27;
            mainCamera.transform.position = new Vector3(0,0,-10);
        });
        
    }

    private void OnCameraUnfollow(IMessage msg)
    {
        cinemachine.SetActive(false);

    }

    private void OnCameraFollow(IMessage msg)
    {
        cinemachine.SetActive(true);
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
