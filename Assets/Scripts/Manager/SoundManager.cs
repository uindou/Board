using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    
   
    public void OnClick()
    {
        DataBase.bgmflug = !DataBase.bgmflug;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
