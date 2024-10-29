using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuZhi : MonoBehaviour
{
    public GameObject fire;
    // Start is called before the first frame update
    void OnMouseEnter()
    {
        fire.gameObject.SetActive(true);
        GameInstance.Signal("fire.on");
    }
}
