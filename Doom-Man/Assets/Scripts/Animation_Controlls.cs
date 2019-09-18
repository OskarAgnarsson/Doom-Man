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
        if (playerscript.is_rolling == true)
        {
            playeranim.SetTrigger("roll");
        }
        if(playerscript.is_rolling == false)
        {
            playeranim.ResetTrigger("roll");
        }
    }
}
