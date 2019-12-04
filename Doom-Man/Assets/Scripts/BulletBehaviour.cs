using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float BulletSpeed;

    private Vector3 mv;
    private Rigidbody2D BulletBody;

    private float BulletLifeSpan;
    private float BulletDeath;


    void Start()
    {
        BulletSpeed = 2f;//Hraði kúlunnar
        BulletLifeSpan = 5f;//Hversu lengi byssukúlan verður til áður en hún hverfur
        BulletDeath = Time.time + BulletLifeSpan;
        BulletBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        BulletBody.MovePosition(transform.position + transform.up * BulletSpeed * Time.fixedDeltaTime);//Færir byssukúluna
        if (BulletDeath < Time.time)
        {
            Destroy(transform.gameObject);//Eyðir sér eftir einhvern tíma
        }
    }
}
