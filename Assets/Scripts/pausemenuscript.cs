using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausemenuscript : MonoBehaviour
{

    public PlayerScript playerScript;
    public GameObject player;

    public GameObject setting;
    public bool issettingactive;

    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (playerScript.dead == false)
            {
                if (issettingactive == false)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
            }
        }
    }

    public void Pause()
    {
        setting.SetActive(true);
        issettingactive = true;
        this.GetComponent<CameraScript>().enabled = false;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        setting.SetActive(false);
        issettingactive = false;
        this.GetComponent<CameraScript>().enabled = true;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
