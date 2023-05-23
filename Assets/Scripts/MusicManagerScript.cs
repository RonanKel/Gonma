using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] float songBPM;
    public bool onHook;
    [SerializeField] NoteSpawnerScript noteSpawnerScript;
    private float secPerBeat;
    private float sSongPos;
    private float bSongPos;

    [SerializeField]
    GameObject beatLine;
    [SerializeField]
    GameObject noteSpawner;
    [SerializeField]
    GameObject notebkgd;
    

    // Start is called before the first frame update
    void Awake()
    {
        secPerBeat = songBPM / 60;

    }

    // Update is called once per frame
    void Update()
    {
        if (audio.isPlaying) {
            sSongPos = (float) audio.time;
            bSongPos = sSongPos * secPerBeat;
        
            //Debug.Log(bSongPos);
        }
        
    }

    [ContextMenu("StartMusicGame")]
    public void StartMusicGame() {
        onHook = true;

        audio.Play();

        Debug.Log("Time to jam");

        beatLine.SetActive(true);
        noteSpawner.SetActive(true);
        notebkgd.SetActive(true);
        
    }
    [ContextMenu("EndMusicGame")]
    public void EndMusicGame() {
        onHook = false;

        Debug.Log("U lose Bro");

        beatLine.SetActive(false);
        noteSpawner.SetActive(false);
        notebkgd.SetActive(false);
    }
}