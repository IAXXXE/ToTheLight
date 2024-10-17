using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TeleportObject : ObjectBase
{
    public AssetReference sceneToGo;
    public Vector3 goPos;

    protected override void ExecuteAction()
    {
        base.ExecuteAction();

        GameInstance.Signal("scene.change", sceneToGo);
        // GameInstance.CallLater(0.5f , ()=> GameInstance.Instance.player.transform.position = goPos);
        GameInstance.Instance.player.transform.position = goPos;
    }
}
