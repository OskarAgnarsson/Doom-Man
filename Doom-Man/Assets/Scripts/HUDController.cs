using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    
    public PlayerController playerCont;
    public Text healthAmount;
    public Text ammoAmount;

    private Animator anim;

    void Awake() 
    {
        anim = gameObject.GetComponent<Animator>();
    }


    void Update()
    {
        if (playerCont.Weapons[playerCont.WeaponType]["Bullets"].ToString() != ammoAmount.text) {
            ammoAmount.text = playerCont.Weapons[playerCont.WeaponType]["Bullets"].ToString();
        }

        if (playerCont.health.ToString() != healthAmount.text) {
            healthAmount.text = playerCont.health.ToString();
        }
        hotbar();
    }

    void hotbar() {
        if (playerCont.WeaponType == "Pistol") {
            anim.SetInteger("Weapon",1);
        }
        else if (playerCont.WeaponType == "Shotgun") {
            anim.SetInteger("Weapon",2);
        }
        else if (playerCont.WeaponType == "SMG") {
            anim.SetInteger("Weapon",3);
        }
    }
}
