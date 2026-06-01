using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedkitBehavior : MonoBehaviour
{
    public BaseItemScript baseItem;

    public float healingSpeed = 3f;

    public bool isHealing = false;

    public Slider healthSlider;
    public Slider patchingSlider;

    public GameObject gameText;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BaseItemScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (baseItem.isHeld == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (healthSlider.value >= 100)
                {
                    isHealing = false;
                    gameText.SetActive(true);

                }
                else if(healthSlider.value < 100)
                {
                    isHealing = true;
                    patchingSlider.value += Time.deltaTime * healingSpeed;
                }
            }
        }
        if (gameText == true)
        {

        }
    }
}
