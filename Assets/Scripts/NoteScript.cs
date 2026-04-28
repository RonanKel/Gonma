using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class NoteScript : MonoBehaviour
{

    public float speed;
    public Transform beatLinePos;
    public Transform spawnPos;
    public float spb;
    [SerializeField] LayerMask failBox;
    protected MusicManagerScript mmScript;
    private SFXManager sfxScript;

    [SerializeField] protected int beatsToTravel = 4;

    public float err;
    float signedErr;

    private TextMeshProUGUI combo;

    private bool lose;

    public UnityEvent start = new UnityEvent();
    public UnityEvent<string> fail = new UnityEvent<string>();
    public UnityEvent<GameObject> noteDone = new UnityEvent<GameObject>();

    public double spawnTime;
    public float beatPos;
    public double failTime;

    public int type;
    public bool isDone = false;

    // Start is called before the first frame update
    void Start()
    {
        mmScript = GameObject.Find("RhythmRobot").GetComponent<MusicManagerScript>();
        sfxScript = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        combo = GameObject.Find("Combo").GetComponent<TextMeshProUGUI>();
        fail.AddListener(sfxScript.GetComponent<SFXManager>().Play);
    }

    // Update is called once per frame
    void Update()
    {
        GetErr();
        CalculateMovement();
        CheckFail();
    }    

    void GetErr()
    {
        signedErr = ((float)spawnTime + (beatsToTravel * mmScript.spb)) - (float)mmScript.GetCurrentSongTime();
        err = (float)Mathf.Abs(signedErr);
    }

    protected virtual void CalculateMovement()
    {
        if (!mmScript.waiting)
        {
            float ratio = (float)(mmScript.GetCurrentSongTime() - spawnTime) / (spb * beatsToTravel);
            transform.position = Vector3.LerpUnclamped(spawnPos.position, beatLinePos.position, ratio);
        }
    }

    void CheckFail()
    {
        if (signedErr <= -failTime)
        {
            mmScript.score--;
            mmScript.miss_count++;
            combo.text = "Combo: 0";
            Debug.Log("Passed!");
            fail.Invoke("fail");
            BeDone();
        }
    }

    public void BeDone()
    {
        transform.position = spawnPos.position;
        err = 10000.0f;
        noteDone.Invoke(gameObject);
        
    }
}
