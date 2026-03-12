using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DraggableTrinketScript : MonoBehaviour, IBeginDragHandler, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPos;

    private Image image;

    void Start()
    {
        startPos = transform.position;
        image = GetComponent<Image>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("start drag");
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag");
        //transform.position = startPos;
        image.raycastTarget = true;
        transform.position = startPos;
    }
}
