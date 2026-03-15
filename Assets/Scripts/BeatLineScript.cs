using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;


public class BeatLineScript : MonoBehaviour
{
    [SerializeField] LayerMask noteMask;
    [SerializeField] string inputKey;

    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] TextMeshProUGUI comboText;

    [SerializeField] float poorLength = 3f;
    [SerializeField] float niceLength = 1f;
    [SerializeField] float perfectLength = .4f;

[SerializeField] float poorTime = 1.2f;
    [SerializeField] float niceTime = .1f;
    [SerializeField] float perfectTime = .08f;



    public ParticleSystem perfectParticles;
    public ParticleSystem otherParticles;

    private RaycastHit2D hit;
    //private RaycastHit2D nice;
    //private RaycastHit2D perfect;

    private bool waiting = false;

    private MusicManagerScript mmScript;

    private int i = 0;

    bool perfect = false;
    bool nice = false;
    bool poor = false;

    // Start is called before the first frame update
    void Start()
    {
        mmScript = GameObject.Find("RhythmRobot").GetComponent<MusicManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // raycast to see which note you're hitting
        hit = Physics2D.Raycast(transform.position + new Vector3(-(poorLength), 0f, 0f), Vector2.right, poorLength * 2f, noteMask);
        

        if (Input.GetKeyDown(inputKey) && hit)
        {
            perfect = false;
            nice = false;
            poor = false;

            NoteScript note = hit.transform.GetComponent<NoteScript>();

            float err = Mathf.Abs(((float)note.spawnTime + (4.0f * mmScript.spb)) - (float)mmScript.GetCurrentSongTime());

            if (err <= perfectTime)
            {
                perfect = true;
            }
            else if (err <= niceTime)
            {
                nice = true;
            }
            else if (err <= poorTime)
            {
                poor = true;
            }

            if (perfect) {
                mmScript.score += 3;
                mmScript.perfect_count++;
                // FADE IN .1 GREEN, FADE OUT .5
                statusText.text = "Perfect!";
                statusText.color = new Color(0, 1, 0, 1f);

                statusText.canvasRenderer.SetAlpha(1f);
                statusText.CrossFadeAlpha(0f, .5f, false);
                comboFun(3);
                
                // Debug.Log("Perfect!");

                // Emit particles
                perfectParticles.Emit(5);

                // a little bit of hitstop
                if (!waiting) {
                    Time.timeScale = 0.0f;
                    StartCoroutine(Wait(.04f));
                }   
            }
            else if (nice) {
                mmScript.score += 2;
                mmScript.non_perfect_count++;
                // FADE IN .1 YELLOW, FADE OUT .5
                statusText.text = "Nice!";
                statusText.color = new Color(1, 1, 0, 1f);

                statusText.canvasRenderer.SetAlpha(1f);
                statusText.CrossFadeAlpha(0f, .5f, false);
                comboFun(2);
                // Debug.Log("Nice!");

                // Emit particles
                otherParticles.Emit(5);

                if (!waiting) {
                    Time.timeScale = 0.0f;
                    StartCoroutine(Wait(.01f));
                }
                
            }
            else if (poor) {
                mmScript.score++;
                mmScript.non_perfect_count++;
                // FADE IN .1 ORANGE, FADE OUT .5
                statusText.text = "Poor!";
                statusText.color = new Color(1f, 0.64f, 0f, 1f);

                statusText.canvasRenderer.SetAlpha(1f);
                statusText.CrossFadeAlpha(0f, .5f, false);
                comboFun(1);
                // Debug.Log("Poor!");

                // Emit particles
                otherParticles.Emit(5);
                if (!waiting) {
                    Time.timeScale = 0.0f;
                    StartCoroutine(Wait(.01f));
                }
            }
            else if (Input.GetKeyDown(inputKey)){
                // FADE IN .1 RED, FADE OUT .5
                statusText.text = "Miss!";
                statusText.color = new Color(1, 0, 0, 1f);

                statusText.canvasRenderer.SetAlpha(1f);
                statusText.CrossFadeAlpha(0f, .5f, false);
                comboFun(0);
                mmScript.score -= 1;
                mmScript.miss_count++;
                // Debug.Log("Miss!");
            }

            note.BeDone();
        }
            
    }



    void OnDrawGizmos()
    {
    
        Gizmos.color = Color.red;

        Vector3 start = transform.position + new Vector3(-poorLength, 0f, 0f);
        Vector3 end = start + Vector3.right * (poorLength * 2f);

        Gizmos.DrawLine(start, end);
    }


    void comboFun(int score)
    {
        if (score != 0){
            // Trin wreck of parseing a int of the text box
            var matches = Regex.Matches(comboText.text, @"\d+");
            string st2 ="";
            foreach(var match in matches){
                st2 += match;
                // Debug.Log(st2);
            }
            if (st2 == "")
            {
                comboText.text = "Combo: 1";
            }
            else
            {
                i = int.Parse(st2);
                // Debug.Log("" + i);
                ++i;
                comboText.text = "Combo: " + i;
                if (i > mmScript.longest_streak)
                {
                    mmScript.longest_streak = i;
                }
            }
            StartCoroutine(TextPop());
            

        }else{
            i = 0;
            comboText.text = "Combo: " + i;
        }
    }  

    IEnumerator TextPop()
    {

        comboText.fontSize = comboText.fontSize + 5;
        // Debug.Log("TEST");
        for(int framecnt = 0; framecnt < 100; framecnt++) {
                yield return new WaitForEndOfFrame();
            }
        comboText.fontSize = comboText.fontSize - 5;
                
    }



    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }
}
