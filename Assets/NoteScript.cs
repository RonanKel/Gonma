using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{

    [SerializeField] float speed;
    //[SerializeField] MusicManagerScript music;

    [SerializeField] Vector3 noteOrigin;
    [SerializeField] float poorLength;
    [SerializeField] LayerMask beatLine;

    private bool poor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        poor = Physics2D.Raycast(transform.position + noteOrigin, Vector2.left, poorLength, beatLine);
        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);

        if (transform.position.x < -30) {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown("space") && poor) {
            Destroy(gameObject);
            Debug.Log("nice");
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + noteOrigin, ((transform.position + noteOrigin) + (Vector3.left * (poorLength))));
    }

    
}
