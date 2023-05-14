using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitingCircleScript : MonoBehaviour
{

    [SerializeField] GameObject dial;
    [SerializeField] BoxCollider2D dialCol;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] GameObject green;
    [SerializeField] BoxCollider2D greenCol;
    [SerializeField] CharacterController CharacterController;

    [HideInInspector]
    public bool isTouching;

    

    private Vector3 rotation = new Vector3(0, 0, 1);




    // Start is called before the first frame update
    void Start()
    {

        
    }

    void Update()
    {
        isTouching = greenCol.IsTouching(dialCol);
    }

    void FixedUpdate()
    {
        dial.transform.RotateAround(transform.position, rotation, rotationSpeed);

    }



}
