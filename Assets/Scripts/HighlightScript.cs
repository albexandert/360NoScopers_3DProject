using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightScript : MonoBehaviour
{
    private Outline itemOutline;
    void Start()
    {
        itemOutline = GetComponent<Outline>();
        itemOutline.enabled = false;
    }

    private void OnMouseEnter()
    {
        if (Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position) < 4f)
        {
            itemOutline.enabled = true;
        }
    }

    private void OnMouseExit()
    {
        itemOutline.enabled = false;
    }
}
