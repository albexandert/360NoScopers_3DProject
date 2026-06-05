using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float timer;
    public float dmg;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.75f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 3 || collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().takeDamage(dmg);
            Destroy(gameObject);
        }
    }
}
