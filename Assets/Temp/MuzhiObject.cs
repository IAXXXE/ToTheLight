using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzhiObject : ObjectBase
{
    public GameObject getItem;
    public GameObject UiItem;

    protected override void ExecuteAction()
    {
        if(gameObject == null) return;
        if(sprite.transform.childCount > 0) return;
        base.ExecuteAction();

        GameInstance.Instance.player.GetItem(getItem);
        GameInstance.CallLater(1f, () => GameInstance.Signal("item.add", UiItem)); 

        Destroy(gameObject);
    }


}
