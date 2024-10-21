using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrassObj : ObjectBase
{
    public GameObject getItem;
    public GameObject UiItem;

    public GameObject shuimu;
    public GameObject B;


    // normal/ useBait / hasBait/ hasJellyfish
    private string stat = "normal";

    protected override void ExecuteAction()
    {
        base.ExecuteAction();
        if(stat == "normal")
            SwitchStat();
        else if(stat == "useBait")
        {
            sprite.transform.Find("_yFood").gameObject.SetActive(true);
            sprited.transform.Find("_yFood").gameObject.SetActive(true);
            stat = "hasBait";
        }
        else if(stat == "hasBait" || stat == "hasJellyfish")
        {
            SwitchStat();
        }
        else if(stat == "getLight")
        {
            sprite.transform.Find("_Shuimu").gameObject.SetActive(false);
            sprited.transform.Find("_Shuimu").gameObject.SetActive(false);

            GameInstance.Instance.player.GetItem(getItem);
            GameInstance.CallLater(1f, () => GameInstance.Signal("item.add", UiItem)); 

            shuimu.gameObject.SetActive(true);
        }
    }

    private void SwitchStat ()
    {
        sprite.gameObject.SetActive(!sprite.gameObject.activeSelf);
        sprited.gameObject.SetActive(!sprited.gameObject.activeSelf);

        if(sprited.gameObject.activeSelf && sprited.transform.Find("_yFood").gameObject.activeSelf)
        {
            sprite.transform.Find("_yFood").gameObject.SetActive(false);
            sprited.transform.Find("_yFood").gameObject.SetActive(false);

            sprite.transform.Find("_Shuimu").gameObject.SetActive(true);
            sprited.transform.Find("_Shuimu").gameObject.SetActive(true);

            B.gameObject.SetActive(false);

            stat = "hasJellyfish";
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Item"))
        {
            if(collider.GetComponent<ItemBase>().itemId.Contains("food") && sprite.gameObject.activeSelf)
            {
                stat = "useBait";
                GameInstance.Signal("item.use", collider.GetComponent<ItemBase>().itemId);
                var playerAction = GenerateAction();
                GameInstance.Signal("player.interact", playerAction);
            }
            else if(collider.GetComponent<ItemBase>().itemId == "laba" && sprite.gameObject.activeSelf && sprited.transform.Find("_Shuimu").gameObject.activeSelf)
            {
                stat = "getLight";
                var playerAction = GenerateAction();
                GameInstance.Signal("player.interact", playerAction);
            }
            
        }
    }

}
