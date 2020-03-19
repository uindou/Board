using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    //Gameなど、音量を変えないシーンで用いる。音量を変えるシーンではSoundManagerが付いてる。

    public AudioSource bgm;
    private AudioSource se;

    void Start()
    {
        if (PlayerPrefs.HasKey("bgmVolume"))　//BGM音量のPlayerPrefがある場合
        {
            bgm.volume = PlayerPrefs.GetFloat("bgmVolume");
        }
        else　//PlayerPrefが無い場合
        {
            bgm.volume = 1;
        }

    }

    //
    public void SEPlay(int i)　//効果音を再生するときに呼び出される
    {
        se = this.transform.GetChild(i).GetComponent<AudioSource>();
        se.volume = PlayerPrefs.GetFloat("seVolume");
        se.Play();
    }
}
