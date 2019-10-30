using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float health;
    public float AttackCooldown;

    float nextAttack;
    PlayerController player;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hit()
    {
        if(Time.time >= nextAttack)
        {
            player.health -= 50;
            nextAttack = Time.time + AttackCooldown;
        }
    }
}
