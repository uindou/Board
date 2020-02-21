using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using static gameManage;

public class Pass : MonoBehaviour
{
    private bool passflug;
    public void OnClick()
    {
        if (passflug)
        {
            if (!gameManage.aiPlaying)
            {
                gameManage.Skip();
                PassWait();
            }
        }
    }
    private async void PassWait()
    {
        passflug = false;
        await Task.Delay(300);
        passflug = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        passflug = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
