using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerspeed;
    public float roll_thrust;

    private float mvx;
    private float mvy;
    private Vector2 mv;
    private Rigidbody2D playerbody;
    

    void Awake()
    {
        roll_thrust = 5f;
        playerspeed = 5f;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        roll_thrust = 100f;
        mvx = Input.GetAxis("Horizontal");
        mvy = Input.GetAxis("Vertical");
        mv = new Vector2(mvx,mvy);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            for(int i = 0;i < 5;i++)
            {
                transform.position = Vector2.MoveTowards(playerbody.position,playerbody.position + mv,roll_thrust * Time.deltaTime);
            }
        }
    }

    void FixedUpdate()
    {
        playerbody.MovePosition(playerbody.position + mv * playerspeed * Time.fixedDeltaTime);
        
    }
}