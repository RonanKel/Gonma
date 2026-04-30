using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitStopMovePrototype : MonoBehaviour
{

    private bool active = false;
    private float timer = 0.0f;
    [SerializeField] private float timeUntilDrop = 30.0f;

    [SerializeField] private Vector3 start = Vector3.zero;
    [SerializeField] private Vector3 end = Vector3.zero;
    [SerializeField] private float totalTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer > timeUntilDrop)
            {
                if (timer < timeUntilDrop + totalTime)
                {
                    transform.position = Vector3.Lerp(start, end, (timer - timeUntilDrop / totalTime));
                }
                
            }
        }
    }

    void OnEnable()
    {
        Debug.Log("ENABLED");
        active = true;
    }
}
