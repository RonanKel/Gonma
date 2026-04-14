using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteSpawnerScript : MonoBehaviour
{

    [SerializeField] List<GameObject> notePrefabs = new List<GameObject>();
    [SerializeField] MusicManagerScript music;
    [SerializeField] int noteCount = 20;
    [SerializeField] double failTime = 0.5;
    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] Color color = new Color();
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
    private List<List<GameObject>> inactiveNotesLists = new List<List<GameObject>>();

    public ParticleSystem perfectParticles;
    public ParticleSystem otherParticles;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject note;
        for (int i = 0; i < notePrefabs.Count; i++) 
        {   
            List<GameObject> lst = new List<GameObject>();
            inactiveNotesLists.Add(lst);
            for (int j = 0; j < noteCount; j++)
            {
                note = SpawnNote(notePrefabs[i], i);
                notes.Add(note);
                note.SetActive(false);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject SpawnNote(GameObject note, int type) {

        /* This makes it so that after spawning, a note will reach the beat line
        within 4 beats of whatever bpm */
        
        thisNote = Instantiate(note, transform);
        NoteScript noteScript = thisNote.GetComponent<NoteScript>();
        noteScript.noteDone.AddListener(NoteDone);
        noteScript.spawnPos = transform.position;
        noteScript.type = type;

        if (type != 0) 
        {
            thisNote.GetComponent<SpriteRenderer>().color = color;
        }   
        return thisNote;

    }

    public void PlayNote(Vector3 beatLinePos, float spb, float beatPos, int type)
    {
        if (inactiveNotesLists[type].Count >= 1)
        {
            GameObject thisNote = inactiveNotesLists[type][0];
            thisNote.SetActive(true);
            inactiveNotesLists[type].Remove(thisNote);
            thisNote.transform.position = transform.position;

            NoteScript noteScript = thisNote.GetComponent<NoteScript>();
            noteScript.spawnPos = transform.position;
            noteScript.speed = speed;
            noteScript.beatLinePos = beatLinePos;
            noteScript.spb = spb;
            noteScript.beatPos = beatPos;
            noteScript.spawnTime = music.GetCurrentSongTime();
            noteScript.failTime = failTime;
            noteScript.err = 1000f;
            noteScript.start.Invoke();
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
        int type = note.GetComponent<NoteScript>().type;
        if (inactiveNotesLists[type].Contains(note))
        {
            return;
        }
        note.SetActive(false);
        inactiveNotesLists[type].Add(note);
        
    }

    public NoteScript GetBestNote(float hitzone)
    {
        float lowestErr = hitzone;
        NoteScript bestNote = null;
        NoteScript note;

        for (int i = 0; i < notes.Count; i++)
        {
            note = notes[i].GetComponent<NoteScript>();
            if (note.gameObject.activeSelf)
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

