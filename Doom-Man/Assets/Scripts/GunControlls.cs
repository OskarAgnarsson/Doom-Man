using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControlls : MonoBehaviour
{
    public PlayerController player;
    Animator gunAnim;
    private void Start()
    {
        gunAnim = transform.gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetAxis("Fire1") == 1 && Time.time > player.NextShot && player.firerate > 0)
        {
            gunAnim.SetTrigger("Shoot");
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
