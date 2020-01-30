using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    GameObject ChildObject;
    public Sprite image;
    public float duration;
    public GameObject cube1;

    // Start is called before the first frame update
    void Start()
    {
        ChildObject = transform.GetChild(0).gameObject;
        ChildObject.GetComponent<Image>().sprite = image;
    }

    // Update is called once per frame
    void Update()
    {
        //durationの時間ごとに色が変わる
        float phi = Time.time / duration * 2 * Mathf.PI;
        float amplitude = Mathf.Cos(phi) * 0.5F + 0.5F;
        //色をRGBではなくHSVで指定
        cube1.GetComponent<Image>().color = new Color(amplitude,170,amplitude);
    }
}
