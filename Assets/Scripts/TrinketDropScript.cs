using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class TrinketDropScript : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{

    

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("entered");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("left");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("up");
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped");
    }
}
