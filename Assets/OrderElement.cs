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
        int value = button == 0 ? order.successValue1 : order.successValue2;
        string effect = button == 0 ? order.effects1 : order.effects2;

        GameManager.Instance.UpdateGameStatus(value, effect);

        Destroy(gameObject);
    }

}
