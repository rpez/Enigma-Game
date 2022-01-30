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

    private Order order;

    public void Awake()
    {
        
    }

    public void Initialize(Order order)
    {
        this.order = order;
        text.text = order.description;
        option1.GetComponent<OrderButton>().Initialize(order.option1);
        option2.GetComponent<OrderButton>().Initialize(order.option2);
    }

    public void OnClick(int button)
    {
        if (button == 0)
        {
            Debug.Log(order.successValue1 + ", " + order.effects1);
        } else
        {
            Debug.Log(order.successValue2 + ", " + order.effects2);
        }

        //TODO

        Destroy(gameObject);
    }

}
