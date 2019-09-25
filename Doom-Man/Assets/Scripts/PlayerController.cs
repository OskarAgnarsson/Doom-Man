using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Normal Public Vars
    public float playerspeed;
    public bool is_rolling;
    public float mouseangle;

    //Unity Specific Public Vars
    public GameObject mouseangletracker;

    //Normal Private Vars
    private bool testvar;
    private float original_playerspeed;
    private float mvx;
    private float mvy;
    [SerializeField] private float roll_time;
    private float original_roll_time;
    [SerializeField] private float roll_modifier;

    //Unity Specific Private Vars
    private Camera cam;
    private Vector2 PlayerToMousetracker;
    private Vector3 PlayerToMouse;
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
        PlayerToMousetracker = new Vector2(mouseangletracker.transform.position.x,mouseangletracker.transform.position.y) - new Vector2(transform.position.x,transform.position.y);
        mouseangle = Vector2.Angle(PlayerToMousetracker,cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,cam.nearClipPlane))-transform.position);
        PlayerToMouse = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,cam.nearClipPlane))-transform.position;
        if(PlayerToMouse.y < 0)
        {
            mouseangle = -mouseangle;
        }
        Debug.Log(mouseangle);
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