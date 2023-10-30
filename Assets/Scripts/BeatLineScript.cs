using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatLineScript : MonoBehaviour
{
    [SerializeField] float force;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

        rb = transform.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) || Input.GetAxis("Jump") > 0) { //0 is left 1 is right and 3 is middle
            rb.AddForce(transform.up * force); //move with force up.
        }
        
    }
}
