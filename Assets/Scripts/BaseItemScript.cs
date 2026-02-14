using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItemScript : MonoBehaviour
{
    public bool isHeld;
    public GameObject cam;
    public Transform itemPosition;
    public Color originalColor;
    public Color highlightColor;
    public Collider itemCollider;


    // Start is called before the first frame update
    void Start()
    {
        itemPosition = GameObject.Find("ItemLocation").transform;
        cam = GameObject.Find("Main Camera");
        itemCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            itemCollider.enabled = false;
            transform.parent = cam.transform;
            transform.position = itemPosition.position;
        }
        else
        {
            itemCollider.enabled = true;
            gameObject.transform.SetParent(null);
        }
    }
/*
    private void OnMouseEnter()
    {
        GetComponent<MeshRenderer>().material.color = highlightColor;
    }

    private void OnMouseExit()
    {
        GetComponent<MeshRenderer>().material.color = originalColor;
    }
*/
}
