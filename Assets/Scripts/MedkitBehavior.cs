using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedkitBehavior : MonoBehaviour
{
    public BaseItemScript baseItem;

    public float healingSpeed;

    public bool isHealing = false;

    public PlayerScript ps;
    public GameObject patchingOb;
    public Slider patchingSlider;

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        patchingOb = ps.patchingSlider;
        patchingSlider = patchingOb.GetComponent<Slider>();
        baseItem = GetComponent<BaseItemScript>();
        patchingOb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (baseItem.isHeld == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (ps.playerHP >= 100)
                {
                    isHealing = false;
                }
                else if(ps.playerHP < 100)
                {
                    patchingOb.SetActive(true);
                    isHealing = true;
                    patchingSlider.value += Time.deltaTime * healingSpeed;
                    if (patchingSlider.value >= 100)
                    {
                        ps.playerHP = 100;
                        baseItem.OnPickUpEnded();
                        patchingSlider.value = 0;
                        patchingOb.SetActive(false);
                        Destroy(gameObject);
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                patchingOb.SetActive(false);
                patchingSlider.value = 0;
                isHealing = false;
            }
        }
    }
}
