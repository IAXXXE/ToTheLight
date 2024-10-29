using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void OnButton1Click()
    {
        transform.Find("_Bg1").gameObject.SetActive(false);
        transform.Find("_Bg2").gameObject.SetActive(true);
        GameInstance.Instance.audioManager.PlayAudio(4);
    }
    public void OnButton2Click()
    {
        transform.Find("_Bg2").gameObject.SetActive(false);
        transform.Find("_Bg3").gameObject.SetActive(true);
        GameInstance.Instance.audioManager.PlayAudio(4);
    }
    public void OnButtonClick()
    {
        GameInstance.Signal("game.start");
        GameInstance.Signal("scene.changee");
        GameInstance.Instance.audioManager.PlayAudio(4);
    }
}
