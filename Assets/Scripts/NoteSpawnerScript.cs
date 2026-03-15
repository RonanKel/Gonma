using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawnerScript : MonoBehaviour
{

    [SerializeField] GameObject notePrefab;
    [SerializeField] MusicManagerScript music;
    [SerializeField] int noteCount = 20;
    //private float timer = 0f;

    private float speed;
    private GameObject thisNote;

    /*private bool poor;
    [SerializeField] Vector3 noteOrigin;
    [SerializeField] float poorLength;
    [SerializeField] LayerMask beatLine;
    private bool nice;
    private bool great;*/

    private List<GameObject> notes = new List<GameObject>();
    private List<GameObject> inactiveNotes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject note;
        for (int i = 0; i < noteCount; i++)
        {
            note = SpawnNote();
            inactiveNotes.Add(note);
            notes.Add(note);
            note.SetActive(false);
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject SpawnNote() {

        /* This makes it so that after spawning, a note will reach the beat line
        within 4 beats of whatever bpm */
        
        thisNote = Instantiate(notePrefab, transform);
        NoteScript noteScript = thisNote.GetComponent<NoteScript>();
        noteScript.noteDone.AddListener(NoteDone);
        noteScript.spawnPos = transform.position.x;
        return thisNote;

    }

    public void PlayNote(Vector3 beatLinePos, float spb, float beatPos)
    {
        if (inactiveNotes.Count >= 1) 
        {
            GameObject thisNote = inactiveNotes[0];
            thisNote.SetActive(true);
            inactiveNotes.Remove(thisNote);
            thisNote.transform.position = transform.position;

            float speed = ((transform.position.x - beatLinePos.x) / (spb * 4));

            NoteScript noteScript = thisNote.GetComponent<NoteScript>();
            noteScript.speed = speed;
            noteScript.beatLinePos = beatLinePos.x;
            noteScript.spb = spb;
            noteScript.beatPos = beatPos;
            noteScript.spawnTime = music.GetCurrentSongTime();
        }
        else
        {
            Debug.Log("Note enough notes to go around, all currently active. Cannot spawn new Note");
        }
        
    }

    public void CleanUp() {

        /* This will get rid of all the notes that have been generated 
        thus reseting the song */

        for (int i = 0; i < notes.Count; i++) {
            NoteDone(notes[i]);
        }
    }

    public void NoteDone(GameObject note)
    {
        inactiveNotes.Add(note);
        note.SetActive(false);
    }
}