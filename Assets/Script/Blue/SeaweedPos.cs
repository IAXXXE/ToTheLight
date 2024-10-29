using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeaweedPos : MonoBehaviour
{
    public int idx;
    private GameObject line;

    // Start is called before the first frame update
    void Start()
    {
        line = transform.Find("_Line").gameObject;
        line.SetActive(false);
    }

    void OnDisable()
    {
        line.SetActive(false);
    }

    void OnMouseEnter()
    {
        line.SetActive(true);
    }

    void OnMouseExit()
    {
        line.SetActive(false);
    }

    void OnMouseDown()
    {
        GameInstance.Signal("seaweed_pos.update", idx);
    }
}
