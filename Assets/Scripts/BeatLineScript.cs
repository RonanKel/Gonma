using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatLineScript : MonoBehaviour
{
    [SerializeField] float force;
    private float verticalInput;


    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
    }
    void FixedUpdate()
    {

       Vector2 movement = new Vector2(0, verticalInput);

        movement *= force * .001f;
        
        // Add the movement vector to the current position
        Vector3 newPosition = transform.position + new Vector3(movement.x, movement.y, 0);

        transform.Translate(newPosition - transform.position);
        
    }
}
