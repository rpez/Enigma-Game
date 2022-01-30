using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class OrderButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Color newColor;
    [SerializeField] Color onHoverColor;
    [SerializeField] Color normalColor;

    private Color originalColor;
    private TextMeshProUGUI tmp;
    private Outline outline;

    public void Awake()
    {
        outline = GetComponent<Outline>();
        //button = GetComponent<Button>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        //originalColor = tmp.color;
    }

    public void OnEnable()
    {
        tmp.color = newColor;
        outline.effectColor = newColor;
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
        tmp.color = normalColor;
        outline.effectColor = normalColor;
    }

    public void OnClick()
    {
        Debug.Log("Button clicked");
        //TODO call joku funktio

        Destroy(transform.parent.parent.gameObject);
    }
}
