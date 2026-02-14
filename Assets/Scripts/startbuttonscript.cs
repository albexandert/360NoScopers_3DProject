using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class startbuttonscript : MonoBehaviour
{
    [SerializeField] private string newGameLevel = "Test Area";
    public void NewGameButton()
    {
        EditorSceneManager.LoadScene(newGameLevel);
    }
}
