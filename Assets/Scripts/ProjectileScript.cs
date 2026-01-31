using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }
}
