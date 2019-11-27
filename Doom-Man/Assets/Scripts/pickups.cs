using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickups : MonoBehaviour
{
    public List<GameObject> pickupList;
    public Animator pickupAnim;
    public bool haspickedup;

    GameObject currentobject;
    GameObject prevobject;
    float spawnTime;
    float nextSpawn;
    int itemindex;

    void Start()
    {
        haspickedup = false;
        itemindex = Random.Range(0,2);
        currentobject = pickupList[itemindex];
        spawnTime = 1f;
        pickupAnim = this.gameObject.GetComponent<Animator>();
        Debug.Log(itemindex);
    }

    void Update()
    {
        Debug.Log(haspickedup);
        itemindex = Random.Range(0,2);
        if(itemindex != 0)
        {
            itemindex = 1;
        }

        if(nextSpawn <= Time.time)
        {
            pickupAnim.SetBool("Item",true);
        }
    }
    
    void ExtendNextSpawn()
    {
        nextSpawn = Time.time + spawnTime;
    }

    void spawnitem()
    {
        if(nextSpawn <= Time.time && haspickedup)
        {
            pickupAnim.SetBool("Item",false);
        }
        Instantiate(currentobject,transform.position+new Vector3(0f,.5f,0f),new Quaternion(0f,0f,0f,0f),transform);
        prevobject = currentobject;
        haspickedup = false;
        currentobject = pickupList[itemindex];
    }
}
