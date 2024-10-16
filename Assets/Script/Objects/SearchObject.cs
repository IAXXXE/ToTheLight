using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchObject : ObjectBase
{
    public string panelId;

    protected override void ExecuteAction()
    {
        base.ExecuteAction();

        GameInstance.Signal("panel.show", panelId);
    }

}
