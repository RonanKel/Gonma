using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SongFormatScript;
using TMPro;
using UnityEngine.Events;
using System;
using System.Text.RegularExpressions;


[Serializable]
public class Line
{
    public string inputKey;
    public MinHeap beatMap = new MinHeap();
    public GameObject beatLine;
    public GameObject noteSpawner;
    public NoteSpawnerScript noteSpawnerScript;
    public GameObject noteText;

    void Start()
    {
        
    }

}



public class MusicManagerScript : MonoBehaviour
{
    public List<Line> lines = new List<Line>();

    public AudioSource music;
    [SerializeField] float bpm = 90;
    [SerializeField] List<Level> levels;
    private List<Level> selectedLevels = new List<Level>();

    [SerializeField, Header("Cat Sprite")] Sprite singingCat;
    [SerializeField] Sprite standingCat;
    [SerializeField] SpriteRenderer catSpriteRenderer;


    [SerializeField, Range(0f, 5f)]
    public float delay = 0;

    public bool onHook;
    public int score;
    public int miss_count;
    public int longest_streak;
    public int perfect_count;
    public int non_perfect_count;

    private int noteCount;
    private int winningScore;
    private bool gameRan = false;
    private Level level;
    private int lvlCount;

    public bool paused = false;
    double paused_time;

    public float bps;
    public float spb;
    //private int beatCount = 0;
    private double sSongPos;
    private double bSongPos;
    private float noteSpeed;
    private Note curr;

    // Things to hide and un-hide when starting or ending the rhythm game
    private GameObject beatLine;


    [SerializeField] private VictoryCardManagerScript vcScript;

    private double songStartTime;
    private double totalPausedTime = 0;
    private GameObject comboText;

    private GameObject noteBackground;
    private GameObject fish;

    public UnityEvent start_song_event = new UnityEvent();
    public UnityEvent win_song_event = new UnityEvent();
    public UnityEvent lose_song_event = new UnityEvent();

    [SerializeField] float poorTime = .12f;
    [SerializeField] float niceTime = .1f;
    [SerializeField] float perfectTime = .08f;

    [SerializeField] float hitDetectionZone = .2f;

    public static event Action DialogueBegin;

    public bool waiting = false;

