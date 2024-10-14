using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnMouseEnter()
    { 
        GameInstance.Signal("cursor.enter", "ban");
    }

    private void OnMouseExit()
    {
        GameInstance.Signal("cursor.exit", "ban");
    }
}
