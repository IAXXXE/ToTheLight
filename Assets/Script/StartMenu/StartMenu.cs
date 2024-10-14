using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void OnButtonClick()
    {
        GameInstance.Signal("game.start");
        GameInstance.Signal("scene.change");
    }
}
