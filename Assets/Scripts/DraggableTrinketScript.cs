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

    void Awake()
    {
        startPos = transform.position;
        image = GetComponent<Image>();
        image.sprite = level.trinketSprite;
    }

    void OnEnable()
    {
        if (level != null)
        {
            if (PlayerPrefs.HasKey(level.name))
            {
                if (PlayerPrefs.GetInt(level.name + "award1") == 1)
                {
                    image.enabled = true;
                    return;
                }
            }
        }
        image.enabled = false;
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
