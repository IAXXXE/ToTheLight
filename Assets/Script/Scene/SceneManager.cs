using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public AssetReference startScene;

    public AssetReference secondScene;

    private AssetReference activeScene;

    public void Start()
    {
        startScene.LoadSceneAsync(LoadSceneMode.Additive);
        activeScene = startScene;

        GameInstance.Connect("scene.change", OnSceneChange);
    }

    public void OnDestroy()
    {
        GameInstance.Disconnect("scene.change", OnSceneChange); 
    }

    private void OnSceneChange(IMessage msg)
    {
        Debug.Log(" OnSceneChange ");

        activeScene.UnLoadScene();
        secondScene.LoadSceneAsync(LoadSceneMode.Additive);

    }

    public void LoadScene()
    {
        // Addressables.LoadSceneAsync(sceneReference, LoadSceneMode.Additive);
        // sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
        // sceneReference.UnLoadScene();
    }
}
