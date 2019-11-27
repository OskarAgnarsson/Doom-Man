using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    
    public PlayerController playerCont;
    public Text healthAmount;
    public Text ammoAmount;
    public GameObject menu;
    public bool menuOpen;
    public bool cursorVisible;

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
        if (!menu.activeSelf) {
            menuOpen = false;
        }
        else {
            menuOpen = true;
        }
        if (menuOpen && Time.timeScale != 0) {
            Time.timeScale = 0;
        }
        else if (!menuOpen && Time.timeScale == 0) {
            Time.timeScale = 1;
        }
        if (!menuOpen && Input.GetKeyDown("escape")) {
            openMenu();
        }
        else if (menuOpen && Input.GetKeyDown("escape")) {
            closeMenu();
        }
        if (cursorVisible) {
            Cursor.visible = true;
        }
        else {
            Cursor.visible = false;
        }
        if(menuOpen && !cursorVisible) {
            cursorVisible = true;
        }
        else if (!menuOpen && cursorVisible) {
            cursorVisible = false;
        }
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

    void openMenu() {
        menu.SetActive(true);
        menuOpen = true;
        cursorVisible = true;
        Time.timeScale = 0;
    }

    void closeMenu() {
        menu.SetActive(false);
        menuOpen = false;
        cursorVisible = false;
        Time.timeScale = 1;
    }
}
