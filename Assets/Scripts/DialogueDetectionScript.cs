using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDetectionScript : MonoBehaviour
{
    public static bool inRange;
    public static bool dialogueInProgress;

    public GameObject interactUI;
    public GameObject dialogueCanvas;
    // Start is called before the first frame update
    void Start()
    {
        if (interactUI != null)
        {
            interactUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            if (interactUI != null)
            {
                interactUI.SetActive(true); // Show interact prompt
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            if (interactUI != null)
            {
                interactUI.SetActive(false); 
            }
        }
    }
    void TriggerDialogue()
    {
        dialogueCanvas.SetActive(true);
        dialogueInProgress = true;
    }
}
