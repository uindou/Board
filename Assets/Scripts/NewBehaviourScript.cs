using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    GameObject ChildObject;
    public Sprite image;

    // Start is called before the first frame update
    void Start()
    {
        ChildObject = transform.GetChild(0).gameObject;
        ChildObject.GetComponent<Image>().sprite = image;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
