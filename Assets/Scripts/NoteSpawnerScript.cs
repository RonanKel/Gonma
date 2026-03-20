using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteSpawnerScript : MonoBehaviour
{

    [SerializeField] GameObject notePrefab;
    [SerializeField] MusicManagerScript music;
    [SerializeField] int noteCount = 20;
    [SerializeField] double failTime = 0.5;
    [SerializeField] TextMeshProUGUI statusText;
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

    public ParticleSystem perfectParticles;
    public ParticleSystem otherParticles;

    private int id = 0;

    // Start is called before the first frame update
    void Awake()
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
        noteScript.spawnPos = transform.position;
        noteScript.ID = id;
        id++;
        
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
            noteScript.beatLinePos = beatLinePos;
            noteScript.spb = spb;
            noteScript.beatPos = beatPos;
            noteScript.spawnTime = music.GetCurrentSongTime();
            noteScript.failTime = failTime;
            noteScript.err = 1000f;
        }
        else
        {
            Debug.Log("Note enough notes to go around, all currently active. Cannot spawn new Note");
        }
        
    }

    public void CleanUp() {

        /* This will get rid of all the notes that have been generated 
        thus reseting the song */

        for (int i = 0; i < notes.Count; i++)
        {
            NoteDone(notes[i]);
        }
    }

    public void NoteDone(GameObject note)
    {
        if (inactiveNotes.Contains(note))
        {
            return;
        }
        note.SetActive(false);
        inactiveNotes.Add(note);
        
    }

    public NoteScript GetBestNote(float hitzone)
    {
        float lowestErr = hitzone;
        NoteScript bestNote = null;
        NoteScript note;

        for (int i = 0; i < notes.Count; i++)
        {
            note = notes[i].GetComponent<NoteScript>();
            if (!inactiveNotes.Contains(note.gameObject))
            {
                if (note.err < lowestErr)
                {
                    bestNote = note;
                    lowestErr = note.err;
                }
            }  
        }

        return bestNote;
    }
    public void ChangeStatusText(string text)
    {
        if (text == "Perfect!")
        {
            statusText.text = "Perfect!";
            statusText.color = new Color(0, 1, 0, 1f);

            statusText.canvasRenderer.SetAlpha(1f);
            statusText.CrossFadeAlpha(0f, .5f, false);
        }
        if (text == "Nice!") 
        {
            statusText.text = "Nice!";
            statusText.color = new Color(1, 1, 0, 1f);

            statusText.canvasRenderer.SetAlpha(1f);
            statusText.CrossFadeAlpha(0f, .5f, false);
        }
        if (text == "Poor!") 
        {
            statusText.text = "Poor!";
            statusText.color = new Color(1f, 0.64f, 0f, 1f);

            statusText.canvasRenderer.SetAlpha(1f);
            statusText.CrossFadeAlpha(0f, .5f, false);
        }
        if (text == "Miss!") 
        {         
            statusText.text = "Miss!";
            statusText.color = new Color(1, 0, 0, 1f);

            statusText.canvasRenderer.SetAlpha(1f);
            statusText.CrossFadeAlpha(0f, .5f, false);
        }
    }
}

