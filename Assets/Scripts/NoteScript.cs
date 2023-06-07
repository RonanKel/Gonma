using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{

    public float speed;
    [SerializeField] LayerMask beatLine;
    [SerializeField] LayerMask failBox;
    [SerializeField] string inputKey;
    private MusicManagerScript mmScript;

    [Header("Poor Condition")]
    private bool poor;
    [SerializeField] Vector3 poorNoteOrigin;
    [SerializeField] float poorLength;

    [Header("Nice Condition")]
    private bool nice;
    [SerializeField] Vector3 niceNoteOrigin;
    [SerializeField] float niceLength;

    [Header("Perfect Condition")]
    private bool perfect;
    [SerializeField] Vector3 perfectNoteOrigin;
    [SerializeField] float perfectLength;

    private bool lose;

    // Start is called before the first frame update
    void Start()
    {
        
        mmScript = GameObject.Find("RhythmRobot").GetComponent<MusicManagerScript>();

    }

    // Update is called once per frame
    void Update()
    {

        poor = Physics2D.Raycast(transform.position + poorNoteOrigin, Vector2.left, poorLength, beatLine);
        nice = Physics2D.Raycast(transform.position + niceNoteOrigin, Vector2.left, niceLength, beatLine);
        perfect = Physics2D.Raycast(transform.position + perfectNoteOrigin, Vector2.left, perfectLength, beatLine);

        lose = Physics2D.Raycast(transform.position + poorNoteOrigin, Vector2.left, poorLength, failBox);

        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < -30) {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(inputKey) && perfect) {
            Destroy(gameObject);
            Debug.Log("Perfect!");
        }
        else if (Input.GetKeyDown(inputKey) && nice) {
            Destroy(gameObject);
            Debug.Log("Nice!");
        }
        else if (Input.GetKeyDown(inputKey) && poor) {
            Destroy(gameObject);
            Debug.Log("Poor!");
        }

        if (lose) {
            Destroy(gameObject);
            Debug.Log("Miss!");

        }
        
    }

    void OnDrawGizmos()
    {
    
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + poorNoteOrigin, ((transform.position + poorNoteOrigin) + (Vector3.left * (poorLength))));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + niceNoteOrigin, ((transform.position + niceNoteOrigin) + (Vector3.left * (niceLength))));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + perfectNoteOrigin, ((transform.position + perfectNoteOrigin) + (Vector3.left * (perfectLength))));



    }

    
}
