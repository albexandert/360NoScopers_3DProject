using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeybindsPanel : MonoBehaviour
{
    public GameObject keybindsPanel;

    public void OpenKeybindsPanel()
    {
        if (keybindsPanel != null)
        {
            bool isActive = keybindsPanel.activeSelf;

            keybindsPanel.SetActive(!isActive);
        }
    }
}
