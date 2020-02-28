using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// InputFieldを使えるようにする
using UnityEngine.UI;

public class InputFieldScript : MonoBehaviour
{

    // inputfieldを格納する変数
    InputField inputField;

    // テキストを格納する変数
    public Text text;


    // Use this for initialization
    void Start()
    {

        // InputFieldコンポーネントを格納
        inputField = GetComponent<InputField>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    // InputFieldに入力された内容をテキストに表示
    public void DisplayText()
    {
        // テキストに入力内容を表示
        text.GetComponent<Text>().text = inputField.text;
    }


}