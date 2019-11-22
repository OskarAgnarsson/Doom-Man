using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickups : MonoBehaviour
{
    public List<GameObject> pickupList;

    GameObject currentobject;
    GameObject prevobject;
    float spawnTime;
    float nextSpawn;
    Animator pickupAnim;

    void Start()
    {
        currentobject = pickupList[Random.Range(0,1)];
        spawnTime = 5f;
        pickupAnim = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(nextSpawn <= Time.time && GameObject.Find(currentobject.name) == null)
        {
            pickupAnim.SetBool("Item",true);
        }
    }
    
    void spawnitem()
    {
        Instantiate(currentobject,transform.position+new Vector3(0f,2f,0f),new Quaternion(0f,0f,0f,0f),transform);
        nextSpawn = Time.time + spawnTime;
        prevobject = currentobject;
        currentobject = pickupList[Random.Range(0,1)];
    }
}
