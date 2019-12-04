using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickups : MonoBehaviour
{
    public List<GameObject> pickupList;
    public Animator pickupAnim;
    GameObject currentobject;
    float spawnTime;
    float nextSpawn;
    int itemindex;

    public int pickupCount = 0;

    void Start()
    {
        itemindex = Random.Range(0,2);
        currentobject = pickupList[itemindex];
        spawnTime = 1.5f;
        pickupAnim = this.gameObject.GetComponent<Animator>();
        nextSpawn = Time.time;
    }

    void Update()
    {
        spawnitem();
    }
    
    public void ExtendNextSpawn()
    {
        //Lengir tíma í næsta spawn
        nextSpawn = Time.time + spawnTime;
    }

    void spawnitem()
    {
        //Spawnar pickup eftir einhvern tíma
        if(nextSpawn <= Time.time && pickupCount == 0)
        {
            pickupAnim.SetBool("Item",true);
            Instantiate(currentobject,transform.position+new Vector3(0f,.5f,0f),new Quaternion(0f,0f,0f,0f),transform);
            itemindex = Random.Range(0,2);
            currentobject = pickupList[itemindex];
            pickupCount += 1;
        }
    }
}
