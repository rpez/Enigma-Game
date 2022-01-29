using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterField : MonoBehaviour
{
    TMP_InputField inputField;
    private string encryptedLetter;
    private Action<string, string> onChangeCallback;

    public void Initalize(string letter, Action<string, string> callback)
    {
        encryptedLetter = letter;
        onChangeCallback = callback;
    }

    public void OnValueChanged()
    {
        inputField.text = inputField.text[inputField.text.Length - 1].ToString();
        onChangeCallback.Invoke(encryptedLetter, inputField.text);
    }

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
