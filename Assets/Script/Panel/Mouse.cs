using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse : MonoBehaviour, IPointerEnterHandler
{
    public string id;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter + " + id + " drag id : " + GameInstance.Instance.cursor.dragId);
        if (GameInstance.Instance.cursor.dragId == "") return;

        if (GameInstance.Instance.cursor.dragId == id)
        {
            GameInstance.Signal("light.use", id);
            GameInstance.Signal("mouth.eat", id);
            GameInstance.Instance.cursor.dragId = "";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
