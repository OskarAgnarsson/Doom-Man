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
    //ÞETTA SCRIPT VIRKAR EKKI OG ER EKKI NOTAÐ
    void Update()
    {
        if (hasGun) {
            playerCon.inventory = new List<string>(){ "Pistol","Shotgun"};
        }
    }
}
