using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider bgmSlider;
    public Slider seSlider;
    public AudioSource bgm;

    void Start()
    {
        if (PlayerPrefs.HasKey("bgmVolume")) 
        {
            bgm.volume = PlayerPrefs.GetFloat("bgmVolume");
            bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        }
        else　//音量の調整を一度もしていない場合
        {
            bgm.volume = 1;
            bgmSlider.value = 1;
        }
        
    }

    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat("bgmVolume", bgmSlider.GetComponent<Slider>().normalizedValue);
    }

    // Update is called once per frame
    void Update()
    {
        bgm.volume = PlayerPrefs.GetFloat("bgmVolume");
    }
}
