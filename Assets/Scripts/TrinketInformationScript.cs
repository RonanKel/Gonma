using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class TrinketInformationScript : MonoBehaviour
{
    [SerializeField] GameObject trinketInfoUI;
    [SerializeField] Image trinketInfoImage;
    Level level;

    float startTime;
    bool hovering = false;

    [SerializeField] float timeUntilInfoPopup = .5f;

    void Update()
    {
        if (hovering)
        {
            if (Time.time - startTime >= timeUntilInfoPopup)
            {
                trinketInfoUI.SetActive(true);
            }
        }
    }

    public void SetHovering(bool status)
    {
        hovering = status;
        if (hovering == true)
        {
            startTime = Time.time;
        }
        else
        {
            trinketInfoUI.SetActive(false);
        }
    }

    public void SetData(Level lvl, Vector3 pos)
    {
        if (lvl != null) 
        {
            level = lvl;
            trinketInfoImage.sprite = level.fishSprite;
            transform.position = pos;
        }
    }
}
