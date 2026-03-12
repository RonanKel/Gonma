using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DraggableTrinketScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPos;

    private Image image;

    public Level level;

    void Start()
    {
        startPos = transform.position;
        image = GetComponent<Image>();
        image.sprite = level.trinketSprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //transform.position = startPos;
        image.raycastTarget = true;
        transform.position = startPos;
    }
}
