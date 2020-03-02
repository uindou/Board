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
        DataBase.SceneName = nameObject.GetComponent<Text>().text;
        SceneManager.LoadScene("Game");
    }
}
