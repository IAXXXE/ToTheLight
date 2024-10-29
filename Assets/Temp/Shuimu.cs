using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shuimu : MonoBehaviour
{
    public GameObject muzhi;
    public GameObject fire;

    void OnMouseDown()
    {
        if(muzhi.gameObject.activeSelf)
        {
            fire.gameObject.SetActive(true);
            muzhi.gameObject.SetActive(false);
            gameObject.SetActive(false);

            GameInstance.Signal("fire.on");
        }
    }
}
