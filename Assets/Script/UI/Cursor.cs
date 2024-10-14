using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public RectTransform cursorCanvas;

    private string stat;
    
    private RaycastHit hit;
    private Ray ray;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetTrigger("interaction");

        GameInstance.Connect("game.start", OnGameStart);

        GameInstance.Connect("cursor.enter", OnCursorEnter);
        GameInstance.Connect("cursor.exit", OnCursorExit);
        
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        GameInstance.Disconnect("game.start", OnGameStart);
        GameInstance.Disconnect("cursor.enter", OnCursorEnter);
        GameInstance.Disconnect("cursor.exit", OnCursorExit);
        
    }

    private void OnGameStart(IMessage mag)
    {
        gameObject.SetActive(true);
    }

    private void OnCursorEnter(IMessage msg)
    {
        var animId = (string)msg.Data;
        stat = animId;
        animator.SetTrigger(animId);
    }


    private void OnCursorExit(IMessage msg)
    {
        stat = "walk";
        animator.SetTrigger("walk");
    }

    // Update is called once per frame
    void Update()
    {
        if (cursorCanvas == null) return;

        transform.position = Input.mousePosition;

        if(Input.GetMouseButtonDown(0) && stat == "walk")
        {
            GameInstance.Signal("player.move", Camera.main.ScreenToWorldPoint(Input.mousePosition));
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

    private void SetCursorAnim(string id)
    {
        animator.SetTrigger(id);
    }
}
