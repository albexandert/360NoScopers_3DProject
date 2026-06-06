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
    public GameObject patchingOb;
    public Slider patchingSlider;

    // Start is called before the first frame update
    void Start()
    {
        baseItem = GetComponent<BaseItemScript>();
        patchingSlider = patchingOb.GetComponent<Slider>();
        patchingOb.SetActive(false);
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
                }
                else if(healthSlider.value < 100)
                {
                    patchingOb.SetActive(true);
                    isHealing = true;
                    patchingSlider.value += Time.deltaTime * healingSpeed;
                }
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                patchingOb.SetActive(false);
                isHealing = false;
            }
        }
    }
}
