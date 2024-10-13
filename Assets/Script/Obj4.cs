using UnityEngine;
using UnityEngine.EventSystems;

public class Obj4 : MonoBehaviour
{
    private void OnMouseEnter()
    {
        // transform.Find("o1").GetComponent<SpriteRenderer>().color = new Color(0.1f, 1f, 1f, 1f);
    }

    private void OnMouseExit()
    {
        // transform.Find("o1").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    private void OnMouseDown()
    {
        transform.Find("o").gameObject.SetActive(true);
    }

    private void OnMouseUp()
    {
        transform.Find("o").gameObject.SetActive(false);
    }
}
