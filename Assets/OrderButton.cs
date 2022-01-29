using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class OrderButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Color onHoverColor;

    private Color originalColor;
    private TextMeshProUGUI tmp;
    private Outline outline;

    public void Awake()
    {
        outline = GetComponent<Outline>();
        //button = GetComponent<Button>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        originalColor = tmp.color;
    }

    public void Initialize(string text) //TODO: add other effects
    {
        tmp.text = text;
        Debug.Log(tmp.text + ", " + text);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tmp.color = onHoverColor;
        outline.effectColor = onHoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tmp.color = originalColor;
        outline.effectColor = originalColor;
    }

    public void OnClick()
    {
        Debug.Log("Button clicked");
    }
}
