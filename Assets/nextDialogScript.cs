using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextDialogScript : MonoBehaviour
{
    int index = 2;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && transform.childCount > 1)
        {
            if (PlayerScript.dialog)
            {
                transform.GetChild(index).gameObject.SetActive(true);
                index += 1;
                if (transform.childCount == index)
                {
                    index = 2;
                    PlayerScript.dialog = false;
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
