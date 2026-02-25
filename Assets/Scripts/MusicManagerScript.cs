using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SongFormatScript;
using TMPro;
using UnityEngine.Events;

public class MusicManagerScript : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] float bpm = 90;
    [SerializeField] Level[] levels;
    [SerializeField] Sprite singingCat;
    [SerializeField] Sprite standingCat;
    [SerializeField] SpriteRenderer catSpriteRenderer;
    [SerializeField, Range(0f, 5f)]
    float delay = 0;

    public bool onHook;
    public int score;
    public int miss_count;
    public int longest_streak;
    
    private MinHeap goldBeatMap;
    private MinHeap tealBeatMap;
    private MinHeap magentaBeatMap;
    private int noteCount;
    private int winningScore;
    private bool gameRan = false;
    private Level level;
    private int lvlCount;

    private bool paused = false;
    double paused_time;

    private float bps;
    private float spb;
    //private int beatCount = 0;
    private double sSongPos;
    private double bSongPos;
    private float noteSpeed;
    private Note curr;

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

    [SerializeField] private VictoryCardManagerScript vcScript;


    private GameObject goldNoteText;
    private GameObject magentaNoteText;
    private GameObject tealNoteText;
    private GameObject comboText;

    private GameObject noteBackground;
    private GameObject fish;

    //float[] beatMap = {0f,1f,2f,3f,4f,6f,8f,8.5f,9f,9.5f,10f,10.5f,11f,11.5f,12f, 10000000f, 10000000f};

    public UnityEvent win_song_event = new UnityEvent();
    public UnityEvent lose_song_event = new UnityEvent();


    void PickLevel()
    {
        if (lvlCount >= 0)
        {
            int lvlNum = Random.Range(0, lvlCount - 1);
            Debug.Log(lvlNum);
            level = levels[lvlNum];
            // replace the level with the last one
            levels[lvlNum] = levels[lvlCount - 1];
            levels[lvlCount - 1] = level;
            lvlCount--;

            music.clip = level.song;
            fish.GetComponent<SpriteRenderer>().sprite = level.fishSprite;

        }
    }




    // Start is called before the first frame update
    void Start()
    {
        lvlCount = levels.Length;
        winningScore = -1; // to make it not give victory when starting the game
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

        goldNoteText = GameObject.Find("/---FeedBackText---/YText");
        magentaNoteText = GameObject.Find("/---FeedBackText---/MText");
        tealNoteText = GameObject.Find("/---FeedBackText---/CText");
        comboText = GameObject.Find("/---FeedBackText---/Combo");

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
        if (music.isPlaying)
        {
            sSongPos = (float)music.time;
            bSongPos = sSongPos * bps;

            if (goldBeatMap.Count > 0 && bSongPos >= goldBeatMap.heap[0].beatPos)
            {
                while (goldBeatMap.Count > 0 && bSongPos >= goldBeatMap.heap[0].beatPos + delay)
                {
                    curr = goldBeatMap.ExtractMin();
                    goldNoteSS.SpawnNote(goldBeatLine.transform.position, spb);

                }
            }

            if (tealBeatMap.Count > 0 && bSongPos >= tealBeatMap.heap[0].beatPos)
            {
                while (tealBeatMap.Count > 0 && bSongPos >= tealBeatMap.heap[0].beatPos + delay)
                {
                    curr = tealBeatMap.ExtractMin();
                    tealNoteSS.SpawnNote(tealBeatLine.transform.position, spb);
                }
            }

            if (magentaBeatMap.Count > 0 && bSongPos >= magentaBeatMap.heap[0].beatPos)
            {
                while (magentaBeatMap.Count > 0 && bSongPos >= magentaBeatMap.heap[0].beatPos + delay)
                {
                    curr = magentaBeatMap.ExtractMin();
                    magentaNoteSS.SpawnNote(magentaBeatLine.transform.position, spb);
                }
            }

        }
        else if (!music.isPlaying && gameRan && !paused)
        {
            EndMusicGame();
            gameRan = false;
        }
    }

    [ContextMenu("StartMusicGame")]
    public void StartMusicGame()
    {

        PickLevel();

        onHook = true;
        catSpriteRenderer.sprite = singingCat;
        score = 0;
        miss_count = 0;
        longest_streak = 0;
        gameRan = true;



        Debug.Log("Time To Jam!!!");
        BuildNoteHeap();
        music.Play();

        goldBeatLine.SetActive(true);
        magentaBeatLine.SetActive(true);
        tealBeatLine.SetActive(true);

        goldNoteSpawner.SetActive(true);
        magentaNoteSpawner.SetActive(true);
        tealNoteSpawner.SetActive(true);

        goldNoteText.SetActive(true);
        magentaNoteText.SetActive(true);
        tealNoteText.SetActive(true);
        comboText.SetActive(true);

        noteBackground.SetActive(true);
        fish.SetActive(true);
    }

    public void ReplayLastMusicGame()
    {
        int lvlNum = lvlCount;
        Debug.Log(lvlNum);
        level = levels[lvlNum];
        music.clip = level.song;
        fish.GetComponent<SpriteRenderer>().sprite = level.fishSprite;

        onHook = true;
        catSpriteRenderer.sprite = singingCat;
        score = 0;
        miss_count = 0;
        longest_streak = 0;
        gameRan = true;



        Debug.Log("Time To Jam!!!");
        BuildNoteHeap();
        music.Play();

        goldBeatLine.SetActive(true);
        magentaBeatLine.SetActive(true);
        tealBeatLine.SetActive(true);

        goldNoteSpawner.SetActive(true);
        magentaNoteSpawner.SetActive(true);
        tealNoteSpawner.SetActive(true);

        goldNoteText.SetActive(true);
        magentaNoteText.SetActive(true);
        tealNoteText.SetActive(true);
        comboText.SetActive(true);

        noteBackground.SetActive(true);
        fish.SetActive(true);
    }

    [ContextMenu("EndMusicGame")]
    public void EndMusicGame()
    {
        onHook = false;
        catSpriteRenderer.sprite = standingCat;

        bool win = false;

        if (winningScore < 0) { }
        else if (score >= winningScore)
        {
            Debug.Log("You Win!");
            win_song_event.Invoke();
            Debug.Log("score: " + score);
            win = true;
        }
        else
        {
            Debug.Log("You Lose...");
            lose_song_event.Invoke();
            Debug.Log("score: " + score);
            win = false;
        }

        // save high score
        if (!PlayerPrefs.HasKey(level.name))
        {
            PlayerPrefs.SetInt(level.name, score);
        }
        else
        {
            if (score > PlayerPrefs.GetInt(level.name))
            {
                PlayerPrefs.SetInt(level.name, score);
            }
        }

        vcScript.SendData(win, score, PlayerPrefs.GetInt(level.name), longest_streak, 0f, miss_count);


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

        goldNoteText.SetActive(false);
        magentaNoteText.SetActive(false);
        tealNoteText.SetActive(false);
        comboText.GetComponent<TextMeshProUGUI>().text = "Combo: 0";
        comboText.SetActive(false);

        noteBackground.SetActive(false);
        fish.SetActive(false);
    }

    public void BuildNoteHeap()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, level.GetLevelData());
        string line;
        noteCount = 0;

        goldBeatMap = new MinHeap();
        tealBeatMap = new MinHeap();
        magentaBeatMap = new MinHeap();

        StreamReader reader = new StreamReader(filePath);
        Debug.Log(filePath);

        while ((line = reader.ReadLine()) != null)
        {
            noteCount++;
            string[] data = line.Split();

            switch (data[1])
            {
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
            }
        }

        winningScore = (int)(noteCount * 1.2);
    }

    public void Pause()
    {
        paused = true;
        paused_time = music.time;
        music.Pause();



    }

    public void UnPause()
    {
        music.time = (float)paused_time;
        music.UnPause();
        paused = false;
    }
}