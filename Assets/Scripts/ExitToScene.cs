using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToScene : MonoBehaviour
{
    public string nextScene;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.CompareTag("Finish"))
            {
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene(nextScene);
            }
            
        }
    }
}
