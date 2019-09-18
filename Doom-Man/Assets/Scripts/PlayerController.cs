using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerspeed;
    public bool is_rolling;

    bool testvar;
    private float mvx;
    private float mvy;
    private float roll_time;
    private Vector2 mv;
    private Rigidbody2D playerbody;
    

    void Awake()
    {
        testvar = false;
        is_rolling = false;
        playerspeed = 5f;
        roll_time = 0.5f;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mvx = Input.GetAxis("Horizontal") * playerspeed;
        mvy = Input.GetAxis("Vertical") * playerspeed;
        mv = new Vector2(mvx,mvy);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            testvar = true;
            playerspeed = 10f;
            is_rolling = true;
        }
        if(roll_time <= 0f)
        {
            playerspeed = 5f;
            roll_time = 0.5f;
            is_rolling = false;
        }
        if(roll_time > 0f && is_rolling)
        {
            roll_time -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        Debug.Log(testvar);
        Debug.Log(mv*Time.fixedDeltaTime);
        playerbody.MovePosition(playerbody.position +  mv *  Time.fixedDeltaTime);
    }
}