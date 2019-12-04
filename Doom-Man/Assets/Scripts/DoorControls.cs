using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControls : MonoBehaviour
{
    public GameObject player;
    public bool playerClose = false;
    public int nextLevelIndex;
    public bool doorLocked = true;

    private Animator doorAnim;
    private RedButton redButton;

    void Awake() {
        doorAnim = gameObject.GetComponent<Animator>();
        if (GameObject.FindWithTag("Button")) {
            redButton = GameObject.FindWithTag("Button").GetComponent<RedButton>();
        }
    }


    void Update()
    {
        //opnar hurð þegar player er nálægt
        if (Vector2.Distance(new Vector2(transform.position.x,transform.position.y),new Vector2(player.transform.position.x,player.transform.position.y)) < 5f) {
            playerClose = true;
        } else {
            playerClose = false;
        }

        //Kóði fyrir takkann í Facility2E
        if (GameObject.FindWithTag("Button")){
            //Opnar hurð ef allir óvinir eru dauðir
            if (GameObject.FindWithTag("Enemy") == null && redButton.button) {
                doorAnim.SetBool("Locked",false);
                doorLocked = false;
            }
        }
        else {
            if (GameObject.FindWithTag("Enemy") == null) {
                doorAnim.SetBool("Locked",false);
                doorLocked = false;
            }
        }

        if (playerClose) {
            doorAnim.SetBool("Open",true);
        }
        else {
            doorAnim.SetBool("Open",false);
        }

    }
}
