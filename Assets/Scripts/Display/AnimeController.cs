using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class AnimeController : MonoBehaviour
{
    public Sprite Attack1;
    public Sprite Attack2;

    // Start is called before the first frame update
    void Start()
    {
        AttackMotion();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public async void AttackMotion()
    {

        while(true)
        {
            this.GetComponent<Image>().sprite = Attack1;
            Debug.Log("Attak1");
            await Task.Delay(250);
            this.GetComponent<Image>().sprite = Attack2;
            Debug.Log("Attak2");
            await Task.Delay(250);
        }
    }
}
