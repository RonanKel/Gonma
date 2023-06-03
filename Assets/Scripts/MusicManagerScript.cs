using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManagerScript : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] float bpm = 90;
    public bool onHook;
    
    private float bps;
    private float spb;
    private int beatCount = 0;
    private float sSongPos;
    private float bSongPos;
    private float noteSpeed;

    //private float timer = 0f;

    // Things to hide and un-hide when starting or ending the rhythm game
    private GameObject goldBeatLine;
    private GameObject magentaBeatLine;
    private GameObject tealBeatLine;
    
    private GameObject goldNoteSpawner;
    private GameObject magentaNoteSpawner;
    private GameObject tealNoteSpawner;

    private NoteSpawnerScript goldNoteSS;
    private NoteSpawnerScript magentaNoteSS;
    private NoteSpawnerScript tealNoteSS;

    private GameObject noteBackground;

    float[] beatMap = {0f,1f,2f,3f,4f,6f,8f,8.5f,9f,9.5f,10f,10.5f,11f,11.5f,12f, 10000000f, 10000000f};
    

    // Start is called before the first frame update
    void Start() 
    {
        EndMusicGame();
    }

    void Awake()
    {
        goldBeatLine = GameObject.Find("/---BeatLines---/GoldBeatLine");
        magentaBeatLine = GameObject.Find("/---BeatLines---/MagentaBeatLine");
        tealBeatLine = GameObject.Find("/---BeatLines---/TealBeatLine");

        goldNoteSpawner = GameObject.Find("/---NoteSpawners---/GoldNoteSpawner");
        magentaNoteSpawner = GameObject.Find("/---NoteSpawners---/MagentaNoteSpawner");
        tealNoteSpawner = GameObject.Find("/---NoteSpawners---/TealNoteSpawner");

        goldNoteSS = goldNoteSpawner.GetComponent<NoteSpawnerScript>();
        magentaNoteSS = magentaNoteSpawner.GetComponent<NoteSpawnerScript>();
        tealNoteSS = tealNoteSpawner.GetComponent<NoteSpawnerScript>();

        noteBackground = GameObject.Find("---Notes Stuff---/Notes Backdrop");

        bps = bpm / 60f;
        spb = 60f / bpm;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (music.isPlaying) {
            sSongPos = (float) music.time;
            bSongPos = sSongPos * bps;

            
            if (bSongPos > beatMap[beatCount]) {
                goldNoteSS.SpawnNote(goldBeatLine.transform.position, spb);
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

        music.Play();

        Debug.Log("Time To Jam!!!");

        goldBeatLine.SetActive(true);
        magentaBeatLine.SetActive(true);
        tealBeatLine.SetActive(true);

        goldNoteSpawner.SetActive(true);
        magentaNoteSpawner.SetActive(true);
        tealNoteSpawner.SetActive(true);

        noteBackground.SetActive(true);
        
    }

    [ContextMenu("EndMusicGame")]
    public void EndMusicGame() {
        onHook = false;

        beatCount = 0;

        goldNoteSS.CleanUp();
        magentaNoteSS.CleanUp();
        tealNoteSS.CleanUp();

        music.Stop();

        Debug.Log("It's Over");

        goldBeatLine.SetActive(false);
        magentaBeatLine.SetActive(false);
        tealBeatLine.SetActive(false);

        goldNoteSpawner.SetActive(false);
        magentaNoteSpawner.SetActive(false);
        tealNoteSpawner.SetActive(false);

        noteBackground.SetActive(false);
    }
}