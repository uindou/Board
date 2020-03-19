using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour//音量を変更するシーンのみで用いる。それ以外はsoundPlayというスクリプトを用いる。
{   
    //スライドして値変えるやつ
    public Slider bgmSlider; 
    public Slider seSlider;

    public AudioSource bgm;
    private AudioSource se;

    void Start()
    {
        if (PlayerPrefs.HasKey("bgmVolume")) //BGM音量のPlayerPrefがある場合
        {
            bgm.volume = PlayerPrefs.GetFloat("bgmVolume");
            bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        }
        else　//PlayerPrefが無い場合(Initとかで予め設定してもいいと思う)
        {
            bgm.volume = 1;
            bgmSlider.value = 1;
        }

        if (PlayerPrefs.HasKey("seVolume"))
        {
            seSlider.value = PlayerPrefs.GetFloat("seVolume");
        }
        else　
        {
            seSlider.value = 1;
        }

    }

    public void ChangeVolume(int order) //スライダーで音量を変えたときの処理
    {
        if (order == 0) //BGMスライダーの場合
        {
            PlayerPrefs.SetFloat("bgmVolume", bgmSlider.GetComponent<Slider>().normalizedValue);
            bgm.volume = PlayerPrefs.GetFloat("bgmVolume");
        }
        else if(order == 1)　//SEスライダーの場合
        {
            PlayerPrefs.SetFloat("seVolume", seSlider.GetComponent<Slider>().normalizedValue);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SEPlay(int i) //効果音を再生するときに呼び出される
    {
        se = this.transform.GetChild(i).GetComponent<AudioSource>();
        se.volume = PlayerPrefs.GetFloat("seVolume");
        se.Play();
    }
}
