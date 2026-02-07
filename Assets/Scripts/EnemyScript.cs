using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public float enemyHP;
    public float dmg;
    private MeshRenderer objectRenderer;
    private Color originalColor;
    public Color flashColor = Color.red;
    public float flashDuration = 0.01f;
    public GameObject player;

    private NavMeshAgent agent;

    [SerializeField] LayerMask groundLayer, playerLayer;

    //patrol
    public Vector3 destPoint;
    private bool walkPointSet;
    [SerializeField] float walkRange;

    //state change
    [SerializeField] float sightRange;
    [SerializeField] private bool playerInSight;
    void Start()
    {
        objectRenderer = GetComponent<MeshRenderer>();
        originalColor = objectRenderer.material.color;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);

        if (!playerInSight)
        { 
            Patrol();
        }
        if (playerInSight)
        {
            Chase();
        }
        
    }

    void Chase()
    {
        agent.SetDestination(player.transform.position);
    }
    public void Flash()
    {
        StopCoroutine("DoFlash");
        StartCoroutine("DoFlash");
    }

    public void takeDamage(float dmg)
    {
        Flash();
        SoundManager.PlaySound(SoundType.MONSTERGROWL2);
        if (enemyHP <= 0f)
        {
            Destroy(gameObject);
        }
        else
        {
            enemyHP -= dmg;
        }
    }

    IEnumerator DoFlash()
    {
        objectRenderer.material.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        objectRenderer.material.color = originalColor;
        
    }

    void Patrol()
    {
        if (!walkPointSet) SearchForDest();
        if (walkPointSet) agent.SetDestination(destPoint);
        if (Vector3.Distance(transform.position, destPoint) < 10) walkPointSet = false;
    }

    void SearchForDest()
    {
        float z = Random.Range(-walkRange, walkRange);
        float x = Random.Range(-walkRange, walkRange);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destPoint, Vector3.down, groundLayer))
        {
            walkPointSet = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerScript>().takeDamage(dmg);
        }
    }
}
