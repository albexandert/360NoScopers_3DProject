using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public Camera mainCamera;

    public GameObject player; //hold the position, rotation, and scale of the player object
    public float mouseSensitivity; //hold the sensitivity multiplier of our mouse movement
    public float xRot = 0f; //hold the rotation value of our axis, and set it to 0 by default
    public float minY = -75f; //hold the lower bound of our camera rotation up/down
    public float maxY = 75f; //hold the upper bound of our camera rotation up/down

    public PlayerScript playerScript;

    public Slider sensitivitySlider;
    public Slider fovSlider;

    // Start is called before the first frame update
    void Awake()
    {
        sensitivitySlider = GameObject.Find("Sensitivity Slider").GetComponent<Slider>();
        fovSlider = GameObject.Find("FOV Slider").GetComponent<Slider>();
        if (mainCamera != null && fovSlider != null)
        {
            fovSlider.value = mainCamera.fieldOfView;
            fovSlider.onValueChanged.AddListener(ChangeFOV);
        }
        mouseSensitivity = PlayerPrefs.GetFloat("currentSensitivity", 100);
        //search through the hierarchy for an object tagged as "Player" ansd sets the transform to our player variable
        player = GameObject.FindGameObjectWithTag("Player");
        sensitivitySlider.value = mouseSensitivity / 1;

        playerScript = player.GetComponent<PlayerScript>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (playerScript.dead == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; //set the lockstate of our cursor to locked, making it stick to the center of the screen and turn invisible
            MouseLook();  //call the mouselook method every frame
        }
    }

    //MouseLook will take mouse input and rotate the player/camera accordingly 
    void MouseLook()
    {
        //set mouseX variable to the x input from mouse movement multiplied by time and sensitivity
        float mouseX = Input.GetAxis("Mouse X")  * mouseSensitivity;
        // set mouseY variable to the y input from mouse movement multiplied by time and sensitivity
        float mouseY = Input.GetAxis("Mouse Y")  * mouseSensitivity;

        xRot -= mouseY; //subtract the mouseY input from the rotation of our camera around the x axis
        xRot = Mathf.Clamp(xRot, minY, maxY); //hold the xRot value between the values of minY and maxY

        //rotate the camera locally around the x axis by the value of xRot every frame
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        player.GetComponent<Transform>().Rotate(Vector3.up * mouseX); //rotate the player object around the y axis by the value of mouseX every frame
    }

    public void AdjustSpeed(float newSpeed)
    {
        mouseSensitivity = newSpeed * 1;
    }
    public void ChangeFOV(float newFOV)
    {
        if (mainCamera != null)
        {
            mainCamera.fieldOfView = newFOV;
        }
    }
}
