using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCSystemScript : MonoBehaviour
{
    public bool playerDetection = false;

    public GameObject dialogTemplate;
    public GameObject canvas;
    // Update is called once per frame
    void Update()
    {
        if (playerDetection && Input.GetKeyDown(KeyCode.E) && !PlayerScript.dialog)
        {
            canvas.SetActive(true);
            PlayerScript.dialog = true;
            newDialog("Hi!");
            newDialog("This is a test!");
            canvas.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            playerDetection = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerDetection = false;
    }
    void newDialog(string text)
    {
        GameObject template_clone = Instantiate(dialogTemplate, dialogTemplate.transform);
        template_clone.transform.parent = canvas.transform;
        template_clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
    }
}
