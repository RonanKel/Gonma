using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] float songBPM;
    [SerializeField] bool onHook;
    private float secPerBear;
    private float sSongPos;
    private float bSongPos;

    

    [SerializeField]
    GameObject beatLine;
    [SerializeField]
    GameObject noteSpawner;
    [SerializeField]
    GameObject notebkgd;
    

    // Start is called before the first frame update
    void Start()
    {
        secPerBear = songBPM / 60;

        
    }

    // Update is called once per frame
    void Update()
    {
        sSongPos = (float) AudioSettings.dspTime;
        
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
