using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterField : MonoBehaviour
{
    public float cursorAnimationSpeed;

    private Image background;
    private TMP_InputField inputField;
    public string encryptedLetter;
    private int index;
    private Action<string, string> onChangeCallback;
    private Action<int, bool> highlightCallback;

    private bool editing;
    private bool animate;

    public void Initalize(string letter, Action<string, string> callback, int idx, Action<int, bool> hcallback)
    {
        encryptedLetter = letter;
        onChangeCallback = callback;
        index = idx;
        highlightCallback = hcallback;
    }

    public void OnChange()
    {
        if (!editing) return;

        if (inputField.text.Length > 1)
        {
            inputField.text = inputField.text[inputField.text.Length - 1].ToString().ToLower();
        }
        onChangeCallback.Invoke(encryptedLetter, inputField.text);
    }

    public void OnSelect()
    {
        editing = true;
        StartHighlight(Color.white);
        highlightCallback.Invoke(index, true);
    }

    public void OnDeselect()
    {
        editing = false;
        StopHighlight();
        highlightCallback.Invoke(index, false);
    }

    public void StartHighlight(Color color)
    {
        animate = true;
        background.color = color;
    }

    public void StopHighlight()
    {
        animate = false;
        background.color = new Color(background.color.r, background.color.b, background.color.g, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        background = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animate)
        {
            background.color = new Color(background.color.r, background.color.b, background.color.g, Mathf.PingPong(Time.time, cursorAnimationSpeed));
        }
    }
}
