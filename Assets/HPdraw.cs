using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPdraw : MonoBehaviour
{
    public int maxHP;
    public int HP;
    public Sprite heart;
    public Sprite brokenheart;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        for (i=0; i<3; i++)
        {
            if (i < HP)
            {
                this.transform.GetChild(i).GetComponent<Image>().sprite = heart;
            }
            else if(i<maxHP)
            {
                this.transform.GetChild(i).GetComponent<Image>().sprite = brokenheart;
            }
            else
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
