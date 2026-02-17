// Jacks Dumb code for mashing

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Masher : MonoBehaviour
{
[SerializeField] GameObject MasherCircle;
[SerializeField] GameObject IndicatorCircle;
[SerializeField] float StartingScale;

private Vector3 posChange, negChange;

[HideInInspector] public bool done = false;
[HideInInspector] public bool result;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // FixedUpdate is called once per REAL frame
    void FixedUpdate()
    {
        ColorHeat();
        BasicMash();
    }

    // Update is called once per frame
    void Update()
    {
        if (!done){
            OnMouseOver(0.1f);
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
            if (IndicatorCircle.transform.localScale.x < Vector3.zero.x){
                lose();
            }
            if (IndicatorCircle.transform.localScale.x > MasherCircle.transform.localScale.x){
                win();
            }
            }

    }



    void win() {
        done = true;
        result = true;
    }

    void lose() {
        done = true;
        result = false;
    }


    void OnMouseOver(float gain){
        if(Input.GetMouseButtonDown(0)){
            posChange = new Vector3(gain, gain, gain);
            IndicatorCircle.transform.localScale += posChange;
        }
        
   }

        void OnEnable(){
            // Debug.Log("OnEnable");
            done = false;
            IndicatorCircle.transform.localScale = new Vector3(StartingScale, StartingScale, StartingScale);
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
