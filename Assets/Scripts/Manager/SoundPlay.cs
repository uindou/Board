using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    public AudioSource bgm;
    // Update is called once per frame
    void Start()
    {
        Debug.Log(PlayerPrefs.GetFloat("bgmVolume"));
        if (PlayerPrefs.HasKey("bgmVolume"))
        {
            bgm.volume = PlayerPrefs.GetFloat("bgmVolume");
        }
        else　//音量の調整を一度もしていない場合
        {
            bgm.volume = 1;
        }
    }
}
