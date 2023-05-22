using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{

    [SerializeField] float speed;
    private MusicManagerScript mmScript;

    [SerializeField] Vector3 noteOrigin;
    [SerializeField] float poorLength;
    [SerializeField] LayerMask beatLine;
    [SerializeField] LayerMask failBox;

    private bool poor;
    private bool poorLose;

    // Start is called before the first frame update
    void Start()
    {
        
        mmScript = GameObject.Find("RhythmRobot").GetComponent<MusicManagerScript>();

    }

    // Update is called once per frame
    void Update()
    {

        poor = Physics2D.Raycast(transform.position + noteOrigin, Vector2.left, poorLength, beatLine);
        poorLose = Physics2D.Raycast(transform.position + noteOrigin, Vector2.left, poorLength, failBox);

        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);

        if (transform.position.x < -30) {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown("space") && poor) {
            Destroy(gameObject);
            Debug.Log("nice");
        }

        if (poorLose) {
            Destroy(gameObject);
            mmScript.EndMusicGame();

        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + noteOrigin, ((transform.position + noteOrigin) + (Vector3.left * (poorLength))));
    }

    
}
