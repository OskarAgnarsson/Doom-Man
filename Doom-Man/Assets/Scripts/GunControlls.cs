﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControlls : MonoBehaviour
{
    int count = 0;
    public PlayerController player;
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
            
            if (count < 1 && player.mouseangle < -90 || player.mouseangle > 90)
            {
                transform.position -= new Vector3(transform.position.x-.470f,0.0f,0.0f);
                count += 1;
            }
            if (player.mouseangle > 90 )
            {
                gunrenderer.flipY = false;
            }
            else
            {
                gunrenderer.flipY = true;
            }
        }
        else 
        {
            count = 0;
            transform.position = original_pos;
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
