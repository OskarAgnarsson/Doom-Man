using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{

    private Vector3 mousePos;

    private void Start()
    {
        Cursor.visible = false;//Lætur músina verða ósýnilega
    }

    void FixedUpdate()
    {
        if (Cursor.visible)//Tryggir það að músin sé ósýnileg
        {
            Cursor.visible = false;
        }
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));//Staðsetning músar
        transform.position = mousePos;//Færir crosshair með músinni
    }
}
