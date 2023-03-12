using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControllerScript : MonoBehaviour
{
    private bool casting = false;
    //private bool fishBite = false;
    private float fishTimer = 0f;
    private float catchWarningTime;
    //private bool caught = false;
    private bool prealert = false;
    private float alertTimer = 0f;
    private bool alerted = false;

    [SerializeField]
    GameObject alert;
    [SerializeField]
    GameObject hook;
    //[SerializeField]
    //float reactionTime = 2f;
    [SerializeField]
    float initDelayTime = 5f;
    //[SerializeField]
    //float alertLength = 1f;
    [SerializeField]
    GameObject timingCircle;
    [SerializeField]
    BitingCircleScript tCScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // casts
        if (Input.GetKeyDown(KeyCode.Space) && !casting)
        {
            cast();
        }
        // if its clicked again it reels it in
        else if (Input.GetKeyDown(KeyCode.Space) && casting)
        {
            //if there's a fish, it's caught
            if (tCScript.isTouching && alerted) {
                Debug.Log("Caught!");
                uncast();
            }
            else if (!tCScript.isTouching && alerted) {
                Debug.Log("It got away!");
                recast();
            }
            else if (!alerted) {
                uncast();
            }
            
            unspin();
            /*else if (fishTimer < catchWarningTime+reactionTime && fishBite) {
                Debug.Log("Caught!");
            }*/
            
        }
        // if the timer goes to long the fish goes away
        /*if (fishTimer > catchWarningTime+reactionTime) {
            fishBite = false;
            Debug.Log("U suc");
        }*/
        

        // casting timer
        if (casting)
        {
            fishTimer += Time.deltaTime;
        }
        // alert timer
        if (prealert) {
            alertTimer += Time.deltaTime;
        }


        // handles alert timer and deactivation
        if (fishTimer > catchWarningTime && !prealert) {
            spin();
            fishBite = true;
            prealert = true;
        }

        /*if (alertTimer > alertLength) {
            alert.SetActive(false);
            prealert = false;
        }*/

    }

    void cast()
    {
        Debug.Log("Casting");
        casting = true;
        alertTimer = 0f;
        prealert = false;
        hook.SetActive(true);
        calcFishTime();
    }

    void recast() {
        fishBite = false;
        fishTimer = 0f;
        alertTimer = 0f;
        prealert = false;
        calcFishTime();
    }

    void uncast()
    {
        Debug.Log("Uncasting");
        fishTimer = 0f;
        fishBite = false;
        casting = false;
        hook.SetActive(false);

    }

    void spin() {
        timingCircle.SetActive(true);
        alerted = true;
    }

    void unspin(){
        timingCircle.SetActive(false);
        alerted = false;
    }

    void calcFishTime() {
        catchWarningTime = initDelayTime + Random.Range(1f,5f);
    }
}
