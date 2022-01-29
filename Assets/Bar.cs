using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] bool timer;

    private int value;
    private int maximum;
    private float time;

    void Update()
    {
        if (timer)
        {
            if (time >= 0) time -= Time.deltaTime;
            else TimeUp();
            this.value = (int)Mathf.Round(time);
        }

        UpdateVisual();
    }

    public void Set(int value, int maxValue)
    {
        this.value = value;
        this.maximum = maxValue;
    }

    private void UpdateVisual()
    {
        float proportion = this.value / maximum;
        int value = (int)Mathf.Round(proportion / 5);
        string s = "[" + new string('#', value) + new string('-', 20-value) + "]";
        text.text = s;
    }

    public void Increase(int addition)
    {
        value += addition;
    }

    void TimeUp()
    {
        Debug.Log("Time ran out!");
    }
}
