using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Tool.Module.Message;
using UnityEngine;

public class BlueEgg : MonoBehaviour
{
    public Transform seaweed;

    public List<Transform> pos;

    // Start is called before the first frame update
    void Start()
    {
        GameInstance.Connect("seaweed_pos.update", OnPosUpdate);
    }

    private void OnPosUpdate(IMessage msg)
    {
        var idx = (int)msg.Data;
        for(int i = 0; i < pos.Count ; i++)
        {
            pos[i].gameObject.SetActive(i != idx);
        }

        var newPos = pos[idx].position;

        seaweed.DOMoveX( newPos.x, 1f);
    }
}
