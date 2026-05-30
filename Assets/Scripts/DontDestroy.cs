using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private static GameObject[] persistentObjects = new GameObject[4];
    public int objectIndex;

    // Start is called before the first frame update
    void Awake()
    {
        if (SceneManager.GetSceneByBuildIndex(0).isLoaded)
        {
            MakeDestroyableAgain();
            Destroy(gameObject);
        }

        if (persistentObjects[objectIndex] == null)
        {
            persistentObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (persistentObjects[objectIndex] != gameObject)
        {
            Destroy(gameObject);
        }
        
       
    }
    public void MakeDestroyableAgain()
    {
        // Moves the object out of DontDestroyOnLoad and into your current scene
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
    }

}
