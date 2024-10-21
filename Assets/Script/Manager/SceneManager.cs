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

        GameInstance.Connect("scene.changee", OnSceneChangee);
        GameInstance.Connect("scene.change", OnSceneChange);
    }

    public void OnDestroy()
    {
        GameInstance.Disconnect("scene.changee", OnSceneChangee);
        GameInstance.Disconnect("scene.change", OnSceneChange);
    }

    private void OnSceneChange(IMessage msg)
    {
        activeScene.UnLoadScene();
        var newScene = msg.Data as AssetReference;

        newScene.LoadSceneAsync(LoadSceneMode.Additive);
        activeScene = newScene;

        GameInstance.CallLater(0.5f, () => GameInstance.Signal("path.scan") );
    }

    private void OnSceneChangee(IMessage msg)
    {
        activeScene.UnLoadScene();
        secondScene.LoadSceneAsync(LoadSceneMode.Additive);
        activeScene = secondScene;

        GameInstance.CallLater(0.5f, () => GameInstance.Signal("path.scan") );
        
    }

    public void LoadScene()
    {
        // Addressables.LoadSceneAsync(sceneReference, LoadSceneMode.Additive);
        // sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
        // sceneReference.UnLoadScene();
    }
}
