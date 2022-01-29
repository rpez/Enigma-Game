using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OrderElement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject option1;
    [SerializeField] GameObject option2;

    public void Awake()
    {
        
    }

    public void Initialize(Order order)
    {
        text.text = order.description;
        option1.GetComponent<OrderButton>().Initialize(order.option1);
        option2.GetComponent<OrderButton>().Initialize(order.option2);
    }

    public void OnClick1()
    {
        Debug.Log("Option 1 pressed");
    }

    public void OnClick2()
    {
        Debug.Log("Option 2 pressed");
    }
}
