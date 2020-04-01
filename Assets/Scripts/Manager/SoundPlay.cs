using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
    {
        //Gameなど、音量を変えないシーンで用いる。音量を変えるシーンではSoundManagerが付いてる。

        public AudioSource bgm;
        private AudioSource se;
    public static AudioSource SoldierAttackBgm;
    public static AudioSource TankAttackBgm;
    public static AudioSource FighterAttackBgm;
    public static AudioSource DeathBgm;

    void Start()
        {
            bgm.volume = PlayerPrefs.GetFloat("bgmVolume",1);//こう書くと、無い場合は1になるよ
        SoldierAttackBgm = this.transform.GetChild(1).GetComponent<AudioSource>();
        TankAttackBgm = this.transform.GetChild(2).GetComponent<AudioSource>();
        FighterAttackBgm = this.transform.GetChild(3).GetComponent<AudioSource>();
        DeathBgm = this.transform.GetChild(5).GetComponent<AudioSource>();
    }
        public static void SoldierPlay()
        {
        SoldierAttackBgm.volume = PlayerPrefs.GetFloat("seVolume");
        SoldierAttackBgm.Play();
    }
    public static void TankPlay()
    {
        TankAttackBgm.volume = PlayerPrefs.GetFloat("seVolume");
        TankAttackBgm.Play();
    }
    public static void FighterPlay()
    {
        FighterAttackBgm.volume = PlayerPrefs.GetFloat("seVolume");
        FighterAttackBgm.Play();
    }
    public static void DeathPlay()
    {
        DeathBgm.volume = PlayerPrefs.GetFloat("seVolume");
        DeathBgm.Play();
    }
        //
    public void SEPlay(int i) //効果音を再生するときに呼び出される
    {
         se = this.transform.GetChild(i).GetComponent<AudioSource>();
         se.volume = PlayerPrefs.GetFloat("seVolume");
         se.Play();
    }
}
