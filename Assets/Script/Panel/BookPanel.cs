using System.Collections;
using System.Collections.Generic;
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
    
    // Start is called before the first frame update
    void Start()
    {
        pageImage = transform.Find("_Page").GetComponent<Image>();
        leftArr = transform.Find("_ArrLeft").gameObject;
        rightArr = transform.Find("_ArrRight").gameObject;

        pageImage.sprite = pageList[0];

        leftArr.SetActive(false);
        rightArr.SetActive(true);
    }

    public void onPageChange(int num)
    {
        currPage += num;

        pageImage.sprite = pageList[currPage];

        leftArr.SetActive(true);
        rightArr.SetActive(true);

        if(currPage <= 0) leftArr.SetActive(false);
        else if(currPage >= pageList.Count - 1) rightArr.SetActive(false);
    }


}
