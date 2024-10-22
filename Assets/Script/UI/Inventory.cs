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

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // GameInstance.Connect("inventory.show", OnInventoryShow);

        GameInstance.Connect("item.add", OnItemAdd);
        GameInstance.Connect("item.use", OnItemUse);

    }

    void OnDestroy()
    {
        // GameInstance.Disconnect("inventory.show", OnInventoryShow);

        GameInstance.Disconnect("item.add", OnItemAdd);
        GameInstance.Disconnect("item.use", OnItemUse);
    }

    protected void OnEnable()
    {
        GameInstance.Signal("cursor.hide");
    }

    protected void OnDisable()
    {
        GameInstance.Signal("cursor.show");
    }

    // private void OnInventoryShow(IMessage msg)
    // {

    //     // var pos = Camera.main.WorldToScreenPoint((Vector3)msg.Data);
    //     // var pos = Input.mousePosition;
    //     // pos.z = 0;
    //     // rectTransform.position = pos;
    //     var pos = Camera.main.WorldToScreenPoint(GameInstance.Instance.player.transform.position);
    //     pos.y += 100f;
    //     pos.z = 0;
    //     rectTransform.position = pos;
    //     gameObject.SetActive(true);

    //     GameInstance.Signal("cursor.hide");
    // }


    private void OnItemUse(IMessage msg)
    {
        var id = (string)msg.Data;
        Debug.Log("use item " + id);
        var newList = new List<GameObject>();

        GameObject removeItem = null;
        foreach(GameObject item in itemList)
        {
            if(item == null)
            {
                continue;   
            }
            if(item.GetComponent<ItemBaseUI>().itemId == id)
            {
                Debug.Log("find remove " + id);
                if(!item.GetComponent<ItemBaseUI>().isSingleUse) newList.Add(item);
                else removeItem = item;
                continue;
            }

            newList.Add(item);
        }

        itemList = newList;

        Destroy(removeItem);

        // ArrangeItemsInSemiCircle();
        ArrangeItemsInLeftScreen();
    }

    private void ClearBadItem() 
    {
        
    }


    private void OnItemAdd(IMessage msg)
    {
        var item = (GameObject)msg.Data;
        var itemObj = Instantiate(item, transform);
        itemList.Add(itemObj);

        // itemObj.GetComponent<RectTransform>().localPosition = new Vector3(initPos.x + space * (itemList.Count - 1), initPos.y, 0);
        // ArrangeItemsInSemiCircle();
        ArrangeItemsInLeftScreen();
    }

    // public void SortItems()
    // {
    //     List<Vector3> posList = new();
    //     for(int i = 0; i < itemList.Count; i++)
    //     {
    //         itemList[i].GetComponent<RectTransform>().localPosition = new Vector3(initPos.x + space * i, initPos.y, 0);
    //         // .DOAnchorPosX(100f, 1f);
    //     }
    // }

    private float radius = 200f;          // 圆弧的半径
    private int itemCount = 6;            // 道具数量（这里以6个道具为例）
    private float startAngle = 180f;      // 开始角度 (-90度是从中间开始，左右对称)
    private float endAngle = 0f;         // 结束角度

    private void Start()
    {
        // ArrangeItemsInSemiCircle();
        ArrangeItemsInLeftScreen();
    }

    // private void Update()
    // {
    //     if(gameObject.activeSelf)
    //     {
    //         var pos = Camera.main.WorldToScreenPoint(GameInstance.Instance.player.transform.position);
    //         pos.y += 100f;
    //         pos.z = 0;
    //         rectTransform.position = pos;
    //     }
        
    // }

    private void ArrangeItemsInLeftScreen()
    {
        List<Vector3> posList = new();
        for(int i = 0; i < itemList.Count; i++)
        {
            itemList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(initPos.x , initPos.y - space * i);
            // .DOAnchorPosX(100f, 1f);
        }
    }

    // 将道具栏中的所有道具排列成半圆形
    private void ArrangeItemsInSemiCircle()
    {
        itemCount = itemList.Count;
        if(itemCount == 0) return;

        if(itemCount == 1)
        {
            RectTransform item = rectTransform.GetChild(0).GetComponent<RectTransform>();
            item.anchoredPosition = new Vector2(-radius/2, 0);
            return;
        }
        // 计算角度步长
        float angleStep = (endAngle - startAngle) / (itemCount - 1);

        // 遍历道具栏中的所有道具
        for (int i = 0; i < itemCount; i++)
        {
            // 获取当前道具的 RectTransform
            RectTransform item = rectTransform.GetChild(i).GetComponent<RectTransform>();

            // 计算当前道具的角度
            float angle = startAngle + i * angleStep;

            // 将角度转换为弧度
            float radian = angle * Mathf.Deg2Rad;

            // 计算道具在圆弧上的位置
            float x = Mathf.Cos(radian) * radius;
            float y = Mathf.Sin(radian) * radius;

            // 设置道具的局部位置（相对于 inventoryPanel）
            item.anchoredPosition = new Vector2(x, y);

            // 道具朝向中心，调整其旋转
            // item.localRotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}

