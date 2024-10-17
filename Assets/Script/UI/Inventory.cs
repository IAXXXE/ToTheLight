using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject itemPrefab;
    public List<GameObject> itemList = new();

    public Vector3 initPos = new Vector3(0, 0, 0);
    public float space = 1f;

    void Awake()
    {
        GameInstance.Connect("item.add", OnItemAdd);

    }

    void OnDestroy()
    {
        GameInstance.Disconnect("item.add", OnItemAdd);
    }

    private void OnItemAdd(IMessage msg)
    {
        var item = (GameObject)msg.Data;
        var itemObj = Instantiate(item, transform);
        itemList.Add(itemObj);

        itemObj.GetComponent<RectTransform>().localPosition = new Vector3(initPos.x + space * (itemList.Count - 1), initPos.y, 0);
        Debug.Log(" pos : " + itemObj.GetComponent<RectTransform>().localPosition);
    }

    public void SortItems()
    {
        List<Vector3> posList = new();
        for(int i = 0; i < itemList.Count; i++)
        {
            itemList[i].GetComponent<RectTransform>().localPosition = new Vector3(initPos.x + space * i, initPos.y, 0);
            // .DOAnchorPosX(100f, 1f);
        }
    }
}

