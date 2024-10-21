using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyntheticMachine : MonoBehaviour
{
    private Transform lights;

    private List<string> lightList = new();
    private List<string> newLight = new();

    // public GameObject legghtR;
    // public GameObject legghtG;
    // public GameObject legghtB;

    public GameObject getItem;
    public GameObject UiItem;

    // Start is called before the first frame update
    void Start()
    {
        lights = transform.Find("_Lights");
    }

    void OnMouseDown()
    {
        Debug.Log("Mouse down " + lightList);
        Synthetic();
    }

    private void Synthetic()
    {
        if(lightList.Contains("R") && lightList.Contains("G") && lightList.Contains("B"))
        {
            GameInstance.Instance.player.GetItem(getItem);
            GameInstance.CallLater(1f, () => GameInstance.Signal("item.add", UiItem));
            lightList.Clear();
        }
    }

    private void GetLegght(string color)
    {
        lightList.Add(color);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Item") )
        {
            if(collider.GetComponent<ItemBase>().itemId == "R")
            {
                GameInstance.Signal("item.use","R");
                lightList.Add("R");
                
            }
            if(collider.GetComponent<ItemBase>().itemId == "G")
            {
                GameInstance.Signal("item.use","G");
                lightList.Add("G");
            }
            if(collider.GetComponent<ItemBase>().itemId == "B")
            {
                GameInstance.Signal("item.use","B");
                lightList.Add("B");
            }
        }
    }
}
