using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] float songBPM;
    private float secPerBear;
    private float sSongPos;
    private float bSongPos;
    

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
}
