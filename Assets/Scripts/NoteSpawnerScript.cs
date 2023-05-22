using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawnerScript : MonoBehaviour
{

    [SerializeField] GameObject note;
    [SerializeField] MusicManagerScript music;
    private float timer = 0f;
    /*private bool poor;
    [SerializeField] Vector3 noteOrigin;
    [SerializeField] float poorLength;
    [SerializeField] LayerMask beatLine;
    private bool nice;
    private bool great;*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //poor = Physics2D.Raycast(transform.position + noteOrigin, Vector2.left, poorLength, beatLine);

        timer -= Time.deltaTime;
        if (timer < 0) {
            Instantiate(note, transform);
            timer = 5f;
        }
    }

}
