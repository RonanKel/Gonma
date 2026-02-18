using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawnerScript : MonoBehaviour
{

    [SerializeField] GameObject note;
    [SerializeField] MusicManagerScript music;
    //private float timer = 0f;

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
        thisNote = Instantiate(note, transform);
        Renderer renderer = thisNote.GetComponent<Renderer>();
        renderer.sortingOrder = 2;
        thisNote.GetComponent<NoteScript>().speed = speed;

    }

    public void CleanUp() {

        /* This will get rid of all the notes that have been generated 
        thus reseting the song */

        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}