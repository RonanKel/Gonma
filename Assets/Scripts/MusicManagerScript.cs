using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class MusicManagerScript : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] float bpm = 90;
    [SerializeField] string level = "TurtleLevel";
    [SerializeField] Sprite singingCat;
    [SerializeField] Sprite standingCat;
    [SerializeField] SpriteRenderer catSpriteRenderer; 
    public bool onHook;
    private MinHeap goldBeatMap;
    private MinHeap tealBeatMap;
    private MinHeap magentaBeatMap;
    
    private float bps;
    private float spb;
    //private int beatCount = 0;
    private double sSongPos;
    private double bSongPos;
    private float noteSpeed;
    private Note curr;
    private SongFormatScript songFormatScript;

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
    private GameObject fish;

    //float[] beatMap = {0f,1f,2f,3f,4f,6f,8f,8.5f,9f,9.5f,10f,10.5f,11f,11.5f,12f, 10000000f, 10000000f};
    

    // Start is called before the first frame update
    void Start() 
    {
        EndMusicGame();
    }

    void Awake()
    {
        songFormatScript = GetComponent<SongFormatScript>();

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
        fish = GameObject.Find("---Scene Management---/Fish");

        bps = bpm / 60f;
        spb = 60f / bpm;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (music.isPlaying) {
            sSongPos = (float) music.time;
            bSongPos = sSongPos * bps;

            if (goldBeatMap.Count > 0 && bSongPos >= goldBeatMap.heap[0].beatPos) {
                    while (goldBeatMap.Count > 0 && bSongPos >= goldBeatMap.heap[0].beatPos) {
                        curr = goldBeatMap.ExtractMin();
                        goldNoteSS.SpawnNote(goldBeatLine.transform.position, spb);
                    }
            }

            if (tealBeatMap.Count > 0 && bSongPos >= tealBeatMap.heap[0].beatPos) {
                    while (tealBeatMap.Count > 0 && bSongPos >= tealBeatMap.heap[0].beatPos) {
                        curr = tealBeatMap.ExtractMin();
                        tealNoteSS.SpawnNote(tealBeatLine.transform.position, spb);
                    }
            }

            if (magentaBeatMap.Count > 0 && bSongPos >= magentaBeatMap.heap[0].beatPos) {
                    while (magentaBeatMap.Count > 0 && bSongPos >= magentaBeatMap.heap[0].beatPos) {
                        curr = magentaBeatMap.ExtractMin();
                        magentaNoteSS.SpawnNote(magentaBeatLine.transform.position, spb);
                    }
            }

            /*if (beatMap.Count > 0) {
                if (bSongPos >= beatMap.heap[0].beatPos) {
                    curr = beatMap.ExtractMin();
                    switch(curr.color) {
                        case "gold":
                            goldNoteSS.SpawnNote(goldBeatLine.transform.position, spb);
                            Debug.Log(bSongPos);
                            break;
                        case "teal":
                            tealNoteSS.SpawnNote(tealBeatLine.transform.position, spb);
                            break;
                        case "magenta":
                            magentaNoteSS.SpawnNote(magentaBeatLine.transform.position, spb);
                            break;
                        
                    }
                }
            }
            else {
                Invoke("EndMusicGame", 6);
            }*/
        } else {
            EndMusicGame();
        }
    }

    [ContextMenu("StartMusicGame")]
    public void StartMusicGame() {
        onHook = true;
        catSpriteRenderer.sprite = singingCat;
        //timer = 0f;

        music.Play();

        Debug.Log("Time To Jam!!!");
        BuildNoteHeap();

        goldBeatLine.SetActive(true);
        magentaBeatLine.SetActive(true);
        tealBeatLine.SetActive(true);

        goldNoteSpawner.SetActive(true);
        magentaNoteSpawner.SetActive(true);
        tealNoteSpawner.SetActive(true);

        noteBackground.SetActive(true);
        fish.SetActive(true);
    }

    [ContextMenu("EndMusicGame")]
    public void EndMusicGame() {
        onHook = false;
        catSpriteRenderer.sprite = standingCat;

        //beatCount = 0;

        goldNoteSS.CleanUp();
        magentaNoteSS.CleanUp();
        tealNoteSS.CleanUp();

        music.Stop();

        //Debug.Log("It's Over");

        goldBeatLine.SetActive(false);
        magentaBeatLine.SetActive(false);
        tealBeatLine.SetActive(false);

        goldNoteSpawner.SetActive(false);
        magentaNoteSpawner.SetActive(false);
        tealNoteSpawner.SetActive(false);

        noteBackground.SetActive(false);
        fish.SetActive(false);
    }

    public void BuildNoteHeap() 
    {
        string filePath = "Assets/Levels/" + level + ".txt";
        string line;

        goldBeatMap = new MinHeap();
        tealBeatMap = new MinHeap();
        magentaBeatMap = new MinHeap();

        StreamReader reader = new StreamReader(filePath);

        while ((line = reader.ReadLine()) != null) {
            string[] data = line.Split();

            switch(data[1]) {
                case "gold":
                    GoldNote goldNote = new GoldNote(float.Parse(data[0]) - 1);
                    goldBeatMap.Insert(goldNote);
                    break;
                case "teal":
                    TealNote tealNote = new TealNote(float.Parse(data[0]) - 1);
                    tealBeatMap.Insert(tealNote);
                    break;
                case "magenta":
                    MagentaNote magentaNote = new MagentaNote(float.Parse(data[0]) - 1);
                    magentaBeatMap.Insert(magentaNote);
                    break;
                default:
                    Debug.Log("Faulty Note. color: "+ data[1] +". position: "+ data[0]);
                    break;
            }
        }
    }
}