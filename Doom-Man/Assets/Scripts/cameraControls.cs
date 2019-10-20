using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControls : MonoBehaviour
{
    public GameObject target;
    
    void LateUpdate()
    {
        transform.position = new Vector3(target.gameObject.transform.position.x,target.gameObject.transform.position.y,-10);//Færir camera með player
    }
}
