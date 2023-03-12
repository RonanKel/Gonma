using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControllerScript : MonoBehaviour
{
    private bool casting = false;
    private bool fishBite = false;
    private float fishTimer = 0f;
    private float catchWarningTime;
<<<<<<< HEAD:Assets/CatControllerScript.cs
    private bool alerted = false;
=======
    private bool caught = false;
    private bool prealert = false;
>>>>>>> 01e2649 (The DBD Circle Prototype):Assets/CharacterController.cs
    private float alertTimer = 0f;
    private bool alerted = false;

    [SerializeField]
    GameObject alert;
    [SerializeField]
    GameObject hook;
    [SerializeField]
    float reactionTime = 2f;
    [SerializeField]
    float initDelayTime = 5f;
    [SerializeField]
    float alertLength = 1f;
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
<<<<<<< HEAD:Assets/CatControllerScript.cs
            if (fishTimer < catchWarningTime + reactionTime && fishBite)
            {
=======
            if (tCScript.isTouching) {
>>>>>>> 01e2649 (The DBD Circle Prototype):Assets/CharacterController.cs
                Debug.Log("Caught!");
                uncast();
            }
            else if (!tCScript.isTouching) {
                Debug.Log("It got away!");
                recast();
            }
            
            unspin();
            /*else if (fishTimer < catchWarningTime+reactionTime && fishBite) {
                Debug.Log("Caught!");
            }*/
            
        }
        // if the timer goes to long the fish goes away
<<<<<<< HEAD:Assets/CatControllerScript.cs
        if (fishTimer > catchWarningTime + reactionTime)
        {
            fishBite = false;
            Debug.Log("U suc");
        }

=======
        /*if (fishTimer > catchWarningTime+reactionTime) {
            fishBite = false;
            Debug.Log("U suc");
        }*/
        
>>>>>>> 01e2649 (The DBD Circle Prototype):Assets/CharacterController.cs

        // casting timer
        if (casting)
        {
            fishTimer += Time.deltaTime;
        }
        // alert timer
<<<<<<< HEAD:Assets/CatControllerScript.cs
        if (alerted)
        {
=======
        if (prealert) {
>>>>>>> 01e2649 (The DBD Circle Prototype):Assets/CharacterController.cs
            alertTimer += Time.deltaTime;
        }


        // handles alert timer and deactivation
<<<<<<< HEAD:Assets/CatControllerScript.cs
        if (fishTimer > catchWarningTime && !alerted)
        {
            alert.SetActive(true);
=======
        if (fishTimer > catchWarningTime && !prealert) {
            spin();
>>>>>>> 01e2649 (The DBD Circle Prototype):Assets/CharacterController.cs
            fishBite = true;
            prealert = true;
        }
<<<<<<< HEAD:Assets/CatControllerScript.cs
        if (alertTimer > alertLength)
        {
=======

        /*if (alertTimer > alertLength) {
>>>>>>> 01e2649 (The DBD Circle Prototype):Assets/CharacterController.cs
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
<<<<<<< HEAD:Assets/CatControllerScript.cs
        catchWarningTime = initDelayTime + Random.Range(1f, 5f);
=======
        calcFishTime();
    }

    void recast() {
        fishBite = false;
        fishTimer = 0f;
        alertTimer = 0f;
        prealert = false;
        calcFishTime();
>>>>>>> 01e2649 (The DBD Circle Prototype):Assets/CharacterController.cs
    }

    void uncast()
    {
        Debug.Log("Uncasting");
        fishTimer = 0f;
        fishBite = false;
        casting = false;
        hook.SetActive(false);

    }
<<<<<<< HEAD:Assets/CatControllerScript.cs
}
=======

    void spin() {
        timingCircle.SetActive(true);
    }

    void unspin(){
        timingCircle.SetActive(false);
    }

    void calcFishTime() {
        catchWarningTime = initDelayTime + Random.Range(1f,5f);
    }
}
>>>>>>> 01e2649 (The DBD Circle Prototype):Assets/CharacterController.cs
