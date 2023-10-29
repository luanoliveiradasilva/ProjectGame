using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private Image image;
    [SerializeField] private Sprite defaultButton, pressedButton;

    public void OnPointerDown(PointerEventData eventData)
    {
        image.sprite = pressedButton;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = defaultButton;
    }
}
