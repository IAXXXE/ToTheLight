using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GetObject : ObjectBase
{
    public GameObject getItem;
    public GameObject UiItem;

    public bool needDestroy = false;

    protected override void ExecuteAction()
    {
        base.ExecuteAction();
        sprite.SetActive(false);
        sprited.SetActive(true);
        interactable = false;

        GameInstance.Instance.player.GetItem(getItem);
        GameInstance.CallLater(1f, () => GameInstance.Signal("item.add", UiItem)); 
        if(needDestroy) Destroy(gameObject);
    }

    protected override PlayerAction GenerateAction()
    {
        return base.GenerateAction();
    }

}
