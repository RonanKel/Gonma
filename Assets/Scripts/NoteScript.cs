using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{

    public float speed;
    public string type = "";
    
    [SerializeField] LayerMask failBox;
    [SerializeField] float amplitude = 2f;
    [SerializeField] float frequency = 2.37f;
    private MusicManagerScript mmScript;
    private float startTime;
    private Vector3 initialPosition;

    private bool lose;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        mmScript = GameObject.Find("RhythmRobot").GetComponent<MusicManagerScript>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (type == "") {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        else if (type == "sine") {
            float yOffset =  amplitude * Mathf.Sin(frequency * (Time.time - startTime));
            transform.Translate(Vector3.left * (speed/2) * Time.deltaTime, Space.World);
            transform.position = new Vector3(transform.position.x, initialPosition.y + yOffset, transform.position.z);
        }


        lose = Physics2D.Raycast(transform.position + new Vector3(0.75f, 0f, 0f), Vector2.left, 1.5f, failBox);

        if (lose) {
            Destroy(gameObject);
            mmScript.score--;
            Debug.Log("Passed!");
        }
    }    
}