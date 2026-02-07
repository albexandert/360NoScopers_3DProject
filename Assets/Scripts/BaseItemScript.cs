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


    // Start is called before the first frame update
    void Start()
    {
        itemPosition = GameObject.Find("ItemLocation").transform;
        cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            transform.parent = cam.transform;
            transform.position = itemPosition.position;
        }
        else
        {
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
