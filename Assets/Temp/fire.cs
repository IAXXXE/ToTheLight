using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : ObjectBase
{
    public GameObject getItem;
    public GameObject UiItem;

    private void OnMouseUp()
    {
        if(!mouseIn) return;
        if(GameInstance.Instance.cursor.dragId == "laba" )
        {
            gameObject.SetActive(false);
            GameInstance.Instance.player.GetItem(getItem);
            GameInstance.CallLater(1f, () => GameInstance.Signal("item.add", UiItem)); 
        }
    }
}
