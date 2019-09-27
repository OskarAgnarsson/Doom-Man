using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Controlls : MonoBehaviour
{
    [SerializeField] public GameObject player;
    private PlayerController playerscript;
    private Animator playeranim;
    

    void Start()
    {
        playerscript = player.gameObject.GetComponent<PlayerController>();
        playeranim = player.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseAngle = playerscript.mouseangle;
        if (playerscript.is_rolling == true)
        {
            playeranim.SetTrigger("roll");
        }
        if(playerscript.is_rolling == false)
        {
            playeranim.ResetTrigger("roll");
        }
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) {
            playeranim.SetBool("IsWalking",true);
        } 
        else {
            playeranim.SetBool("IsWalking",false);
        }
        if (mouseAngle >= -45 && mouseAngle <=45) {
            playeranim.SetBool("FaceEast",true);
            playeranim.SetBool("FaceNorth",false);
            playeranim.SetBool("FaceWest",false);
            playeranim.SetBool("FaceSouth",false);
        }
        else if (mouseAngle > 45 && mouseAngle < 135) {
            playeranim.SetBool("FaceEast",false);
            playeranim.SetBool("FaceNorth",true);
            playeranim.SetBool("FaceWest",false);
            playeranim.SetBool("FaceSouth",false);
        }
        else if (mouseAngle >= 135 || mouseAngle <= -135) {
            playeranim.SetBool("FaceEast",false);
            playeranim.SetBool("FaceNorth",false);
            playeranim.SetBool("FaceWest",true);
            playeranim.SetBool("FaceSouth",false);
        }
        else if (mouseAngle > -135 && mouseAngle < -45) {
            playeranim.SetBool("FaceEast",false);
            playeranim.SetBool("FaceNorth",false);
            playeranim.SetBool("FaceWest",false);
            playeranim.SetBool("FaceSouth",true);
        }
    }
}
