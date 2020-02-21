using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class DamageAnime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DamageMotion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void DamageMotion()
    {

        while(true)
        {
            GetComponent<Image>().color = Color.red;
            Debug.Log("Damage1");
            await Task.Delay(250);
            GetComponent<Image>().color = Color.white;
            Debug.Log("Damage2");
            await Task.Delay(250);
        }
    }
}
