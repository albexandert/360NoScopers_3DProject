using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector3 moveDirection; //a (x,y,z) variable to hold the direction that our player will move
    public float baseSpeed; //a decimal variable to hold our baseSpeed multiplier value
    public float sprintSpeed; //a decimal variable to hold our sprintSpeed multiplier value
    public float crouchSpeed; //a decimal variable to hold our crouchSpeed multiplier value 
    public float jumpForce; //holds the multiplier used for our jump
    public bool crouch; //bool for if player is crouching
    public float crouchHeight; //holds the crouch height

    public LayerMask groundMask; //variable to hold the layers that we want our ground check to interact with

    public Rigidbody rb; //holds a reference to the RigidBody component of the player

    public bool itemHeld; //let the script know if an item is being held by the player or not
    public GameObject currentItem; //hold the GameObject that the player is actively holding
    public Transform cameraPosition; //hold the Transform of the main camera
    public LayerMask interactMask; //holds the layers that out player will be able to intersct with
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //sets rb to the rigidbody of this object
        crouch = false;
        crouchHeight = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal"); //set the x variable to the output of our horizontal axis
        float z = Input.GetAxisRaw("Vertical"); //set the z variable to the output of our vertical axis

        moveDirection = new Vector3(x, 0, z); //set moveDirection to the x and z values of our input
        //move the player at the speed of our variable, smoothed out by time, in the direction of our inputs
        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(sprintSpeed * Time.deltaTime * moveDirection);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(crouchSpeed * Time.deltaTime * moveDirection);
        }
        else
        {
            transform.Translate(baseSpeed * Time.deltaTime * moveDirection);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            crouch = true;
        }
        else
        {
            crouch = false;
        }

        float TY = crouch ? crouchHeight : 2.5f;
        Vector3 targetPos = new Vector3(cameraPosition.localPosition.x, TY, cameraPosition.localPosition.z);
        cameraPosition.localPosition = Vector3.Lerp(cameraPosition.localPosition, targetPos, Time.deltaTime * 8f);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            //adds an instant force to the player in the upward direction multiplied by the value of jumpforce
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool isGrounded()
    {
        //creates a ray at the players position - .9 Y, aimed downward, traveling for .2 units, and only interacting with the groundMask
        //returns true or false
        return Physics.Raycast(transform.position - new Vector3(0, .9f, 0), Vector3.down, out RaycastHit hit, .4f, groundMask);
    }
}
