using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirearmScript : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnPoint;
    public float fireRate;
    public float cooldownRate;
    public float overheatLockout;
    public bool canFire;
    public bool overheating;
    public float firePower;
    public int shotHeatValue;
    public Slider overheatSlider;
    public GameObject overheatObject;
    public PlayerScript ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>(); 
        overheatObject = GameObject.Find("OverheatBarSlider");
        overheatSlider = overheatObject.GetComponent<Slider>();
        overheatObject.SetActive(false);
        spawnPoint = gameObject.GetComponentInChildren<Transform>();
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (overheatSlider.value > 0 && !overheating)
        {
            overheatSlider.value -= Time.deltaTime * cooldownRate;
        }

        if (ps.currentItem != null && ps.currentItem.CompareTag("Firearm") && !overheatObject.activeInHierarchy)
        {
            overheatObject.SetActive(true);
        }
        else if (ps.currentItem == null && overheatObject.activeInHierarchy)
        {
            overheatObject.SetActive(false);
        }
    }

    public void OnFire()
    {
       
        if (canFire && !overheating)
        {
            SoundManager.PlaySound(SoundType.SHOOT);
            GameObject currentProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
            currentProjectile.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * firePower, ForceMode.Impulse);
            overheatSlider.value += shotHeatValue;
            StartCoroutine(ProjectileCooldown(fireRate));
        }
        else
        {
            return;
        }
        if (overheatSlider.value > 90)
        {
            overheatSlider.value = 100;
            StartCoroutine(OverheatCooldown(overheatLockout));
        }
        
    }
    IEnumerator ProjectileCooldown(float cooldownTime)
    {
        canFire = false;
        yield return new WaitForSeconds(cooldownTime);
        canFire = true;
    }

    IEnumerator OverheatCooldown(float cooldownTime)
    {
        canFire = false;
        overheating = true;
        float timePassed = 0f;
        while (timePassed < cooldownTime)
        {
            float time = timePassed / cooldownTime;
            overheatSlider.value = Mathf.Lerp(100, 0, time);
            timePassed += Time.deltaTime;
            yield return null;
        }
        overheatSlider.value = 0;
        overheating = false;
        canFire = true;
    }
}
