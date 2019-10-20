using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControlls : MonoBehaviour
{
    public PlayerController player;
    public Transform right;
    public Transform left;
    Animator gunAnim;
    Camera cam;
    SpriteRenderer gunrenderer;
    Vector3 original_pos;

    private void Start()
    {
        gunrenderer = transform.gameObject.GetComponent<SpriteRenderer>();
        cam = Camera.main;
        gunAnim = transform.gameObject.GetComponent<Animator>();
        original_pos = transform.position;
    }
    private void Update()
    {
        if (Input.GetAxis("Fire1") == 1 && Time.time > player.NextShot && player.firerate > 0)
        {
            gunAnim.SetTrigger("Shoot");
        }
        transform.rotation = Quaternion.Euler(0.0f, 0.0f,90f)*(Quaternion.AngleAxis(player.playermouseangle, Vector3.back));
        if (player.mouseangle > 90 || player.mouseangle < -90)
        {
            
            if (((player.mouseangle > 90 && player.mouseangle <= 180) || (player.mouseangle < -90 && player.mouseangle >= -180)))
            {
                transform.position = left.position;
            }
            if ((player.mouseangle < 90 && player.mouseangle >= 0)|| (player.mouseangle > -90 && player.mouseangle <= 0))
            {
                gunrenderer.flipY = false;
            }
            else if ((player.mouseangle > 90 && player.mouseangle <= 180) || (player.mouseangle < -90 && player.mouseangle >= -180))
            {
                gunrenderer.flipY = true;
            }
        }
        else 
        {
            if (((player.mouseangle < 90 && player.mouseangle >= 0)|| (player.mouseangle > -90 && player.mouseangle <= 0))) {
                transform.position = right.position;
            }
            if ((player.mouseangle < 90 && player.mouseangle >= 0)|| (player.mouseangle > -90 && player.mouseangle <= 0))
            {
                gunrenderer.flipY = false;
            }
            else if ((player.mouseangle > 90 && player.mouseangle <= 180) || (player.mouseangle < -90 && player.mouseangle >= -180))
            {
                gunrenderer.flipY = true;
            }
        }
    }


    void shoot()
    {
        player.shoot(player.WeaponType);
    }
    void triggeroff()
    {
        gunAnim.ResetTrigger("Shoot");
    }

}
