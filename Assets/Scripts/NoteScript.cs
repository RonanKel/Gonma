using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class NoteScript : MonoBehaviour
{

    public float speed;
    public float beatLinePos;
    public float spb;
    [SerializeField] LayerMask failBox;
    private MusicManagerScript mmScript;
    private SFXManager sfxScript;

    private TextMeshProUGUI combo;

    private bool lose;

    public UnityEvent<string> fail = new UnityEvent<string>();

    public double creationTime;
    private float startPosition;
    public float beatPos;

    // Start is called before the first frame update
    void Start()
    {
        mmScript = GameObject.Find("RhythmRobot").GetComponent<MusicManagerScript>();
        sfxScript = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        combo = GameObject.Find("Combo").GetComponent<TextMeshProUGUI>();
        fail.AddListener(sfxScript.GetComponent<SFXManager>().Play);
        creationTime = mmScript.GetCurrentSongTime();
        startPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        transform.position = new Vector3(startPosition + -1 * speed * (float)(mmScript.GetCurrentSongTime() - creationTime), transform.position.y, transform.position.z);
        lose = Physics2D.Raycast(transform.position + new Vector3(0.75f, 0f, 0f), Vector2.left, 1.5f, failBox);

        if (lose)
        {
            Destroy(gameObject);
            mmScript.score--;
            mmScript.miss_count++;
            combo.text = "Combo: 0";
            Debug.Log("Passed!");
            fail.Invoke("fail");
        }
    }    
}
