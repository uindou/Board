using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject obj;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Play()
    {
        if (DataBase.bgmflug)
        {
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }
    }
    void Start()
    {
        Play();
    }
    public void OnClick()
    {
        DataBase.bgmflug = !DataBase.bgmflug;
        Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
