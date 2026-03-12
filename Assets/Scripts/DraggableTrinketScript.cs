using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class DraggableTrinketScript : MonoBehaviour, IBeginDragHandler, IPointerDownHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("start drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag");
        //transform.position = startPos;

    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("dropped");
        transform.position = startPos;
    }
}
