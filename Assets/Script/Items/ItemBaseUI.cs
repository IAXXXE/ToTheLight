using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemBaseUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler
{
    public string itemId;
    public GameObject itemObject;
    public GameObject item;

    public bool isCraftable = false;
    public string targetID;
    public GameObject compositeObj;

    public bool isSingleUse = true;

    private RectTransform tran;
    private Vector2 pointerOffset;

    private Image image;

    private void Awake()
    {
        tran = GetComponent<RectTransform>();

        image = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // pointerOffset = eventData.position - (Vector2)tran.position;
        GameInstance.Signal("cursor.drag", itemId);

        image.color = new Color(1, 1, 1, 0.5f);

        item = Instantiate(itemObject);
        item.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        item.transform.parent = GameInstance.Instance.itemParent;
        item.GetComponent<ItemBase>().isDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // tran.position = eventData.position - pointerOffset;
        // GameInstance.Signal("cursor.drag", itemId);
        // GetComponent<Image>().raycastTarget = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameInstance.Signal("cursor.release", itemId);
        // pointerOffset = Vector2.zero;
        // GameInstance.Signal("cursor.release", itemId);
        // GetComponent<Image>().raycastTarget = true;
        if (item == null)
        {
            return;
        }
        item.GetComponent<ItemBase>().isDrag = false;
        Destroy(item);
        item = null;
        image.color = new Color(1, 1, 1, 1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isCraftable || GameInstance.Instance.cursor.dragId == "") return;

        if (GameInstance.Instance.cursor.dragId == targetID)
        {
            
            DestroyItem();
            GameInstance.Signal("item.add", compositeObj);
            GameInstance.Signal("item.ues", targetID);
            
            Destroy(this.gameObject);

        }
    }

    public void DestroyItem()
    {
        if (item != null)
            Destroy(item);
    }
}

