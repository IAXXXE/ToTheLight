using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using Unity.VisualScripting;
using UnityEngine;

public class GrassObj : ObjectBase
{
    public Animator animator;

    public GameObject getItem;
    public GameObject UiItem;

    public GameObject shuimu;
    public GameObject B;

    public GameObject pFood;

    private bool isLong = false;

    // normal/ useBait / hasBait/ hasJellyfish
    private string stat = "normal";

    private int pos = 2;

    void Start()
    {
        GameInstance.Connect("seaweed_pos.update", OnPosUpdate);
    }

    private void OnPosUpdate(IMessage msg)
    {
        pos = (int)msg.Data;
    }

    protected override void ExecuteAction()
    {
        base.ExecuteAction();
        if(stat == "normal")
        {
            SwitchStat();
        }
        else if(stat == "useBait")
        {
            sprite.transform.Find("_yFood").gameObject.SetActive(true);
            sprited.transform.Find("_yFood").gameObject.SetActive(true);
            stat = "hasBait";
        }
        else if(stat == "hasBait" || stat == "hasJellyfish" )
        {
            SwitchStat();
        }
        else if(stat == "getLight")
        {
            sprite.transform.Find("_yFood").gameObject.SetActive(false);
            sprited.transform.Find("_yFood").gameObject.SetActive(false);
            sprite.transform.Find("_Shuimu").gameObject.SetActive(false);
            sprited.transform.Find("_Shuimu").gameObject.SetActive(false);

            Destroy(pFood);

            GameInstance.Instance.player.GetItem(getItem);
            GameInstance.CallLater(1f, () => GameInstance.Signal("item.add", UiItem));
            GameInstance.CallLater(0.1f, () => GameInstance.Instance.audioManager.PlayAudio(3));

            GameInstance.Signal("condition4.unlock", "getB");
            shuimu.transform.position = transform.Find("_Sprite/_Shuimu").position;
            shuimu.GetComponent<JellyfishBlue>().SetStat("follow");
            shuimu.gameObject.SetActive(true);

            GameInstance.Signal("green.follow");

            stat = "normal";
        }
    }

    private void SwitchStat ()
    {
        isLong = !isLong;

        if(isLong)
        {
            animator.SetTrigger("elongate");
        }
        else
        {
            animator.SetTrigger("shrink");
        }
        // sprite.gameObject.SetActive(!sprite.gameObject.activeSelf);
        // sprited.gameObject.SetActive(!sprited.gameObject.activeSelf);

        if(sprited.gameObject.activeSelf && sprited.transform.Find("_yFood").gameObject.activeSelf && pos == 0)
        {
            // sprite.transform.Find("_yFood").gameObject.SetActive(false);
            // sprited.transform.Find("_yFood").gameObject.SetActive(false);

            GameInstance.CallLater(1f, () => {
                sprite.transform.Find("_Shuimu").gameObject.SetActive(true);
                sprited.transform.Find("_Shuimu").gameObject.SetActive(true);
            });

            B.gameObject.SetActive(false);

            stat = "hasJellyfish";
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Item") && collider.GetComponent<ItemBase>().itemId.Contains("y_food") && sprite.gameObject.activeSelf)
        {
                stat = "useBait";
                GameInstance.Signal("item.use", collider.GetComponent<ItemBase>().itemId);
                var playerAction = GenerateAction();
                GameInstance.Signal("player.interact", playerAction);
        }
        else if(collider.CompareTag("Laba") && sprite.gameObject.activeSelf && sprited.transform.Find("_Shuimu").gameObject.activeSelf)
        {
            stat = "getLight";
            var playerAction = GenerateAction();
            GameInstance.Signal("player.interact", playerAction);
        }
    }

    public void SetAnim(string id)
    {
        animator.SetTrigger(id);
    }

}
