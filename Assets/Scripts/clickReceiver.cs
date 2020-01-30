using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static gameManage;

public class clickReceiver : MonoBehaviour
{
   public void OnClick()
    {
        gameManage.requestEnqueue(this.gameObject);
    }
}
