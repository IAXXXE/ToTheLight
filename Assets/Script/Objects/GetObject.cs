using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GetObject : ObjectBase
{
    public GameObject getItem;

    protected override void ExecuteAction()
    {
        base.ExecuteAction();
        sprite.SetActive(false);
        sprited.SetActive(true);
        interactable = false;

        Debug.Log("make player get");
        GameInstance.Instance.player.GetItem(getItem);
    }

    protected override PlayerAction GenerateAction()
    {
        return base.GenerateAction();
    }

}
