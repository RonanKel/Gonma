using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatLineScript : MonoBehaviour
{
    [SerializeField] LayerMask noteMask;
    [SerializeField] string inputKey;

    private RaycastHit2D poor;
    private RaycastHit2D nice;
    private RaycastHit2D perfect;

    private MusicManagerScript mmScript;


    // Start is called before the first frame update
    void Start()
    {
        mmScript = GameObject.Find("RhythmRobot").GetComponent<MusicManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        poor = Physics2D.Raycast(transform.position + new Vector3(-.75f, 0f, 0f), Vector2.right, 1.5f, noteMask);
        nice = Physics2D.Raycast(transform.position + new Vector3(-.5f, 0f, 0f), Vector2.right, 1f, noteMask);
        perfect = Physics2D.Raycast(transform.position + new Vector3(-.2f, 0f, 0f), Vector2.right, .4f, noteMask);

        if (Input.GetKeyDown(inputKey) && perfect) {
            Destroy(perfect.transform.gameObject);
            mmScript.score += 3;
            Debug.Log("Perfect!");
        }
        else if (Input.GetKeyDown(inputKey) && nice) {
            Destroy(nice.transform.gameObject);
            mmScript.score += 2;
            Debug.Log("Nice!");
        }
        else if (Input.GetKeyDown(inputKey) && poor) {
            Destroy(poor.transform.gameObject);
            mmScript.score++;
            Debug.Log("Poor!");
        }
        else if (Input.GetKeyDown(inputKey)){
            mmScript.score -= 1;
            Debug.Log("Miss!");
        }
        
    }

    void OnDrawGizmos()
    {
    
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(-.75f, 0f, 0f) , ((transform.position + new Vector3(-.75f, 0f, 0f)) + (Vector3.right * 1.5f)));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + new Vector3(-.5f, 0f, 0f), ((transform.position + new Vector3(-.5f, 0f, 0f) + (Vector3.right * 1f))));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + new Vector3(-.2f, 0f, 0f), ((transform.position + new Vector3(-.2f, 0f, 0f)) + (Vector3.right * .4f)));

    }
}
