using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading.Tasks;

public class FadeInOut : MonoBehaviour
{
    float alfa;
    float speed = 1f;
    float red, green, blue;

    void Start()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
        alfa = 255;
    }

    void Update()
    {

        if (alfa > 0)
        {
            Debug.Log(alfa);
            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            alfa -= speed;
        }

    }

}