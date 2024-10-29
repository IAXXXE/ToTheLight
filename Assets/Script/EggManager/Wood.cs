using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    public GameObject G;
    public GameObject B;
    public GameObject muzhi;

    public Animator animator;

    private void Start()
    {
        animator.enabled = false;
    }

    public void OnMouseDown()
    {
        if(G.activeSelf && B.activeSelf && muzhi.activeSelf)
        {
            animator.enabled = true;
            animator.SetTrigger("make_fire");
        }
    }

    public void AfterFireOn()
    {
        animator.enabled = false;
        G.SetActive(false);
        B.SetActive(false);
        GameInstance.Signal("green.follow");
        GameInstance.Signal("blue.follow");

        GameInstance.Signal("fire.on");
    }
}
