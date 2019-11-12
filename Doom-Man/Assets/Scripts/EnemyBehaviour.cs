using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyBehaviour : MonoBehaviour
{
    public float health;
    public float AttackCooldown;

    float nexthit;
    float immunity = 2f;
    float nextAttack;
    private Vector3 prevPos;

    Animator enemyanim;
    PlayerController player;
    Seeker enemyAI;
    AIPath enemyPath;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        enemyAI = this.gameObject.GetComponent<Seeker>();
        enemyPath = this.gameObject.GetComponent<AIPath>();
        enemyanim = this.gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        MoveAnim();
    }

    //This Is Used For When The Enemy Hits The Player
    public void hit()
    {
        if(Time.time >= nextAttack)
        {
            player.health -= 50;
            nextAttack = Time.time + AttackCooldown;
        }
    }

    private void MoveAnim()
    {
        if (transform.position != prevPos)
        {
            float xDiff = transform.position.x - prevPos.x;
            float yDiff = transform.position.y - prevPos.y;
            enemyanim.SetBool("Walking", true);

            if (Mathf.Abs(yDiff) > Mathf.Abs(xDiff))
            {
                if (yDiff < 0)
                {
                    enemyanim.SetInteger("Direction", 4);
                }
                else
                {
                    enemyanim.SetInteger("Direction", 3);
                }
            }
            else
            {
                if (xDiff < 0)
                {
                    enemyanim.SetInteger("Direction", 2);
                }
                else
                {
                    enemyanim.SetInteger("Direction", 1);
                }
            }

        }
        prevPos = transform.position;
    }

    //This Function Checks The Enemies Health Every Frame And Calls The Death Animation When The Enemy Is Dead
    void CheckHealth()
    {
        if(health <= 0)
        {
            enemyanim.SetTrigger("Death");
        }
    }

    //Used For Death Animation, One Disables The Pathfinding Scripts And The Other Destroys The GameObject/Enemy
    void DisableSkynet()
    {
        enemyAI.enabled = false;
        enemyPath.enabled = false;
    }

    void EnemyDeath()
    {
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject);
    }

    //Check For Collisions Used For Bullet Hit Detection
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.CompareTag("bullet"))
        {
            Destroy(other.gameObject);
            if(nexthit <= Time.time)
            {
                health = health - player.Weapons[player.WeaponType]["Bullet dmg"];
                nexthit = Time.time + immunity;
            }
        }
    }
}
