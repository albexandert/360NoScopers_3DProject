using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class startbuttonscript : MonoBehaviour
{
    [SerializeField] private string newGameLevel = "360NoScopers_3DProject";
    public void NewGameButton()
    {
        EditorSceneManager.LoadScene(newGameLevel);
    }
}
