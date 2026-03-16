using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class NoteScript : MonoBehaviour
{

    public float speed;
    public Vector3 beatLinePos;
    public Vector3 spawnPos;
    public float spb;
    [SerializeField] LayerMask failBox;
    private MusicManagerScript mmScript;
    private SFXManager sfxScript;

    public float err;

    private TextMeshProUGUI combo;

    private bool lose;

    public UnityEvent<string> fail = new UnityEvent<string>();
    public UnityEvent<GameObject> noteDone = new UnityEvent<GameObject>();

    public double spawnTime;
    public float beatPos;
    public double failTime;

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
        
        float signedErr = ((float)spawnTime + (4.0f * mmScript.spb)) - (float)mmScript.GetCurrentSongTime();
        err = (float)Mathf.Abs(signedErr);

        if (!mmScript.waiting)
        {
            float ratio = (float)(mmScript.GetCurrentSongTime() - spawnTime) / (spb * 4);
            transform.position = Vector3.LerpUnclamped(spawnPos, beatLinePos, ratio);
        }

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
        transform.position = spawnPos;
        noteDone.Invoke(gameObject);
    }
}
