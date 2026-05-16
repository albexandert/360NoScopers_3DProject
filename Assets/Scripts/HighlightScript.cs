using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightScript : MonoBehaviour
{
    private Outline itemOutline;
    public Transform cam;
    void Start()
    {
        cam = GameObject.Find("Main Camera").transform;
        itemOutline = GetComponent<Outline>();
        itemOutline.enabled = false;
    }

    private void OnMouseEnter()
    {
        if (Vector3.Distance(gameObject.transform.position, cam.position) <= 4.5f)
        {
            itemOutline.enabled = true;
        }
    }
    private void OnMouseOver()
    {
        if (Vector3.Distance(gameObject.transform.position, cam.position) <= 4.5f)
        {
            itemOutline.enabled = true;
        }
        else if (Vector3.Distance(gameObject.transform.position, cam.position) > 4.5f)
        {
            itemOutline.enabled = false;
        }
    }
    private void OnMouseExit()
    {
        itemOutline.enabled = false;
    }
}
