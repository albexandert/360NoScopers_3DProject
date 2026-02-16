using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausemenuscript : MonoBehaviour
{

    public PlayerScript playerScript;
    public GameObject player;

    public GameObject setting;
    public bool isSettingActive;

    void Start()
    {
        Time.timeScale = 1f;
        playerScript = player.GetComponent<PlayerScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (playerScript.dead == false)
            {
                if (isSettingActive == false)
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
        isSettingActive = true;
        this.GetComponent<CameraScript>().enabled = false;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        setting.SetActive(false);
        isSettingActive = false;
        this.GetComponent<CameraScript>().enabled = true;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
