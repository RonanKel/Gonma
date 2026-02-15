// Jacks Dumb code for mashing

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Masher : MonoBehaviour
{
[SerializeField] GameObject MasherCircle;
[SerializeField] GameObject IndicatorCircle;
[SerializeField] float StartingScale;
// [SerializeField] GameObject EvilCircle;
// [SerializeField] GameObject Fail;
// [SerializeField] GameObject Win;
// [SerializeField] int mash;
private Vector3 posChange, negChange;
// private bool result = false;

[HideInInspector] public bool done = false;
[HideInInspector] public bool result;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    
    }

    // Update is called once per frame
    void Update()
    {
        ColorHeat();
        BasicMash();
        // switch (mash){
        //     case 0:
        //         BasicMash();
        //         break;
        //     case 1:
        //         EvilMash();
        //         break;
        //     deafult:
        //         break;
        // }
    }


    void ColorHeat() {
        Color heat = new Color(1f, 0.5f, 0f, 1f);
        // IndicatorCircle.material = heat;
        //  75%> green
        //  75 - 50 yellow
        // 50 25 orange
        // 25 red
        // float Gcolor = IndicatorCircle.transform.localScale.x*255;
        // float Rcolor = 127 - Gcolor;
        // Debug.Log("Red: " + Rcolor );
        // Debug.Log("Green: " + Gcolor );
        // Color heat = new Color(Rcolor,Gcolor,0,IndicatorCircle.transform.localScale.x);
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
            OnMouseOver(0.1f);
            Decay(-0.0005f);
            // I know this is dumb
            if (IndicatorCircle.transform.localScale.x < Vector3.zero.x){
                lose();
            }
            if (IndicatorCircle.transform.localScale.x > MasherCircle.transform.localScale.x){
                win();
            }
            }

    }

    // void EvilMash() {
    //     if (!result){
    //         OnMouseOver(0.1f);
    //         // Decay(-0.0005f);
    //         GrowingEvil(0.0005f);
    //         // I know this is dumb
    //         if (IndicatorCircle.transform.localScale.x < EvilCircle.transform.localScale.x || EvilCircle.transform.localScale.x > Vector3.one.x){
    //             lose();
    //         }
    //         if (IndicatorCircle.transform.localScale.x > Vector3.one.x){
    //             win();
    //         }
    //         }

    // }

    // Next one Q W E inputs 
//     if (Input.GetKeyDown(KeyCode.A))
// {
//     //weapon stuff here
// }




    // void GrowingEvil(float gain){
    //     posChange = new Vector3(gain, gain, gain);
    //     EvilCircle.transform.localScale += posChange;
    // }


    void win() {
        // Win.SetActive(true);
        // Fail.SetActive(false);
        done = true;
        result = true;
    }

    void lose() {
        // Win.SetActive(false);
        // Fail.SetActive(true);
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
