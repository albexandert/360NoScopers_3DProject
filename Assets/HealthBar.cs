using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour

{
    public Slider healthSlider;
    public float maxHealth = 100;
    public float health;
    public Slider easeHealthSlider;
    private float lerpspeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(10);
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            healthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpspeed); ;
        }
    }

    void takeDamage(int damage)
    {
        health -= damage;
    }
}
