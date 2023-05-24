using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawnerScript : MonoBehaviour
{

    [SerializeField] GameObject note;
    [SerializeField] MusicManagerScript music;
    private float timer = 0f;

    private float speed;
    private GameObject thisNote;

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

    }

    public void SpawnNote(Vector3 beatLinePos, float spb) {

        /* This makes it so that after spawning, a note will reach the beat line
        within 4 beats of whatever bpm */
        speed = ((transform.position.x - beatLinePos.x) / (spb * 4));
        Debug.Log(spb);
        thisNote = Instantiate(note, transform);
        thisNote.GetComponent<NoteScript>().speed = speed;

    }
}