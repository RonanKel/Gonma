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
    private int beatCount = 0;
    private float sSongPos;
    private float bSongPos;
    private float noteSpeed;

    //private float timer = 0f;

    [SerializeField]
    GameObject beatLine;
    [SerializeField]
    GameObject noteSpawner;
    [SerializeField]
    GameObject notebkgd;

    float[] beatMap = {0f,1f,2f,3f,4f,6f,8f,8.5f,9f,9.5f,10f,10.5f,11f,11.5f,12f, 10000000f, 10000000f};
    

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

            if (bSongPos > beatMap[beatCount]) {
                noteSpawnerScript.SpawnNote(beatLine.transform.position, spb);
                if (beatCount < 15) {
                    beatCount++;
                }
            }
            else if (beatCount == 15) {
                Invoke("EndMusicGame", 4f);
                beatCount++;
            }
        }
    }

    [ContextMenu("StartMusicGame")]
    public void StartMusicGame() {
        onHook = true;
        //timer = 0f;

        audio.Play();

        Debug.Log("Time To Jam!!!");

        beatLine.SetActive(true);
        noteSpawner.SetActive(true);
        notebkgd.SetActive(true);
        
    }

    [ContextMenu("EndMusicGame")]
    public void EndMusicGame() {
        onHook = false;

        beatCount = 0;

        noteSpawnerScript.CleanUp();

        audio.Stop();

        Debug.Log("It's Over");

        beatLine.SetActive(false);
        noteSpawner.SetActive(false);
        notebkgd.SetActive(false);
    }
}