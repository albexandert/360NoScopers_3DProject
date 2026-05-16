using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BaseItemScript : MonoBehaviour
{
    public bool isHeld;
    public GameObject cam;
    public Transform itemPosition;
    public Collider itemCollider;
    public Outline itemOutline;
    public Rigidbody rb;
    public GameObject oG;


    // Start is called before the first frame update
    void Start()
    {
        itemPosition = GameObject.Find("ItemLocation").transform;
        cam = GameObject.Find("Main Camera");
        itemCollider = GetComponent<Collider>();
        itemOutline = GetComponent<Outline>();
        oG = GameObject.FindGameObjectWithTag("OG");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            
            transform.position = itemPosition.position;
           
        }
    }

    public void OnPickUpStarted()
    {

        itemOutline.enabled = false;
        itemCollider.enabled = false;
        isHeld = true;
        rb.useGravity = false;
        rb.isKinematic = true;
        transform.parent = cam.transform;
    }

    public void OnPickUpEnded()
    {
        itemCollider.enabled = true;
        isHeld = false;
        rb.useGravity = true;
        rb.isKinematic = false;
        gameObject.transform.SetParent(oG.transform);
        gameObject.transform.SetParent(null);
    }

}
