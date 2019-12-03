using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPistol : MonoBehaviour
{
    public GameObject gun;
    public bool hasGun;
    private PlayerController playerCon;

    void Awake()
    {
        hasGun = false;
    }
    void Start()
    {
        gun = GameObject.FindWithTag("Gun");
    }
    void Update()
    {
        if(hasGun)
        {
            gun.SetActive(true);
        }
        else
        {
            gun.SetActive(false);
        }
    }
}
