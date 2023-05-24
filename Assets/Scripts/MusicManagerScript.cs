using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] float bpm = 90;
    public bool onHook;
    [SerializeField] NoteSpawnerScript noteSpawnerScript;
    private float bps;
    private float spb;
    private float sSongPos;
    private float bSongPos;
    private float noteSpeed;

    private float timer = 0f;

    [SerializeField]
    GameObject beatLine;
    [SerializeField]
    GameObject noteSpawner;
    [SerializeField]
    GameObject notebkgd;
    

    // Start is called before the first frame update
    void Awake()
    {
        bps = bpm / 60f;
        spb = 60f / bpm;

    }

    // Update is called once per frame
    void Update()
    {
        if (audio.isPlaying) {
            sSongPos = (float) audio.time;
            bSongPos = sSongPos * bps;

            timer -= Time.deltaTime;
            if (timer < 0) {
                noteSpawnerScript.SpawnNote(beatLine.transform.position, spb);
                timer = 5f;
        }
        }
        
    }

    [ContextMenu("StartMusicGame")]
    public void StartMusicGame() {
        onHook = true;
        timer = 0f;

        audio.Play();

        Debug.Log("Time to jam");

        beatLine.SetActive(true);
        noteSpawner.SetActive(true);
        notebkgd.SetActive(true);
        
    }
    [ContextMenu("EndMusicGame")]
    public void EndMusicGame() {
        onHook = false;

        audio.Stop();

        Debug.Log("U lose Bro");

        beatLine.SetActive(false);
        noteSpawner.SetActive(false);
        notebkgd.SetActive(false);
    }
}