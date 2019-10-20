using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{

    private Vector3 mousePos;

    private void Start()
    {
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        if (Cursor.visible)
        {
            Cursor.visible = false;
        }
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        transform.position = mousePos;
    }
}