    void update()
    {
        // Change music volume based on player preferences
        music.volume = PlayerPrefs.GetFloat("music_volume", 1f);
    }
    void PickLevel()
    {
        if (selectedLevels.Count > 0)
        {
            int lvlNum = UnityEngine.Random.Range(0, selectedLevels.Count);
            level = selectedLevels[lvlNum];
        }
        else if (lvlCount > 0)
        {

            int lvlNum = UnityEngine.Random.Range(0, lvlCount);
            level = levels[lvlNum];
            while (PlayerPrefs.HasKey(level.name + "award1") && lvlCount > 0)
            {
                if (PlayerPrefs.GetInt(level.name + "award1") != 1)
                {
                    // hasn't won yet so this is eligible
                    break;
                }
                SetCurrLevelToBackOfList();
                lvlNum = UnityEngine.Random.Range(0, lvlCount);
                level = levels[lvlNum];
            }
            if (lvlCount <= 0)
            {
                lvlNum = UnityEngine.Random.Range(0, levels.Count);
            }
            level = levels[lvlNum];
            Debug.Log(lvlNum);

        }
        else if (lvlCount <= 0 && levels.Count >= 1)
        {
            int lvlNum = UnityEngine.Random.Range(0, levels.Count);
            level = levels[lvlNum];
        }
        music.clip = level.song;
        fish.GetComponent<SpriteRenderer>().sprite = level.fishSprite;
    }




    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i].noteSpawnerScript = lines[i].noteSpawner.GetComponent<NoteSpawnerScript>();
        }
        if (PlayerPrefs.HasKey("delay"))
        {
            delay = PlayerPrefs.GetFloat("delay");
        }

        lvlCount = levels.Count;
        winningScore = -1; // to make it not give victory when starting the game
        EndMusicGame();
    }

    void Awake()
    {
        beatLine = GameObject.Find("/---BeatLines---/BeatLine");
        comboText = GameObject.Find("/---FeedBackText---/Combo");

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
            sSongPos = GetCurrentSongTime();
            bSongPos = sSongPos * bps;


            // Input detection
            for (int i = 0; i < lines.Count; i++)
            {
                if (Input.GetKeyDown(lines[i].inputKey))
                {
                    NoteScript note = lines[i].noteSpawnerScript.GetBestNote(hitDetectionZone);
                    if (note == null)
                    {
                        score -= 1;
                        miss_count++;
                        lines[i].noteSpawnerScript.ChangeStatusText("Miss!");
                        // FADE IN .1 RED, FADE OUT .5

                        comboFun(0);
                        continue;
                    }
                    bool perfect = false;
                    bool nice = false;
                    bool poor = false;
                    float err = note.err;

                    ParticleSystem perfectParticles = lines[i].noteSpawnerScript.perfectParticles;
                    ParticleSystem otherParticles = lines[i].noteSpawnerScript.otherParticles;

                    if (err <= perfectTime)
                    {
                        score += 3;
                        perfect_count++;
                        // FADE IN .1 GREEN, FADE OUT .5

                        comboFun(3);

                        // Debug.Log("Perfect!");

                        lines[i].noteSpawnerScript.ChangeStatusText("Perfect!");

                        // Emit particles
                        perfectParticles.Emit(5);
                    }
                    else if (err <= niceTime)
                    {
                        score += 2;
                        non_perfect_count++;
                        // FADE IN .1 YELLOW, FADE OUT .5
                        lines[i].noteSpawnerScript.ChangeStatusText("Nice!");
                        comboFun(2);
                        // Debug.Log("Nice!");

                        // Emit particles
                        otherParticles.Emit(5);
                    }
                    else if (err <= poorTime)
                    {
                        score++;
                        non_perfect_count++;
                        // FADE IN .1 ORANGE, FADE OUT .5
                        lines[i].noteSpawnerScript.ChangeStatusText("Poor!");
                        comboFun(1);
                        // Debug.Log("Poor!");

                        // Emit particles
                        otherParticles.Emit(5);
                    }
                    else
                    {
                        score -= 1;
                        miss_count++;
                        lines[i].noteSpawnerScript.ChangeStatusText("Miss!");
                        // FADE IN .1 RED, FADE OUT .5

                        comboFun(0);

                        // Debug.Log("Miss!");
                    }
                    note.BeDone();
                }
            }



            // Spawn Notes
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].beatMap.Count > 0 && bSongPos >= lines[i].beatMap.heap[0].beatPos + (delay * bps))
                {
                    curr = lines[i].beatMap.ExtractMin();
                    lines[i].noteSpawnerScript.PlayNote(lines[i].beatLine.transform.position, spb, (float)bSongPos);
                }
            }
        }

        else if (!music.isPlaying && gameRan && !paused)
        {
            EndMusicGame();
            gameRan = false;
        }
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        DialogueManager.OnDialogueEnded += ContinueAfterDialogue;
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
        DialogueManager.OnDialogueEnded -= ContinueAfterDialogue;
    }

    private void ContinueAfterDialogue()
    {
        // Debug.Log("Dialogue finished, continuing...");
        gameRan = true;
        Debug.Log("Time To Jam!!!");
        BuildNoteHeap();
        music.Play();

        for (int i = 0; i < lines.Count; i++)
        {
            SetLineActive(lines[i], true);
        }

        beatLine.SetActive(true);
        comboText.SetActive(true);
        noteBackground.SetActive(true);
        songStartTime = AudioSettings.dspTime;
    }


    [ContextMenu("StartMusicGame")]
    public void StartMusicGame()
    {
        PickLevel();
        fish.SetActive(true);
        DialogueBegin?.Invoke();

        start_song_event.Invoke();

        songStartTime = AudioSettings.dspTime;

        onHook = true;
        catSpriteRenderer.sprite = singingCat;
        score = 0;
        miss_count = 0;
        longest_streak = 0;
        perfect_count = 0;
        non_perfect_count = 0;
        
        // gameRan = true;
    }

    public void ReplayLastMusicGame()
    {

        music.clip = level.song;
        fish.GetComponent<SpriteRenderer>().sprite = level.fishSprite;

        onHook = true;
        catSpriteRenderer.sprite = singingCat;
        score = 0;
        miss_count = 0;
        longest_streak = 0;
        perfect_count = 0;
        non_perfect_count = 0;
        gameRan = true;

        start_song_event.Invoke();

        Debug.Log("Time To Jam!!!");
        BuildNoteHeap();
        music.Play();

        for (int i = 0; i < lines.Count; i++)
        {
            SetLineActive(lines[i], true);
        }

        beatLine.SetActive(true);
        comboText.SetActive(true);
        noteBackground.SetActive(true);
        fish.SetActive(true);

        songStartTime = AudioSettings.dspTime;
    }

    [ContextMenu("EndMusicGame")]
    public void EndMusicGame()
    {
        onHook = false;
        catSpriteRenderer.sprite = standingCat;

        bool win = false;

        totalPausedTime = 0f;

        if (winningScore < 0) { }
        else if (score >= winningScore)
        {
            Debug.Log("You Win!");
            win_song_event.Invoke();
            if (PlayerPrefs.HasKey(level.name + "award1"))
            {
                if (PlayerPrefs.GetInt(level.name + "award1") == 0)
                {
                    SetCurrLevelToBackOfList();
                }
            }

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
        if (level != null)
        {
            if (!PlayerPrefs.HasKey(level.name))
            {
                PlayerPrefs.SetInt(level.name, score);
                PlayerPrefs.SetInt(level.name, score);
            }
            else
            {
                if (score > PlayerPrefs.GetInt(level.name))
                {
                    PlayerPrefs.SetInt(level.name, score);
                }
            }
            float accuracy = (float)perfect_count / (float)(miss_count + non_perfect_count + perfect_count);
            Debug.Log(accuracy);
            vcScript.SendData(win, level.trinketSprite, score, PlayerPrefs.GetInt(level.name), longest_streak, accuracy, miss_count);

            // make other save data

            if (!PlayerPrefs.HasKey(level.name + "award1"))
            {
                PlayerPrefs.SetInt(level.name + "award1", 0);
            }
            if (!PlayerPrefs.HasKey(level.name + "award2"))
            {
                PlayerPrefs.SetInt(level.name + "award2", 0);
            }
            if (!PlayerPrefs.HasKey(level.name + "award3"))
            {
                PlayerPrefs.SetInt(level.name + "award3", 0);
            }

            if (win && PlayerPrefs.GetInt(level.name + "award1") == 0)
            {
                PlayerPrefs.SetInt(level.name + "award1", 1);
            }
            if (miss_count == 0 && PlayerPrefs.GetInt(level.name + "award2") == 0)
            {
                PlayerPrefs.SetInt(level.name + "award2", 1);
            }
            if (accuracy == 1 && PlayerPrefs.GetInt(level.name + "award3") == 0)
            {
                PlayerPrefs.SetInt(level.name + "award3", 1);
            }

        }



        //beatCount = 0;
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i].noteSpawnerScript.CleanUp();
            lines[i].beatMap.Clear();
        }

        music.Stop();

        //Debug.Log("It's Over");

        for (int i = 0; i < lines.Count; i++)
        {
            SetLineActive(lines[i], false);
        }

        beatLine.SetActive(false);
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

        StreamReader reader = new StreamReader(filePath);
        Debug.Log(filePath);

        while ((line = reader.ReadLine()) != null)
        {
            noteCount++;
            string[] data = line.Split();

            Note note = new Note(float.Parse(data[0]) - 1, int.Parse(data[2]));
            lines[int.Parse(data[1])].beatMap.Insert(note);
        }

        winningScore = (int)(noteCount * 1.2);
    }

    public void Pause()
    {
        paused = true;
        paused_time = AudioSettings.dspTime;
        music.Pause();
    }

    public void UnPause()
    {
        music.UnPause();
        totalPausedTime += AudioSettings.dspTime - paused_time;
        paused = false;
    }

    private void SetCurrLevelToBackOfList()
    {
        // replace the level with the last one
        int lvlNum = levels.IndexOf(level);
        Debug.Log("lvlNum:" + lvlNum.ToString());
        Debug.Log("lvlCount:" + lvlCount.ToString());
        Debug.Log("levels.Count:" + levels.Count);
        levels[lvlNum] = levels[lvlCount - 1];
        levels[lvlCount - 1] = level;
        lvlCount--;
    }

    public void AddSelectedLevel(Level lvl)
    {
        selectedLevels.Add(lvl);
    }

    public void RemoveSelectedLevel(Level lvl)
    {
        selectedLevels.Remove(lvl);
    }

    public double GetCurrentSongTime()
    {
        if (paused)
        {
            return AudioSettings.dspTime - songStartTime - totalPausedTime - (AudioSettings.dspTime - paused_time);
        }
        return AudioSettings.dspTime - songStartTime - totalPausedTime;
    }

    IEnumerator TextPop()
    {
        TextMeshProUGUI comboTxt = comboText.GetComponent<TextMeshProUGUI>();
        comboTxt.fontSize = comboTxt.fontSize + 5;
        // Debug.Log("TEST");
        for (int framecnt = 0; framecnt < 100; framecnt++)
        {
            yield return new WaitForEndOfFrame();
        }
        comboTxt.fontSize = comboTxt.fontSize - 5;
    }

    void comboFun(int points)
    {
        int i;
        TextMeshProUGUI comboTxt = comboText.GetComponent<TextMeshProUGUI>();
        if (points != 0)
        {
            // Trin wreck of parseing a int of the text box
            var matches = Regex.Matches(comboTxt.text, @"\d+");
            string st2 = "";
            foreach (var match in matches)
            {
                st2 += match;
                // Debug.Log(st2);
            }
            if (st2 == "")
            {
                comboTxt.text = "Combo: 1";
            }
            else
            {
                i = int.Parse(st2);
                // Debug.Log("" + i);
                ++i;
                comboTxt.text = "Combo: " + i;
                if (i > longest_streak)
                {
                    longest_streak = i;
                }
            }
            StartCoroutine(TextPop());


        }
        else
        {
            i = 0;
            comboTxt.text = "Combo: " + i;
        }
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }

    void SetLineActive(Line line, bool status)
    {
        line.beatLine.SetActive(status);
        line.noteSpawner.gameObject.SetActive(status);
        line.noteText.SetActive(status);
    }
}