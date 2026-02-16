using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (staminaSlider.value != player.GetComponent<PlayerScript>().playerStamina)
        {
            staminaSlider.value = player.GetComponent<PlayerScript>().playerStamina;
        }
    }
}
