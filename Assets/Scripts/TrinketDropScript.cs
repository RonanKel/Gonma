using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class TrinketDropScript : MonoBehaviour, IDropHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{   
    [SerializeField] Sprite bkgdSprite;
    private Image image;
    private Level level;
    public UnityEvent<Level> trinketAdded = new UnityEvent<Level>();
    public UnityEvent<Level> trinketRemoved = new UnityEvent<Level>();

    public UnityEvent<Level, Vector3> trinketHoverStart = new UnityEvent<Level, Vector3>();
    public UnityEvent trinketHoverEnd = new UnityEvent();


    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (level != null) {
            trinketHoverStart.Invoke(level, transform.position);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        trinketHoverEnd.Invoke();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        trinketHoverEnd.Invoke();
        if (level != null)
        {
            trinketRemoved.Invoke(level);
            image.sprite = bkgdSprite;
            level = null; 
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        level = eventData.pointerDrag.transform.GetComponent<DraggableTrinketScript>().level;
        image.sprite = level.trinketSprite;
        trinketAdded.Invoke(level);
    }
}
