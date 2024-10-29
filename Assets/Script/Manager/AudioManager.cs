using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public List<AudioClip> audioList;
    public List<AudioClip> bgmList;

    private AudioSource audioSource;
    private AudioSource bgmSource;

    void Start()
    {
        //获取AudioSource组件
        audioSource = this.GetComponent<AudioSource>();
        bgmSource = transform.Find("_BGM").GetComponent<AudioSource>();

        bgmSource.Play();
    }

    public void PlayAudio(int idx)
    {
        audioSource.clip = audioList[idx];
        audioSource.Play();
    }

    public void PlayBGM(int idx)
    {
        bgmSource.clip = audioList[idx];
        bgmSource.Play();
    }

}

