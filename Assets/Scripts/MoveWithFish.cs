using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithFish : MonoBehaviour
{
    [SerializeField] GameObject fish;
    private int fishState = 1;
    private int catState = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (catState!=fishState){
            //something bad get mad

        }
        if (Input.GetKeyDown("up") && !(catState>=2)) { 
            catState++;
            //set cat up a state
        }
        if (Input.GetKeyDown("down") && !(catState<=0)) {
            catState--;
            //set cat up a state
        }
        if (catState==0) { 
            transform.position = new Vector3(-1.91f, -1.88f);
            
        }
        if (catState==1) { 
            transform.position = new Vector3(-0.91f, -1.88f);
            
        }
        if (catState==2) { 
            transform.position = new Vector3(1.51f, -1.88f);
            
        } 
        if (fishState==0) { 
            fish.transform.position = new Vector3(5.8f, -4.88f);
            
        }
        if (fishState==1) { 
            fish.transform.position = new Vector3(5.8f, -3.88f);
            
        }
        if (fishState==2) { 
            fish.transform.position = new Vector3(5.8f, -2.88f);
            
        } 
    }
}
