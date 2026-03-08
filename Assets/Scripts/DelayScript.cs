using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class DelayScript : MonoBehaviour
{
    float start_time;

    List<float> click_times = new List<float>();

    public UnityEvent delaySet = new UnityEvent();



    void Awake()
    {
        start_time = Time.time;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            click_times.Add(Time.time);

            if (click_times.Count >= 10)
            {
                List<float> time_from_clicks = new List<float>();
                for (int i = 0; i < click_times.Count; i++)
                {
                    time_from_clicks.Add((click_times[i] - start_time) % (2.0f/3.0f));
                }
                float totalTime = 0.0f;
                for (int i = 0; i < time_from_clicks.Count; i++)
                {
                    totalTime += time_from_clicks[i];
                }
                float average_delay = totalTime / time_from_clicks.Count;
                PlayerPrefs.SetFloat("delay", average_delay);
                Debug.Log("Delay:" + average_delay.ToString());
                delaySet.Invoke();
            }
        }
    }
}
