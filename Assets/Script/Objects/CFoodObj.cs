using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFoodObj : ObjectBase
{
    public GameObject getItem;
    public GameObject UiItem;

    public bool needDestroy = false;

    protected override void ExecuteAction()
    {
        Debug.Log("cfood");
        base.ExecuteAction();

        GameInstance.Signal("condition4.unlock", "get_cfood");
        GameInstance.Instance.player.GetItem(getItem);
        GameInstance.CallLater(1f, () => GameInstance.Signal("item.add", UiItem));
        if(needDestroy) gameObject.SetActive(false);
    }

    protected override PlayerAction GenerateAction()
    {
        return base.GenerateAction();
    }

}
