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
    public float bgmVolume = 1;
    public float seVolume = 1;

    void Start()
    {
        bgmSlider.value = bgmVolume;
        seSlider.value = seVolume;
    }

    public void ChangeVolume()
    {
        bgmVolume = bgmSlider.GetComponent<Slider>().normalizedValue;
        seVolume = seSlider.GetComponent<Slider>().normalizedValue;
        Debug.Log(bgmVolume);
    }

    // Update is called once per frame
    void Update()
    {
        bgm.volume = bgmVolume;
    }
}
