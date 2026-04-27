using UnityEngine;
using TMPro;
using System.Collections;

public class TextFlashScript : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float startTime;
    [SerializeField] AnimationCurve curve;
    [SerializeField] double length;
    bool fade = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        startTime = Time.time;
    }

    void Update()
    {
        double elapsed = Time.time - startTime;
        if (!fade)
        {
            double completion = elapsed / length;
            text.color = new Color(text.color.r, text.color.g, text.color.b, curve.Evaluate((float)completion));
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }

    public void SetFade(bool fade_status)
    {
        fade = fade_status;
    }

}
