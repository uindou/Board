using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeSceneMove : MonoBehaviour
{
    public GameObject nameObject;
    
    public void OnClick()
    {
        string isAI = this.name == "AIButton" ? "AI" : "";
        DataBase.SceneName = isAI+nameObject.GetComponent<Text>().text;
        SceneManager.LoadScene("Game");
    }
}
