using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoving : MonoBehaviour
{
    public string nextScene;
   
    public void OnClick()
    {
        if (this.gameObject.name=="OK") DataBase.SceneName = "";
        SceneManager.LoadScene(nextScene);
    }
}
