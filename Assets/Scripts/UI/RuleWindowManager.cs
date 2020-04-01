using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleWindowManager : MonoBehaviour
{
    private int page=0;
    public List<GameObject> pages = new List<GameObject>();
    private int i;
    public GameObject leftButton;
    public GameObject rightButton;
    // Start is called before the first frame update
    void Start()
    {
        PageDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PageInc(int vec)
    {
        page += vec;

        if (page < 0) page = 0;
        else if (page > 3) page = 3;

        PageDisplay();
    }

    private void PageDisplay()
    {
        if (page == 0) leftButton.SetActive(false);
        else if (page == 3) rightButton.SetActive(false);
        else
        {
            leftButton.SetActive(true);
            rightButton.SetActive(true);
        }

        for (i=0; i<4; i++)
        {
            if (i != page)
            {
                pages[i].SetActive(false);
            }
            else
            {
                pages[i].SetActive(true);
            }   
        }
        Debug.Log(page);
    }
}
