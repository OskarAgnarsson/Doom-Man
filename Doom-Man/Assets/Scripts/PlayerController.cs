using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Normal Public Vars 
        public float playerspeed;
        public bool is_rolling;
        public float mouseangle;
        public string WeaponType;
        public float NextShot;
        public float playermouseangle;
        public float firerate;
        public float shotcount;
        public float health;
        public string prevweapon;
        public List<string> inventory = new List<string>(){ "Pistol", "Shotgun" };
        public int inventoryIndex;
        public Dictionary<string, Dictionary<string, float>> Weapons = new Dictionary<string, Dictionary<string, float>>()
            {
                {
                    "Pistol",new Dictionary<string,float>()
                    {
                        {"FireRate",0.7f},
                        {"BulletCount",1f},
                        {"Bullet dmg",25f},
                        {"Bullets",20f}
                    }
                },
                {
                    "Shotgun",new Dictionary<string, float>()
                    {
                        {"FireRate",2.5f},
                        {"BulletCount",5f},
                        {"Bullet dmg",15f},
                        {"Bullets",5f}
                    }
                }
            };


    //Unity Specific Public Vars
        //Camera
            public Camera cam;
        public GameObject pistol_bullet;
        public GameObject mouseangletracker;
        public GameObject Gun;
        public Animator gunAnim;
        public SpriteRenderer GunSprite;
        public GameObject Enemy;

    //Normal Private Vars
        private float bulletcount;
        //Movement Vars
            private float mvx;
            private float mvy;
        //Rolling/Dodgeing Vars
            private float original_playerspeed;
            [SerializeField] private float roll_time;
            private float original_roll_time;
            [SerializeField] private float roll_modifier;

    //Unity Specific Private Vars
        //Mouse Vectors
            private Vector2 PlayerToMousetracker;
            private Vector3 PlayerToMouse;
        //Movement
            private Vector2 mv;
            private Rigidbody2D playerbody;
        private EnemyBehaviour EnemyBe;
        

    void Awake()
    {
        EnemyBe = Enemy.gameObject.GetComponent<EnemyBehaviour>();
        playerbody = gameObject.GetComponent<Rigidbody2D>();
        GunSprite = gameObject.GetComponent<SpriteRenderer>();
        inventoryIndex = 0;
        NextShot = Time.time;
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
        health = 100f;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mvInputs();
        roll();
        SwitchWeapons();
        aim();
        firerate = Weapons[WeaponType]["FireRate"];
        bulletcount = Weapons[WeaponType]["BulletCount"];
        Debug.Log(prevweapon);
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
    //Miðar
    void aim()
    {
        PlayerToMouse = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,cam.nearClipPlane))-transform.position;//Mismunur mouseposition og players
        PlayerToMousetracker = new Vector2(mouseangletracker.transform.position.x,mouseangletracker.transform.position.y) - new Vector2(transform.position.x,transform.position.y);
        mouseangle = Vector2.Angle(PlayerToMousetracker,PlayerToMouse);
        playermouseangle = Mathf.Atan2(PlayerToMouse.x,PlayerToMouse.y) * Mathf.Rad2Deg;
        
        if(PlayerToMouse.y < 0)//Breytir mouseangle í mínus ef músin er undir player, leyfir okkur að nota mouseangle sem 360 gráður í staðinn fyrir 180
        {
            mouseangle = -mouseangle;
        }
    }

    //Skýtur
    public void shoot(string weapon_type)
    {
        for (float i = 0; i < bulletcount; i++)
        {
            Quaternion RotationOffset = new Quaternion(0, 0, Random.Range(-0.03f, 0.03f), 1);//Lætur skot ekki vera 100% nákvæmt
            Instantiate(pistol_bullet, transform.position, Quaternion.AngleAxis(playermouseangle, Vector3.back) * RotationOffset);//Býr til Bullet
        }
        NextShot = Time.time + firerate;
    }
    
    void SwitchWeapons()
    {
        WeaponType = inventory[inventoryIndex];
        if (Input.mouseScrollDelta.y < 0)//Skiptir um byssu með því að scrolla
        {
            prevweapon = WeaponType;
            if (inventoryIndex == 0)
            {
                inventoryIndex = inventory.Count - 1;
            }
            else
            {
                inventoryIndex = inventoryIndex - 1;
            }
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            prevweapon = WeaponType;
            if (inventoryIndex == inventory.Count - 1)
            {
                inventoryIndex = 0;
            }
            else
            {
                inventoryIndex = inventoryIndex + 1;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(EnemyBe != null)
            {
                EnemyBe.hit();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ammo"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
