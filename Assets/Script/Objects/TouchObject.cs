using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : ObjectBase
{
    private bool isHight = false;
    protected override void ExecuteAction()
    {
        transform.Find("_Light").gameObject.SetActive(!isHight);

        if(!isHight)
            GameInstance.Signal("camera.move", new Vector3(-18,7,-10));
        else
            GameInstance.Signal("camera.move", new Vector3(-18,0,-10));
        
        isHight = !isHight;
    
    }
}
