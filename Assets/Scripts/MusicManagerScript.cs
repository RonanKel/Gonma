using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SongFormatScript;


public class MusicManagerScript : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] float bpm = 90;
    [SerializeField] string level = "TurtleLevel";
    [SerializeField] Sprite singingCat;
    [SerializeField] Sprite standingCat;
    [SerializeField] SpriteRenderer catSpriteRenderer; 
    public bool onHook;
    public int score;
    private MinHeap beatMap;
    //private MinHeap tealBeatMap;
    //private MinHeap magentaBeatMap;
    private int noteCount;
    private int winningScore;
    private bool gameRan = false;
    
    private float bps;
    private float spb;
    //private int beatCount = 0;
    private double sSongPos;
    private double bSongPos;
    private float noteSpeed;
    private Note curr;

    // Things to hide and un-hide when starting or ending the rhythm game
    private GameObject goldBeatLine;
    //private GameObject magentaBeatLine;
    //private GameObject tealBeatLine;
    
    //private GameObject goldNoteSpawner;
    //private GameObject magentaNoteSpawner;
    private GameObject tealNoteSpawner;

    //private NoteSpawnerScript goldNoteSS;
    //private NoteSpawnerScript magentaNoteSS;
    private NoteSpawnerScript tealNoteSS;

    private GameObject noteBackground;
    private GameObject fish;

    //float[] beatMap = {0f,1f,2f,3f,4f,6f,8f,8.5f,9f,9.5f,10f,10.5f,11f,11.5f,12f, 10000000f, 10000000f};
    

    // Start is called before the first frame update
    void Start() 
    {
        winningScore = -1; // to make it not give victory when starting the game
        EndMusicGame();
    }

    void Awake()
    {
        goldBeatLine = GameObject.Find("GoldBeatLine");
        //magentaBeatLine = GameObject.Find("/---BeatLines---/MagentaBeatLine");
        //tealBeatLine = GameObject.Find("/---BeatLines---/TealBeatLine");

        //goldNoteSpawner = GameObject.Find("/---NoteSpawners---/GoldNoteSpawner");
        //magentaNoteSpawner = GameObject.Find("/---NoteSpawners---/MagentaNoteSpawner");
        tealNoteSpawner = GameObject.Find("TealNoteSpawner");

        //goldNoteSS = goldNoteSpawner.GetComponent<NoteSpawnerScript>();
        //magentaNoteSS = magentaNoteSpawner.GetComponent<NoteSpawnerScript>();
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

            if (beatMap.Count > 0 && bSongPos >= beatMap.heap[0].beatPos) {
                    while (beatMap.Count > 0 && bSongPos >= beatMap.heap[0].beatPos) {
                        curr = beatMap.ExtractMin();
                        tealNoteSS.SpawnNote(goldBeatLine.transform.position, spb, curr.color, curr.xOffset, curr.key);
                        
                    }
            }

        } else if (!music.isPlaying && gameRan) {
            EndMusicGame();
            gameRan = false;
        }
    }

    [ContextMenu("StartMusicGame")]
    public void StartMusicGame() {
        onHook = true;
        catSpriteRenderer.sprite = singingCat;
        score = 0;
        gameRan = true;

        music.Play();

        Debug.Log("Time To Jam!!!");
        BuildNoteHeap();

        goldBeatLine.SetActive(true);
        //magentaBeatLine.SetActive(true);
        //tealBeatLine.SetActive(true);

        //goldNoteSpawner.SetActive(true);
        //magentaNoteSpawner.SetActive(true);
        tealNoteSpawner.SetActive(true);

        noteBackground.SetActive(true);
        fish.SetActive(true);
    }

    [ContextMenu("EndMusicGame")]
    public void EndMusicGame() {
        onHook = false;
        catSpriteRenderer.sprite = standingCat;
        
        if (winningScore < 0) {}
        else if (score >= winningScore) {
            Debug.Log("You Win!");
            Debug.Log("score: "+ score);
        }
        else {
            Debug.Log("You Lose...");
            Debug.Log("score: "+ score);
        }
        

        //beatCount = 0;

        //goldNoteSS.CleanUp();
        //magentaNoteSS.CleanUp();
        tealNoteSS.CleanUp();

        music.Stop();

        //Debug.Log("It's Over");

        goldBeatLine.SetActive(false);
        //magentaBeatLine.SetActive(false);
        //tealBeatLine.SetActive(false);

        //goldNoteSpawner.SetActive(false);
        //magentaNoteSpawner.SetActive(false);
        tealNoteSpawner.SetActive(false);

        noteBackground.SetActive(false);
        fish.SetActive(false);
    }

    public void BuildNoteHeap() 
    {
        string filePath = "Assets/Levels/" + level + ".txt";
        string line;
        noteCount = 0;

        beatMap = new MinHeap();
        //tealBeatMap = new MinHeap();
        //magentaBeatMap = new MinHeap();

        StreamReader reader = new StreamReader(filePath);

        while ((line = reader.ReadLine()) != null) {
            noteCount++;
            string[] data = line.Split();

            Note note = new Note(float.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3]), float.Parse(data[4]), data[5]);
            beatMap.Insert(note);


            /*switch(data[1]) {
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
                    Debug.Log("Faulty Note. color: " + data[1] + ". position: " + data[0]);
                    break;
            }*/
        }

        winningScore = (int)(noteCount * 1.2);

    }
}