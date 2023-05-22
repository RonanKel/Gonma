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
        secPerBeat = 60 / songBPM;

        
    }

    // Update is called once per frame
    void Update()
    {
        sSongPos = (float) AudioSettings.dspTime;
        bSongPos = sSongPos * secPerBeat;
        
    }

    public void StartMusicGame() {
        onHook = true;

        Debug.Log("Time to jam");

        beatLine.SetActive(true);
        noteSpawner.SetActive(true);
        notebkgd.SetActive(true);
        
    }

    public void EndMusicGame() {
        onHook = false;

        Debug.Log("U lose Bro");

        beatLine.SetActive(false);
        noteSpawner.SetActive(false);
        notebkgd.SetActive(false);
    }


}
