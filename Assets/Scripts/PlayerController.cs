using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerspeed;
    
    private float mvx;
    private float mvy;
    private Vector2 mv;
    private Rigidbody2D playerbody;

    void Awake()
    {
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
        mvx = Input.GetAxis("Horizontal");
        mvy = Input.GetAxis("Vertical");
        mv = new Vector2(mvx,mvy);
    }

    void FixedUpdate()
    {
        playerbody.MovePosition(playerbody.position + mv * playerspeed * Time.fixedDeltaTime);
    }
    
}