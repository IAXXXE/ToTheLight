using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    public Light2D globalLight;

    void Start()
    {
        GameInstance.Connect("game.start", OnGameStart);
    }

    void OnDestroy()
    {
        GameInstance.Disconnect("game.start", OnGameStart);
    }

    private void OnGameStart(IMessage msg)
    {
        StartCoroutine(ToDark());
    }

    private IEnumerator ToDark()
    {
        Debug.Log("light" + globalLight.color.r);
        while(globalLight.color.r > 0.3f)
        {
            // yield return null;
            yield return new WaitForSeconds(0.05f);
            var value = globalLight.color.r;
            globalLight.color = new Color(value - 0.01f, value - 0.01f, value - 0.01f);
        }

        GameInstance.Signal("light.on","tele");

        yield break;
    }
}
