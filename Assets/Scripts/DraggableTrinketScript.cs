using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class DraggableTrinketScript : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        Debug.Log(startPos);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
    public void OnStartDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(startPos);
        transform.position = startPos;

    }
}
