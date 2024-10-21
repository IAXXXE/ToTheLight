using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using Unity.VisualScripting;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public string itemId;

    public string useId;

    public bool isDrag = false;

    public bool isSingleUse = true;

    // void Awake()
    // {
    //     GameInstance.Connect("item.use", OnItemUse);
    // }

    // void OnDestroy()
    // {
    //     GameInstance.Connect("item.use", OnItemUse);
    // }

    // private void OnItemUse(IMessage msg)
    // {
    //     var id = (string)msg.Data;

    //     if(id == itemId && gameObject != null && isSingleUse)
    //     {
    //          Destroy(gameObject);
    //     }
    // }

    void Update()
    {
        if(isDrag)
        {
            var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            transform.position = target;
            // Debug.Log("item pos : " + transform.position);
        }
    }

    // void OnTriggerEnter2D(Collider2D collider)
    // {
    //     if(collider.CompareTag("Item") && collider.GetComponent<ObjectBase>().id == useId )
    //     {
    //         if(isSingleUse)
    //             GameInstance.Signal("item.use", itemId);
            
    //         Debug.Log("use " + itemId + " to " + useId);
    //         Destroy(gameObject);

    //     }
    // }
}
