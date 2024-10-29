using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFoodObj : ObjectBase
{
    protected override void ExecuteAction()
    {
        GameInstance.Signal("green.eat", transform.position);
        interactable = false;
    }
}
