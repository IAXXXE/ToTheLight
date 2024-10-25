using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using Unity.VisualScripting;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public RectTransform cursorCanvas;

    private string stat;

    private RaycastHit hit;
    private Ray ray;
    private Animator animator;

    public string dragId = "";
    public bool isPanel = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        GameInstance.Connect("game.start", OnGameStart);
        GameInstance.Connect("cursor.show", OnCursorShow);
        GameInstance.Connect("cursor.hide", OnCursorHide);

        GameInstance.Connect("cursor.drag", OnCursorDrag);
        GameInstance.Connect("cursor.release", OnCursorRelease);

        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        GameInstance.Disconnect("game.start", OnGameStart);

        GameInstance.Disconnect("cursor.show", OnCursorShow);
        GameInstance.Disconnect("cursor.hide", OnCursorHide);

        GameInstance.Disconnect("cursor.drag", OnCursorDrag);
        GameInstance.Disconnect("cursor.release", OnCursorRelease);

    }

    protected void OnEnable()
    {
        GameInstance.Connect("cursor.enter", OnCursorEnter);
        GameInstance.Connect("cursor.exit", OnCursorExit);
    }

    protected void OnDisable()
    {

        GameInstance.Disconnect("cursor.enter", OnCursorEnter);
        GameInstance.Disconnect("cursor.exit", OnCursorExit);

    }

    private void OnCursorHide(IMessage rMessage)
    {
        gameObject.SetActive(false);
    }

    private void OnCursorShow(IMessage rMessage)
    {
        gameObject.SetActive(true);
    }

    private void OnCursorRelease(IMessage msg)
    {
        dragId = "";
    }

    private void OnCursorDrag(IMessage msg)
    {
        dragId = (string)msg.Data;
    }

    private void OnGameStart(IMessage mag)
    {
        gameObject.SetActive(true);
        SetCursor("walk");
    }

    private void OnCursorEnter(IMessage msg)
    {
        var animId = (string)msg.Data;
        SetCursor(animId);
    }


    private void OnCursorExit(IMessage msg)
    {
        var id = (string)msg.Data;
        SetCursor("none");
        var clickPos = Input.mousePosition;
        if (clickPos.x < 345f || clickPos.x > 1645f) return;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
        if(hitCollider == null)
        {
            SetCursor("walk");
        }
        Debug.Log(hitCollider?.name);
    }

    void Update()
    {
        if (cursorCanvas == null || !gameObject.activeSelf ) return;

        transform.position = Input.mousePosition;

        if(isPanel) return;

        var clickPos = Input.mousePosition;
        if (clickPos.x < 345f || clickPos.x > 1645f) return;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

        if(hitCollider != null)
        {
            if(hitCollider.CompareTag("Interactive"))
            {
                SetCursor("interactive");
            }
            else if(hitCollider.CompareTag("Boundary"))
            {
                SetCursor("none");
            }
        }
        else
        {
            SetCursor("walk");
        }


        if (Input.GetMouseButtonDown(0) && !isPanel && !hitCollider)
        {
            GameInstance.Signal("player.move", new Vector3(mousePosition.x, mousePosition.y, 0));
        }

        // ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // if (Physics.Raycast(ray, out hit))
        // {
        //     if (hit.collider.tag.Equals("Interactive")) 
        //     {
        //         SetCursorAnim("interactive");
        //     }
        //     else if(hit.collider.tag.Equals("Boundary"))
        //     {
        //         SetCursorAnim("Ban");
        //     }
        //     else
        //     {
        //         SetCursorAnim("Walk");
        //     }
        // }
    }

    private void SetCursor(string id)
    {
        if(stat == id) return;
        stat = id;
        if(id == "ban") 
        {
            animator.SetTrigger("none");
            return;
        }
        animator.SetTrigger(id);
    }
}
