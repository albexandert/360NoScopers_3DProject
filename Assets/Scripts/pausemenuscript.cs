using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausemenuscript : MonoBehaviour
{

    public PlayerScript playerScript;
    public GameObject player;

    public GameObject setting;
    public static bool isSettingActive = false;

    void Start()
    {
        setting = GameObject.Find("Settings Panel");
        setting.SetActive(false);
        Time.timeScale = 1f;
        player = GameObject.FindGameObjectWithTag("Player");
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
        Cursor.lockState = CursorLockMode.Confined;
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
