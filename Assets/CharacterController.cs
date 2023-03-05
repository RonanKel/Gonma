using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private bool casting = false;
    private bool fishBite = false;
    private float fishTimer = 0f;
    private float catchWarningTime;
    private bool caught = false;
    private bool alerted = false;
    private float alertTimer = 0f;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // casts
        if (Input.GetKeyDown(KeyCode.Space) && !casting) {
            cast();
        }
        // if its clicked again it reels it in
        else if (Input.GetKeyDown(KeyCode.Space) && casting) {
            //if there's a fish, it's caught
            if (fishTimer < catchWarningTime+reactionTime && fishBite) {
                Debug.Log("Caught!");
            }
            uncast();
        }
        // if the timer goes to long the fish goes away
        if (fishTimer > catchWarningTime+reactionTime) {
            fishBite = false;
            Debug.Log("U suc");
        }
        

        // casting timer
        if (casting) {
            fishTimer += Time.deltaTime;
            Debug.Log(fishTimer);
        }
        // alert timer
        if (alerted) {
            alertTimer += Time.deltaTime;
        }


        // handles alert timer and deactivation
        if (fishTimer > catchWarningTime && !alerted) {
            alert.SetActive(true);
            fishBite = true;
            alerted = true;
        }
        if (alertTimer > alertLength) {
            alert.SetActive(false);
        }
    }

    void cast() {
        Debug.Log("Casting");
        casting = true;
        alertTimer = 0f;
        alerted = false;
        hook.SetActive(true);
        catchWarningTime = initDelayTime + Random.Range(1f,5f);
    }

    void uncast() {
        Debug.Log("Uncasting");
        fishTimer = 0f;
        fishBite = false;
        casting = false;
        hook.SetActive(false);

    }
}
