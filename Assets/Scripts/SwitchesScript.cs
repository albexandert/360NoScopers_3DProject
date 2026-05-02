using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchesScript : MonoBehaviour
{
    public GameObject affectedObject;
    
    public void ButtonActivate()
    {
        affectedObject.SetActive(false);
    }
}
