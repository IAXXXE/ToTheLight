using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using UnityEngine;

public class TableObj : SearchObject
{
    void Start()
    {
        GameInstance.Connect("table.clear", OnTableClear);
    }

    private void OnTableClear(IMessage msg)
    {
        sprite.gameObject.SetActive(false);

        sprited.gameObject.SetActive(true);

    }
}
