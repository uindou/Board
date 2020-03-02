using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public GameObject targetTab;
    public int tabNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTab.transform.GetSiblingIndex() != tabNum-1)
        {
            this.GetComponent<Image>().color = Color.gray;
        }
    }

    public void OnClick()
    {
        targetTab.transform.SetAsLastSibling();
        this.GetComponent<Image>().color = Color.white;
    }
}
