using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick_ErrorReport()
    {
        //OSバージョン
        string versionString = SystemInfo.operatingSystem.Replace("Android ", "");

        //機種モデル
        string modelString = SystemInfo.deviceModel;

        //アプリバージョン
        string applicationVersionString = Application.version;

        Debug.Log(versionString);
        Debug.Log(modelString);
        Debug.Log(applicationVersionString);

        //初期入力ありURL ※任意のURLに変更して使って下さい
        var URL = string.Format("https://docs.google.com/forms/d/e/1FAIpQLSfYeJLiAFcpXhp40MHGtjZJ47QnxiBVoSYCQSUE8mAhrZot9A/viewform?usp=pp_url&entry.838688501={0}&entry.999538745={1}&entry.1877343710={2}",
                            versionString, modelString, applicationVersionString);

        Application.OpenURL(URL);
    }
}
