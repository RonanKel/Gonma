using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeatLineMovePrototype : MonoBehaviour
{

    private bool active = false;
    private float timer = 0.0f;
    [SerializeField] private float timeUntilDrop = 30.0f;
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
                Debug.Log("cock");
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                active = false;
                timer = 0.0f;
            }
        }
    }

    void OnEnable()
    {
        Debug.Log("ENABLED");
        active = true;
    }
}
