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

    [SerializeField] float poorLength = 1.5f;
    [SerializeField] float niceLength = 1f;
    [SerializeField] float perfectLength = .4f;

    [SerializeField] ParticleSystem perfectParticles;
    [SerializeField] ParticleSystem otherParticles;

    private RaycastHit2D hit;
    //private RaycastHit2D nice;
    //private RaycastHit2D perfect;

    private bool waiting = false;

    private MusicManagerScript mmScript;

    private int i = 0;

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
            Debug.Log("Detects input in line");
            bool perfect = false;
            bool nice = false;
            bool poor = false;

            NoteScript note = hit.transform.GetComponent<NoteScript>();

            Debug.Log("curr Music time: "+ mmScript.music.time.ToString());
            Debug.Log("perfect note hit time: "+ (4.0f * mmScript.spb).ToString());
            Debug.Log("difference: "+ (4.0f * mmScript.spb).ToString());

            float err = Mathf.Abs((note.creationTime + (4.0f * mmScript.spb)) - mmScript.music.time);
            Debug.Log("error: "+ err.ToString());

            if (err <= .08f)
            {
                perfect = true;
            }
            else if (err <= .1f)
            {
                nice = true;
            }
            else if (err <= .12f)
            {
                poor = true;
            }

            if (perfect) {
                Destroy(hit.transform.gameObject);
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
                Destroy(hit.transform.gameObject);
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
                Destroy(hit.transform.gameObject);
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
        }
            
    }



    void OnDrawGizmos()
    {
    
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(-(poorLength), 0f, 0f) , ((transform.position + new Vector3(-(poorLength), 0f, 0f)) + (Vector3.right * poorLength * 2f)));

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
