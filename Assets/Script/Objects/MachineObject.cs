using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineObject : ObjectBase
{
    private bool hasLaba = true;
    public GameObject getItem;
    public GameObject UiItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void ExecuteAction()
    {
        base.ExecuteAction();
        if(hasLaba)
        {
            sprite.SetActive(false);
            sprited.SetActive(true);

            GameInstance.Instance.player.GetItem(getItem);
            GameInstance.CallLater(1f, () => GameInstance.Signal("item.add", UiItem)); 

            hasLaba = false;
        }
        else
        {
            GameInstance.Signal("panel.show", "Machine");
        }
    }
}