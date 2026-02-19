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

    private RaycastHit2D poor;
    private RaycastHit2D nice;
    private RaycastHit2D perfect;

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
        poor = Physics2D.Raycast(transform.position + new Vector3(-(poorLength/2), 0f, 0f), Vector2.right, poorLength, noteMask);
        nice = Physics2D.Raycast(transform.position + new Vector3(-(niceLength/2), 0f, 0f), Vector2.right, niceLength, noteMask);
        perfect = Physics2D.Raycast(transform.position + new Vector3(-(perfectLength/2), 0f, 0f), Vector2.right, perfectLength, noteMask);

        if (Input.GetKeyDown(inputKey) && perfect) {
            Destroy(perfect.transform.gameObject);
            mmScript.score += 3;
            // FADE IN .1 GREEN, FADE OUT .5
            statusText.CrossFadeAlpha(1, .01f, false);
            statusText.color = new Color(0, 1, 0, 1f);
            statusText.CrossFadeAlpha(0, .5f, false);
            statusText.text = "Perfect!";
            comboFun(3);
            // Debug.Log("Perfect!");
        }
        else if (Input.GetKeyDown(inputKey) && nice) {
            Destroy(nice.transform.gameObject);
            mmScript.score += 2;
            // FADE IN .1 YELLOW, FADE OUT .5
            statusText.CrossFadeAlpha(1, .01f, false);
            statusText.color = new Color(1, 1, 0, 1f);
            statusText.CrossFadeAlpha(0, .5f, false);
            statusText.text = "Nice!";
            comboFun(2);
            // Debug.Log("Nice!");
        }
        else if (Input.GetKeyDown(inputKey) && poor) {
            Destroy(poor.transform.gameObject);
            mmScript.score++;
            // FADE IN .1 ORANGE, FADE OUT .5
            statusText.CrossFadeAlpha(1, .01f, false);
            statusText.color = new Color(1, .64f, 0, 1f);
            statusText.CrossFadeAlpha(0, .5f, false);
            statusText.text = "Poor!";
            comboFun(1);
            // Debug.Log("Poor!");
        }
        else if (Input.GetKeyDown(inputKey)){
            // FADE IN .1 RED, FADE OUT .5
            statusText.CrossFadeAlpha(1, .01f, false);
            statusText.color = new Color(1, 0, 0);
            statusText.CrossFadeAlpha(0, .5f, false);
            statusText.text = "Miss!";
            comboFun(0);
            mmScript.score -= 1;
            // Debug.Log("Miss!");
        }
        
    }

    void OnDrawGizmos()
    {
    
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(-(poorLength/2), 0f, 0f) , ((transform.position + new Vector3(-(poorLength/2), 0f, 0f)) + (Vector3.right * poorLength)));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + new Vector3(-(niceLength/2), 0f, 0f), ((transform.position + new Vector3(-(niceLength/2), 0f, 0f) + (Vector3.right * niceLength))));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + new Vector3(-(perfectLength/2), 0f, 0f), ((transform.position + new Vector3(-(perfectLength/2), 0f, 0f)) + (Vector3.right * perfectLength)));

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

}

