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
        CheckDistance();
    }

    //Attackar player þegar fallið er kallað
    public void hit()
    {
        if(Time.time >= nextAttack && player.health > 0)
        {
            player.health -= 50;
            nextAttack = Time.time + AttackCooldown;
        }
    }

    //Ef player er nálægt þá eltir enemy player, annars ekki
    void CheckDistance() {
        if (Vector3.Distance(transform.position,player.gameObject.transform.position) < 7f && !enemyAI.enabled && !enemyPath.enabled) {
            enemyAI.enabled = true;
            enemyPath.enabled = true;
        } else if (Vector3.Distance(transform.position,player.gameObject.transform.position) > 7f) {
            enemyAI.enabled = false;
            enemyPath.enabled = false;
        }
    }

    private void MoveAnim()
    {
        //Ef enemy er að hreyfast þá animatar þetta player
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
        //Ef player er nógu nálægt snýr enemy í áttina að player
         else if (Vector3.Distance(transform.position, player.gameObject.transform.position) <= 3f) {
            Vector3 playerPos = player.gameObject.transform.position;
            float xDiff = transform.position.x - playerPos.x;
            float yDiff = transform.position.y - playerPos.y;

            if (Mathf.Abs(yDiff) > Mathf.Abs(xDiff))
            {
                if (yDiff > 0)
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
                if (xDiff > 0)
                {
                    enemyanim.SetInteger("Direction", 2);
                }
                else
                {
                    enemyanim.SetInteger("Direction", 1);
                }
            }
        }
        //Ef player er ekki nálægt og enemy hreyfist ekki er ekkert animation
        else {
            enemyanim.SetBool("Walking",false);
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
