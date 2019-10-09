using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float BulletSpeed;

    Vector3 mv;
    Rigidbody2D BulletBody;

    private float BulletLifeSpan;
    private float BulletDeath;

    // Start is called before the first frame update
    void Start()
    {
        BulletSpeed = 2f;
        BulletLifeSpan = 5f;
        BulletDeath = Time.time + BulletLifeSpan;
        BulletBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BulletBody.MovePosition(transform.position + transform.up * BulletSpeed * Time.fixedDeltaTime);
        if (BulletDeath < Time.time)
        {
            Destroy(transform.gameObject);
        }
    }
}
