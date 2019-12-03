using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockShotgun : MonoBehaviour
{
    public bool hasGun;
    private PlayerController playerCon;

    void Awake() {
        hasGun = false;
        playerCon = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (hasGun) {
            playerCon.inventory = new List<string>(){ "Pistol","Shotgun"};
        }
    }
}
