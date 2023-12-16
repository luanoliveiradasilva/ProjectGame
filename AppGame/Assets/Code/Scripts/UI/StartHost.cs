using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartHost : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private List<Image> listComponent;
    public void OnPointerDown(PointerEventData eventData)
    {

            var teste = gameObject.GetComponentsInChildren<Image>();

            /* listComponent.Add(); */
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

}
