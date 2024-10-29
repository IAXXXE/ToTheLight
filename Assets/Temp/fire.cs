using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    public GameObject getItem;
    public GameObject UiItem;

    void OnEnable()
    {
        GameInstance.Signal("fire.on");
    }
}
