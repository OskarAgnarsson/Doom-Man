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

    void Awake() {
        doorAnim = gameObject.GetComponent<Animator>();
    }


    void Update()
    {
        //Ókláraður kóði sem á að opna hurð þegar player er nálægt
        if (Vector2.Distance(new Vector2(transform.position.x,transform.position.y),new Vector2(player.transform.position.x,player.transform.position.y)) < 5f) {
            playerClose = true;
        } else {
            playerClose = false;
        }

        if (GameObject.FindWithTag("Enemy") == null) {
            doorAnim.SetBool("Locked",false);
            doorLocked = false;
        }

        if (playerClose) {
            doorAnim.SetBool("Open",true);
        }
        else {
            doorAnim.SetBool("Open",false);
        }

    }
}
