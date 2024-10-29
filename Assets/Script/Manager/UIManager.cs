using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject screen;

    public GameObject retro;
    public GameObject inventory;

    public GameObject win;
    public Image fade;

    public GameObject endPanel;
    // Start is called before the first frame update
    void Start()
    {
        GameInstance.Connect("game.start", OnGameStart);
        GameInstance.Connect("game.win", OnGameWin);
        GameInstance.Connect("game.end", OnGameEnd);
        fade.color = Color.black;
        fade.DOFade(0,2f);
    }

    private void OnGameEnd(IMessage msg)
    {
        endPanel.gameObject.SetActive(true);
    }

    private void OnGameWin(IMessage msg)
    {
        // win.SetActive(true);
        GameInstance.Instance.cursor.isPanel = true;
        fade.color = Color.black;
        GameInstance.CallLater(1.5f, () => {
            fade.color = new Color(0,0,0,0);
            screen.SetActive(false);
            retro.gameObject.SetActive(false);
            inventory.SetActive(false);
        });
    }

    private void OnGameStart(IMessage msg)
    {
        fade.color = Color.black;
        fade.DOFade(0,4f);
        screen.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
