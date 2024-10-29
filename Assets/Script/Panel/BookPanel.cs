using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Tool.Module.Message;
using UnityEngine;
using UnityEngine.UI;

public class BookPanel : MonoBehaviour
{
    [SerializeField]
    public List<Sprite> pageList;

    private Image pageImage;
    private int currPage = 0;
    private GameObject leftArr;
    private GameObject rightArr;
    private int maxPage = 2;

    private Dictionary<int, bool> pageUnLock = new Dictionary<int, bool>
    {
        {0, true},{1, true},{2, true},{3, false},{4, false},{5, false}
    };
    
    // Start is called before the first frame update
    void Start()
    {
        pageImage = transform.Find("_Page").GetComponent<Image>();
        leftArr = transform.Find("_ArrLeft").gameObject;
        rightArr = transform.Find("_ArrRight").gameObject;

        pageImage.sprite = pageList[0];

        leftArr.SetActive(false);
        rightArr.SetActive(true);

        GameInstance.Connect("book.add", OnBookAdd);
        GameInstance.Connect("unlock.page", OnPageUnlock);

        gameObject.SetActive(false);
    }

    private void OnPageUnlock(IMessage msg)
    {
        pageUnLock[currPage] = true;
    }

    private void OnBookAdd(IMessage msg)
    {
        int num = (int)msg.Data;
        maxPage += num;
        PageTo(maxPage);
    }

    public void PageChange(int num)
    {
        currPage += num;

        pageImage.sprite = pageList[currPage];

        transform.Find("_Sticker").gameObject.SetActive(!pageUnLock[currPage]);

        leftArr.SetActive(true);
        rightArr.SetActive(true);

        if(currPage <= 0) leftArr.SetActive(false);
        else if(currPage >= pageList.Count - 1 || currPage >= maxPage) rightArr.SetActive(false);
    }

    public void PageTo(int num)
    {
        if(num > maxPage) return;

        currPage = num;

        pageImage.sprite = pageList[currPage];

        transform.Find("_Sticker").gameObject.SetActive(!pageUnLock[currPage]);

        leftArr.SetActive(true);
        rightArr.SetActive(true);

        if(currPage <= 0) leftArr.SetActive(false);
        else if(currPage >= pageList.Count - 1 || currPage >= maxPage) rightArr.SetActive(false);
    }



}
