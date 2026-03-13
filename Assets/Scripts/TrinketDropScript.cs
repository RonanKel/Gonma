using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class TrinketDropScript : MonoBehaviour, IDropHandler, IPointerDownHandler
{   
    [SerializeField] Sprite bkgdSprite;
    private Image image;
    private Level level;
    public UnityEvent<Level> trinketAdded = new UnityEvent<Level>();
    public UnityEvent<Level> trinketRemoved = new UnityEvent<Level>();


    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
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
