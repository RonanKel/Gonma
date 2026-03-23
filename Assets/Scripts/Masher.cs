// Jacks Dumb code for mashing

using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public class Masher : MonoBehaviour
{
    [SerializeField] GameObject MasherCircle;
    [SerializeField] GameObject IndicatorCircle;
    [SerializeField] float StartingScale;
    [SerializeField] AudioSource ReelSFX;

    private Vector3 posChange, negChange;

    [HideInInspector] public bool done = false;
    [HideInInspector] public bool result;

    public UnityEvent hooked_event = new UnityEvent();
    public UnityEvent reel_event = new UnityEvent();
    public UnityEvent win_event = new UnityEvent();
    public UnityEvent lose_event = new UnityEvent();



    // FixedUpdate is called once per REAL frame
    void FixedUpdate()
    {
        ColorHeat();
        BasicMash();
    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            OnMouseOver(0.1f);
            ReelSFX.pitch = 1 + IndicatorCircle.transform.localScale.x;
        }
        
    }


    void ColorHeat() {
        Color heat = new Color(1f, 0.5f, 0f, 1f);
        // IndicatorCircle.material = heat;
        // 75%> green
        // 75 - 50 yellow
        // 50 - 25 orange
        // 25 red
        if (IndicatorCircle.transform.localScale.x >= .75f)
        {
            heat = Color.green;
        }
        else if(IndicatorCircle.transform.localScale.x < .75f && IndicatorCircle.transform.localScale.x >= .50f ){
            heat = Color.yellow;
        }
        else if(IndicatorCircle.transform.localScale.x < .50f && IndicatorCircle.transform.localScale.x >= .25f ){
            heat = Color.orange;
        }
        else if(IndicatorCircle.transform.localScale.x < .25f ){
            heat = Color.red;
        }
        IndicatorCircle.GetComponent<Renderer>().material.color = heat;
    }

    void BasicMash() {
        if (!done){
            Decay(-0.005f);
            // I know this is dumb
            if (IndicatorCircle.transform.localScale.x < Vector3.zero.x)
            {
                lose();
                ReelSFX.Stop();
            }
            if (IndicatorCircle.transform.localScale.x > MasherCircle.transform.localScale.x)
            {
                win();
                ReelSFX.Stop();
            }
            }

    }



    void win()
    {
        done = true;
        result = true;
        win_event.Invoke();
    }

    void lose()
    {
        done = true;
        result = false;
        lose_event.Invoke();
    }


    void OnMouseOver(float gain){
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            posChange = new Vector3(gain, gain, gain);
            IndicatorCircle.transform.localScale += posChange;
            reel_event.Invoke();
            
        }
        
   }

        void OnEnable(){
            // Debug.Log("OnEnable");
            done = false;
            IndicatorCircle.transform.localScale = new Vector3(StartingScale, StartingScale, StartingScale);
            hooked_event.Invoke();
            ReelSFX.Play();
        }
        
        void OnDisable(){
            done = false;
            IndicatorCircle.transform.localScale = new Vector3(StartingScale, StartingScale, StartingScale);
        }

       void Decay(float loss){
            negChange = new Vector3(loss, loss, loss);
            IndicatorCircle.transform.localScale += negChange;
        
   }


}
