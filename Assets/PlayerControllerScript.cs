using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    private bool space = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        space = Input.GetKeyDown("space");
    }

    void OnTriggerStay2D(Collider2D col) 
    {
        if (col.gameObject.layer == 6 && space) {
            Debug.Log("yay");
        }
        

    }
}
