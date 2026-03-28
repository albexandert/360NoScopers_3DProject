using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItemScript : MonoBehaviour
{
    public bool isHeld;
    public GameObject cam;
    public Transform itemPosition;
    public Collider itemCollider;
    public Outline itemOutline; 


    // Start is called before the first frame update
    void Start()
    {
        itemPosition = GameObject.Find("ItemLocation").transform;
        cam = GameObject.Find("Main Camera");
        itemCollider = GetComponent<Collider>();
        itemOutline = GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            itemCollider.enabled = false;
            transform.parent = cam.transform;
            transform.position = itemPosition.position;
            itemOutline.enabled = false;
        }
        else
        {
            itemCollider.enabled = true;
            gameObject.transform.SetParent(null);
        }
    }

    private void OnMouseEnter()
    {
        if (Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position) < 5)
        {
            itemOutline.enabled = true;
        }
    }

    private void OnMouseExit()
    {
        itemOutline.enabled = false;
    }
}
