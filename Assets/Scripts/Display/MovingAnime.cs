using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAnime : MonoBehaviour
{
    public GameObject startGrid;
    public GameObject endGrid;
    public Sprite[] images = new Sprite[10];
    //public string character;
    private Vector3 start;
    private Vector3 end;
    private Vector3 move;
    public float speed;
    private float ratio=0f;

    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.GetComponent<SpriteRenderer>().sprite = images[0];
        start = startGrid.transform.GetChild(0).position;
        end = endGrid.transform.GetChild(0).position;
        move = end - start;
    }

    // Update is called once per frame
    void Update()
    {
        ratio += 0.01f*speed;
        if (ratio <= 1) {
            this.gameObject.transform.position = start + move * ratio;
        } else {
            this.gameObject.SetActive(false);
        };
        
    }
}
