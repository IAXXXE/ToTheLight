using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEgg : MonoBehaviour
{
    // stat :  trapped talk getout
    private string stat;

    private Animator animator;
   
    public Transform stones;
    public Transform jellyfish;
    public Transform jellyfishs;

    void Awake()
    {
        animator = GetComponent<Animator>();
        stones = transform.Find("_Stones");
        SetStat("trapped");
    }

    void SetStat(string id)
    {
        stat = id;
        animator.SetTrigger(id);
    }

    // protected void OnTriggerEnter2D(Collider2D collider)
    // {
    //     if(collider.CompareTag("Player") && !isGetOut)
    //     {
    //         SetStat("talk");
    //     }
    // }

    // protected void OnTriggerExit2D(Collider2D collider)
    // {
    //     if(collider.CompareTag("Player") && !isGetOut)
    //     {
    //         SetStat("trapped");
    //     }
    // }

    protected void OnMouseEnter()
    {
        if(stat == "trapped")
        {
            SetStat("talk");
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    protected void OnMouseDown()
    {
        if(stat == "trapped")
        {
            SetStat("talk");
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else if(stat == "talk")
        {
            if(stones.childCount <= 0)
            {
                SetStat("getout");
            }
        }
    }

    public void AfterGetOut()
    {
        Debug.Log("AfterGetOut");
        GetComponent<BoxCollider2D>().enabled = false;
        animator.enabled = false;
        // var pos = jellyfish.localPosition;
        // jellyfish.parent = jellyfishs;
        // jellyfish.localPosition = new Vector3(12f,-5f, 0);

        jellyfish.GetComponent<JellyfishGreen>().SetStat("wait");
    }

    public void PlayAudio()
    {
        GameInstance.Instance.audioManager.PlayAudio(5);
    }
}
