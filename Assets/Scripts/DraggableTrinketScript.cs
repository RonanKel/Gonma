using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;



public class DraggableTrinketScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Vector3 startPos;

    private Image image;

    public Level level;

    public UnityEvent<Level, Vector3> trinketHoverStart = new UnityEvent<Level, Vector3>();
    public UnityEvent trinketHoverEnd = new UnityEvent();

    void Awake()
    {
        startPos = transform.position;
        image = GetComponent<Image>();
        image.sprite = level.trinketSprite;
    }

    void Start()
    {
        startPos = transform.position;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        trinketHoverStart.Invoke(level, transform.position);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        trinketHoverEnd.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        trinketHoverEnd.Invoke();
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
