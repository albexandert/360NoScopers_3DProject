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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        playerScript.dead = false;
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
        SceneManager.LoadScene(TitleScreen);
    }
}
