using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControls : MonoBehaviour
{
    public GameObject player;
    public bool playerClose = false;


    void Update()
    {
        if (Vector2.Distance(new Vector2(transform.position.x,transform.position.y),new Vector2(player.transform.position.x,player.transform.position.y)) < 5f) {
            playerClose = true;
        } else {
            playerClose = false;
        }
        Debug.Log(playerClose);
    }
}
