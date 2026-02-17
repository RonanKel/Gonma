using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    private bool onHook;

    [SerializeField]
    MusicManagerScript musicManager;
    // [SerializeField]
    // GameObject alert;
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
    Masher tCScript;

    public UnityEvent fish_caught_event = new UnityEvent();
    public UnityEvent cast_event = new UnityEvent();


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        

        onHook = musicManager.onHook;

        if (!onHook) {

            // casts
            if (Input.GetKeyDown(KeyCode.Space) && !casting)
            {
                cast();
                
            }
            // if its clicked again it reels it in
            else if (Input.GetKeyDown(KeyCode.Space) && casting)
            {
                if (!alerted) {
                    uncast();
                }
                unQTE();
                
            }

            if (tCScript.done && alerted && tCScript.result){
                Debug.Log("Caught!");
                musicManager.StartMusicGame();
                onHook = true;
                unQTE();
                uncast();
            }
            else if (tCScript.done && !tCScript.result && alerted){
                Debug.Log("It got away!");
                unQTE();
                uncast();
            }
            

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
                QTE();
                //fishBite = true;
                prealert = true;
            }

            /*if (alertTimer > alertLength) {
                alert.SetActive(false);
                prealert = false;
            }*/
        }

    }

    void cast()
    {
        Debug.Log("Casting");
        cast_event.Invoke();
        casting = true;
        alertTimer = 0f;
        prealert = false;
        hook.SetActive(true);
        calcFishTime();
    }

    void recast() {
        //fishBite = false;
        fishTimer = 0f;
        alertTimer = 0f;
        prealert = false;
        calcFishTime();
    }

    void uncast()
    {
        Debug.Log("Uncasting");
        fishTimer = 0f;
        //fishBite = false;
        casting = false;
        hook.SetActive(false);

    }

    void QTE() {
        timingCircle.SetActive(true);
        alerted = true;
    }

    void unQTE(){
        timingCircle.SetActive(false);
        alerted = false;
    }

    void calcFishTime() {
        catchWarningTime = initDelayTime + Random.Range(1f,5f);
    }
}
