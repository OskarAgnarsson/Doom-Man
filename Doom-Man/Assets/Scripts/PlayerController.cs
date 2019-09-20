using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerspeed;
    public bool is_rolling;

    private bool testvar;
    private float original_playerspeed;
    private float mvx;
    private float mvy;
    private Camera cam;
    [SerializeField] private float roll_time;
    private float original_roll_time;
    [SerializeField] private float roll_modifier;
    private Vector2 mv;
    private Rigidbody2D playerbody;
    

    void Awake()
    {
        is_rolling = false;
        playerspeed = 5f;
        original_playerspeed = playerspeed;
        roll_time = 0.3f;
        original_roll_time = roll_time;
        roll_modifier = 2.5f;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerbody = gameObject.GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mvInputs();
        roll();
        Debug.Log(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,cam.nearClipPlane))-transform.position);
    }

    void FixedUpdate()
    {
        playerbody.MovePosition(playerbody.position +  mv *  Time.fixedDeltaTime);
    }


    //These Are The Inputs The Player Uses To Move
    void mvInputs()
    {
        mvx = Input.GetAxis("Horizontal");
        mvy = Input.GetAxis("Vertical") ;
        mv = new Vector2(mvx* playerspeed,mvy* playerspeed);
    }


    //Allows The Player To roll and change direction while doing so
    void roll()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerspeed = playerspeed * roll_modifier;
            is_rolling = true;
        }
        if(roll_time <= 0f)
        {
            playerspeed = original_playerspeed;
            roll_time = original_roll_time;
            is_rolling = false;
        }
        if(roll_time > 0f && is_rolling)
        {
            roll_time -= Time.deltaTime;
        }
    }
}