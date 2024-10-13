using UnityEngine;
using UnityEngine.EventSystems;

public class Obj2 : MonoBehaviour
{
    public GameObject obj;

    private void OnMouseEnter()
    {
        transform.Find("o1").GetComponent<SpriteRenderer>().color = new Color(0.1f, 1f, 1f, 1f);
    }

    private void OnMouseExit()
    {
        transform.Find("o1").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    private void OnMouseDown()
    {
        transform.Find("o1").gameObject.SetActive(false);
        transform.Find("o2").gameObject.SetActive(true);

        obj.SetActive(true);
    }
}
