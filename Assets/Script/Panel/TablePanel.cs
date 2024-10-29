using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablePanel : MonoBehaviour
{
    public Transform items;

    private bool hasGetW;
    private bool hasEmpty;

    void OnDisable()
    {
        if(items.Find("_W") == null && !hasGetW)
        {
            Debug.Log("Get w signal");
            GameInstance.Signal("w1.get");
            hasGetW = true;
        }

        if(items.childCount == 0 && !hasEmpty)
        {
            GameInstance.Signal("table.clear");
            hasEmpty = true;
        }
    }
}
