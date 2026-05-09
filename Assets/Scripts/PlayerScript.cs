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
    public GameObject secondCollider;

    public bool dead = false;

    public LayerMask groundMask; //variable to hold the layers that we want our ground check to interact with

    public Rigidbody rb; //holds a reference to the RigidBody component of the player

    public float playerHP;
    public float playerStamina;
    public float staminaIncrease;
    public float staminaIncreaseWalking;
    public float staminaDecrease;
    public float jumpStaminaDecrease;
    public bool itemHeld; //let the script know if an item is being held by the player or not
    public GameObject currentItem; //hold the GameObject that the player is actively holding
    public Transform cameraPosition; //hold the Transform of the main camera
    public LayerMask interactMask; //holds the layers that out player will be able to intersct with
    public Transform targetPoint;
    public GameObject projectile;
    public Transform spawnPoint;
    public float fireRate;
    private bool canFire;
    public float firePower;
    public GameObject aimPoint;

    public static bool dialog;
    // Start is called before the first frame update
    void Start()
    {
        secondCollider = GameObject.Find("PlayerPart");
        secondCollider.SetActive(false);
        rb = GetComponent<Rigidbody>(); //sets rb to the rigidbody of this object
        crouch = false;
        itemHeld = false;
        targetPoint = GameObject.FindGameObjectWithTag("ShotTarget").transform;
        cameraPosition = GameObject.Find("Main Camera").transform;
        crouchHeight = 1f;
        canFire = true;
        fireRate = 0.2f;
        firePower = 60f;
        aimPoint = GameObject.Find("AimPointer");
    }

    // Update is called once per frame
    void Update()
    {
        if (pausemenuscript.isSettingActive == true)
        {
            return;
        }

        float x = Input.GetAxisRaw("Horizontal"); //set the x variable to the output of our horizontal axis
        float z = Input.GetAxisRaw("Vertical"); //set the z variable to the output of our vertical axis

        moveDirection = new Vector3(x, 0, z); //set moveDirection to the x and z values of our input
                                              //move the player at the speed of our variable, smoothed out by time, in the direction of our inputs
        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl) && (moveDirection.x != 0 || moveDirection.z != 0) && playerStamina >= 0)
        {
            playerStamina -= staminaDecrease * Time.deltaTime;
        }
        if (playerStamina < 100 && (x == 0 && z == 0))
        {
            playerStamina += staminaIncrease * Time.deltaTime;
        }
        else if (playerStamina < 100 && (x != 0 || z != 0))
        {
            playerStamina += staminaIncreaseWalking * Time.deltaTime;
        }
        else if (playerStamina > 100)
        {
            playerStamina = 100;
        }
        else if (playerStamina < 0)
        {
            playerStamina = 0;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            secondCollider.SetActive(false);
            crouch = true;
        }
        else
        {
            secondCollider.SetActive(true);
            crouch = false;
        }

        float TY = crouch ? crouchHeight : 2.5f;
        Vector3 targetPos = new Vector3(cameraPosition.localPosition.x, TY, cameraPosition.localPosition.z);
        cameraPosition.localPosition = Vector3.Lerp(cameraPosition.localPosition, targetPos, Time.deltaTime * 8f);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            //adds an instant force to the player in the upward direction multiplied by the value of jumpforce
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (playerStamina <= 100 && playerStamina >= 0)
            {
                playerStamina -= jumpStaminaDecrease;
            }
        }

        if (!itemHeld)
        { 
            if (Physics.Raycast(cameraPosition.position, cameraPosition.forward, out RaycastHit reach, 4.5f, interactMask))
            {
                if (reach.collider.gameObject.CompareTag("Item") || reach.collider.gameObject.CompareTag("Weapon"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        SoundManager.PlaySound(SoundType.PICKUP);
                        itemHeld = true;
                        currentItem = reach.collider.gameObject;
                        currentItem.GetComponent<BaseItemScript>().OnPickUpStarted();
                    }
                }
                else if (reach.collider.gameObject.CompareTag("Button"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        SoundManager.PlaySound(SoundType.BUTTONPRESS);
                        reach.collider.gameObject.GetComponent<SwitchesScript>().ButtonActivate();
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (itemHeld)
            {
                SoundManager.PlaySound(SoundType.DROP);
                itemHeld = false;
                currentItem.GetComponent<BaseItemScript>().OnPickUpEnded();
                currentItem = null;
            }
        }

        if (itemHeld && currentItem.CompareTag("Weapon"))
        {
           if(Input.GetKey(KeyCode.Mouse0))
           {
                if (canFire)
                {
                    SoundManager.PlaySound(SoundType.SHOOT);
                    GameObject currentProjectile =  Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
                    currentProjectile.GetComponent<Rigidbody>().AddForce(currentItem.transform.forward * firePower, ForceMode.Impulse);
                    StartCoroutine(ProjectileCooldown(fireRate));
                }
                else
                {
                    return;
                }
                
           }
        }
    }

    private void FixedUpdate()
    {
        Vector3 cameraBasedMoveDirection = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * moveDirection;
        if (playerStamina <= 100 && playerStamina > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl) && (moveDirection.x != 0 || moveDirection.z != 0))
            {
                playerStamina -= 0.085f;
                rb.MovePosition(rb.position + sprintSpeed * Time.fixedDeltaTime * cameraBasedMoveDirection);
            }
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            rb.MovePosition(rb.position + crouchSpeed * Time.fixedDeltaTime * cameraBasedMoveDirection);
        }
        else
        {
            rb.MovePosition(rb.position + baseSpeed * Time.fixedDeltaTime * cameraBasedMoveDirection);
        }
    }

    void LateUpdate()
    {
        if (itemHeld && currentItem.CompareTag("Weapon"))
        {
            currentItem.transform.LookAt(targetPoint);
        }
    }
    /*
    private void FixedUpdate()
    {
        if (!dialog)
        {
            return;
        }
    }
    */

    bool isGrounded()
    {
        //creates a ray at the players position - .9 Y, aimed downward, traveling for .2 units, and only interacting with the groundMask
        //returns true or false
        return Physics.Raycast(transform.position - new Vector3(0, .9f, 0), Vector3.down, out RaycastHit hit, .4f, groundMask);
    }

    IEnumerator ProjectileCooldown(float cooldownTime)
    {
        canFire = false;
        yield return new WaitForSeconds(cooldownTime);
        canFire = true;
    }

    public void takeDamage(float dmg)
    {
        playerHP -= dmg;
        if (playerHP <= 0)
        {
            dead = true;
        }
    }

}
