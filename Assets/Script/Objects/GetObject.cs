using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GetObject : ObjectBase
{
    public GameObject getItem;
    public GameObject UiItem;

    protected override void ExecuteAction()
    {
        base.ExecuteAction();
        sprite.SetActive(false);
        sprited.SetActive(true);
        interactable = false;

        Debug.Log("make player get");
        GameInstance.Instance.player.GetItem(getItem);
        GameInstance.CallLater(1f, () => GameInstance.Signal("item.add", UiItem)); 
    }

    protected override PlayerAction GenerateAction()
    {
        return base.GenerateAction();
    }

}
