using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControlls : MonoBehaviour
{
    public float shotcount;

    public PlayerController player;
    public Transform right;
    public Transform left;
    private Animator gunAnim;
    private Camera cam;
    private SpriteRenderer gunrenderer;

    private void Start()
    {
        gunrenderer = transform.gameObject.GetComponent<SpriteRenderer>();
        cam = Camera.main;
        gunAnim = transform.gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        shotcount = player.Weapons[player.WeaponType]["Bullets"];
        if (Input.GetAxis("Fire1") == 1 && Time.time > player.NextShot && player.firerate > 0 && shotcount > 0)//Skýtur og bætir við delay
        {
            gunAnim.SetTrigger("Shoot");
        }
        transform.rotation = Quaternion.Euler(0.0f, 0.0f,90f)*(Quaternion.AngleAxis(player.playermouseangle, Vector3.back));//Snýr byssu í átt að músinni
        if (player.mouseangle > 90 || player.mouseangle < -90)//Byssan skiptir um hlið miðað við hvaða átt músin miðar
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


    void DetractAmmo()
    {
        player.Weapons[player.WeaponType]["Bullets"] = player.Weapons[player.WeaponType]["Bullets"] - 1f;
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
