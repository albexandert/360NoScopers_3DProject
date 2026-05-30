using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathscreenscript : MonoBehaviour
{
    [SerializeField] private string TitleScreen = "Title Screen Test";

    public PlayerScript playerScript;
    public GameObject player;

    public GameObject deathScreen;
    public bool deathScreenShow = false;

    void Start()
    {
        deathScreen = GameObject.Find("Death Screen");
        deathScreen.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
    }

    void Update()
    {
        if (playerScript.dead == true)
        {
            Pause();
        }
    }
    public void RestartGame()
    {
        if (playerScript.itemHeld)
        {
            playerScript.itemHeld = false;
            playerScript.currentItem.GetComponent<BaseItemScript>().OnPickUpEnded();
            playerScript.currentItem = null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        playerScript.dead = false;
        playerScript.playerHP = 100;
        playerScript.playerStamina = 100;
        deathScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Player has succesfully quit the game.");
    }
    public void Pause()
    {
        deathScreen.SetActive(true);
        deathScreenShow = true;
        this.GetComponent<CameraScript>();
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }
    public void Menu()
    {
        deathScreen.SetActive(false);
        SceneManager.LoadScene(TitleScreen);
    }
}
