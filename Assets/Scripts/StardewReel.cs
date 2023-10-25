using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StardewReel : MonoBehaviour
{
    public float force;
    //private RaycastHit2D Ray;
    //[SerializeField] LayerMask mask;
    //[SerializeField] float rayLength = .4f;
    //public string inputKey;
     Rigidbody2D rb2;
         [SerializeField] GameObject line;
    [SerializeField] BoxCollider2D lineCol;
    [SerializeField] GameObject green;
    [SerializeField] BoxCollider2D greenCol;
    private int reelScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>(); //gets the objects Rigidbody
        lineCol = GetComponent<BoxCollider2D>();
        greenCol = green.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ray = Physics2D.Raycast(transform.position + new Vector3(0f, -(rayLength/2), 0f), Vector2.up, rayLength, mask);
        if (Input.GetMouseButton(0)) { //0 is left 1 is right and 3 is middle
            rb2.AddForce(transform.up * force); //move with force up.
        }
        if(lineCol.IsTouching(greenCol)){
            reelScore++;
            Debug.Log("test" + reelScore);
        }

}
    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawLine(transform.position + new Vector3(0f, -(rayLength/2), 0f), ((transform.position + new Vector3(0f, -(rayLength/2), 0f)) + (Vector3.up * rayLength)));

    // }
}
