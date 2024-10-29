using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TelePanel : MonoBehaviour
{
    // public RectTransform ImageTrans;
    public Image image;

    public bool hasPlay = false;

    [SerializeField]
    public List<Sprite> imageList;
    private List<int> idxList = new List<int>{0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,2};
    
    void OnEnable()
    {
        // ImageTrans.anchoredPosition = new Vector3(1039f, -90f, 0);
        // ImageTrans.DOAnchorPos(new Vector2(1535f, -339f), 5f).SetEase(Ease.OutQuart);

        if(!hasPlay)
        {
            GameInstance.Signal("condition1.unlock", "look_telescope");
            StartCoroutine(PlayAnim());

        }
    }

    void OnDisable()
    {
        GameInstance.Signal("player.say", "white");
    }

    private IEnumerator PlayAnim()
    {
        for(int i =0 ; i<idxList.Count; i++)
        {
            image.sprite = imageList[idxList[i]];
            yield return new WaitForSeconds(0.1f);
            if(i == idxList.Count - 2)
            {
                image.color = new Color(1,1,1,0);
                image.DOFade(1f, 3f);
                transform.GetComponent<Image>().DOColor(new Color(0.1f,0.1f,0.1f,1f), 3f);
                transform.Find("_Mask").GetComponent<Image>().DOColor(new Color(0.5f,0.5f,0.5f,1f), 3f);
                GameInstance.Signal("light.off", "tele");
            }
        }
        hasPlay = true;
        yield break;
    }
}
