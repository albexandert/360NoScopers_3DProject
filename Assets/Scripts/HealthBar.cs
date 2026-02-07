using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour

{
    public Slider healthSlider;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != player.GetComponent<PlayerScript>().playerHP)
        {
            healthSlider.value = player.GetComponent<PlayerScript>().playerHP;
        }
    }
}
