using System;
using Scripts.Costumize;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropCostumize : MonoBehaviour, IDropHandler
{

    [SerializeField] private GameObject panelHair;

    [SerializeField] private float positionHairY = -56.5f;
    private bool isTagWith;
    private float posHairY;
    private bool isHair;
    private bool isEyes;
    private bool isShirt;
    private bool isPants;
    private bool isDropHair;
    private bool isDropEye;
    private bool isDropPants;
    private bool isDropShirt;


    private void OnTriggerEnter2D(Collider2D other)
    {
        isTagWith = other.CompareTag(gameObject.name);

        switch (other.tag)
        {
            case nameof(EnumTags.Hair):
                isHair = true;
                isEyes = false;
                isPants = false;
                isShirt = false;
                break;
            case nameof(EnumTags.Eyes):
                isHair = false;
                isPants = false;
                isShirt = false;
                isEyes = true;
                break;
            case nameof(EnumTags.Pants):
                isPants = true;
                isEyes = false;
                isHair = false;
                isShirt = false;
                break;
            case nameof(EnumTags.Shirt):
                isShirt = true;
                isPants = false;
                isEyes = false;
                isHair = false;
                break;
            default:
                break;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (isHair && isTagWith)
            {
                if (isDropHair)
                {
                    gameObject.transform.GetChild(0).SetParent(panelHair.transform);
                }

                SetPosition(eventData);

                isDropHair = true;
                isTagWith = false;
            }

            if (isEyes && isTagWith)
            {
                if (isDropEye)
                {
                    gameObject.transform.GetChild(0).SetParent(panelHair.transform);
                }

                SetPosition(eventData);

                isDropEye = true;
                isTagWith = false;
            }

            if (isPants && isTagWith)
            {
                if (isDropPants)
                {
                    gameObject.transform.GetChild(0).SetParent(panelHair.transform);
                }

                SetPosition(eventData);

                isDropPants = true;
                isTagWith = false;
            }

            if (isShirt && isTagWith)
            {
                if (isDropShirt)
                {
                    gameObject.transform.GetChild(0).SetParent(panelHair.transform);
                }

                SetPosition(eventData);

                isDropShirt = true;
                isTagWith = false;
            }
        }
    }

    private void SetPosition(PointerEventData eventData)
    {
        Vector3 getPosition = GetComponent<RectTransform>().position;

        posHairY = getPosition.y + positionHairY;

        Vector3 posPosition = new(getPosition.x, posHairY, getPosition.z);

        eventData.pointerDrag.GetComponent<RectTransform>().transform.SetParent(GetComponent<RectTransform>().transform);

        eventData.pointerDrag.GetComponent<RectTransform>().position = posPosition;
    }
}
