using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmScript : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnPoint;
    public float fireRate;
    public float overheatRate;
    private bool canFire;
    public float firePower;
    private int shots = 0;
    public int shotsTillOverheat;
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = gameObject.GetComponentInChildren<Transform>();
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFire()
    {
        if (canFire)
        {
            SoundManager.PlaySound(SoundType.SHOOT);
            GameObject currentProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
            currentProjectile.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * firePower, ForceMode.Impulse);
            StartCoroutine(ProjectileCooldown(fireRate));
        }
        else
        {
            return;
        }
        if(shots == shotsTillOverheat)
        {
            StartCoroutine(OverheatCooldown(overheatRate));
        }
    }
    IEnumerator ProjectileCooldown(float cooldownTime)
    {
        canFire = false;
        shots += 1;
        yield return new WaitForSeconds(cooldownTime);
        canFire = true;
    }

    IEnumerator OverheatCooldown(float cooldownTime)
    {
        canFire = false;
        yield return new WaitForSeconds(cooldownTime);
        shots = 0;
        canFire = true;
    }
}
