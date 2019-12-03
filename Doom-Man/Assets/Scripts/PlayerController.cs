using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        public float health;
        public float MaxHealth;
        public string prevweapon;
        public List<string> inventory;
        public int inventoryIndex;
        public Dictionary<string, Dictionary<string, float>> Weapons = new Dictionary<string, Dictionary<string, float>>()
            {
                {
                    "Pistol",new Dictionary<string,float>()
                    {
                        {"FireRate",0.7f},
                        {"BulletCount",1f},
                        {"Bullet dmg",25f},
                        {"Bullets",20f},
                        {"Bullet Pack",5f},
                        {"Max Ammo",20f}
                    }
                },
                {
                    "Shotgun",new Dictionary<string, float>()
                    {
                        {"FireRate",2.5f},
                        {"BulletCount",5f},
                        {"Bullet dmg",15f},
                        {"Bullets",5f},
                        {"Bullet Pack",3f},
                        {"Max Ammo",30f}
                    }
                },
                {
                    "SMG",new Dictionary<string,float>()
                    {
                        {"FireRate",0.3f},
                        {"BulletCount",1f},
                        {"Bullet dmg",10f},
                        {"Bullets",30f},
                        {"Bullet Pack",10f},
                        {"Max Ammo",30f}
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
        public Animator pistolPickup;
        public Animator shotgunPickup;
        public Animator smgPickup;
        public HUDController hudCont;

    //Normal Private Vars
        private float bulletcount;
        private float ammo_after_pickup;
        private float health_after_pickup;
        private int scene;
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
        private pickups pickup;
        private UnlockPistol pistolUnlock;
        private DoorControls doorCon;

    void Awake()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
        playerbody = gameObject.GetComponent<Rigidbody2D>();
        GunSprite = gameObject.GetComponent<SpriteRenderer>();
        if (scene == 1) {
            pistolUnlock = GameObject.FindWithTag("PistolPickup").GetComponent<UnlockPistol>();
            pistolPickup = GameObject.FindWithTag("PistolPickup").GetComponent<Animator>();
        }
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
        WeaponType = inventory[inventoryIndex];
        prevweapon = inventory[inventoryIndex+1];
        health = 100f;
        MaxHealth = 100f;
        cam = Camera.main;
        inventory = new List<string>(){ "Pistol", "Shotgun", "SMG"};
        WeaponType = inventory[inventoryIndex];
        prevweapon = inventory[inventoryIndex+1];
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
        if (!hudCont.menuOpen) {
            PlayerToMouse = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,cam.nearClipPlane))-transform.position;//Mismunur mouseposition og players
            PlayerToMousetracker = new Vector2(mouseangletracker.transform.position.x,mouseangletracker.transform.position.y) - new Vector2(transform.position.x,transform.position.y);
            mouseangle = Vector2.Angle(PlayerToMousetracker,PlayerToMouse);
            playermouseangle = Mathf.Atan2(PlayerToMouse.x,PlayerToMouse.y) * Mathf.Rad2Deg;
            
            if(PlayerToMouse.y < 0)//Breytir mouseangle í mínus ef músin er undir player, leyfir okkur að nota mouseangle sem 360 gráður í staðinn fyrir 180
            {
                mouseangle = -mouseangle;
            }
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

    void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }


    void OnTriggerStay2D(Collider2D other)
    {
        //Ef player er inni í trigger með enemy tag þá ræðst enemy á player
        if(other.CompareTag("Enemy"))
        {
            EnemyBe = other.gameObject.GetComponent<EnemyBehaviour>();
            EnemyBe.hit();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Ókláraður kóði sem á að bæta við ammo þegar player snertir ammo pickup
        if (other.CompareTag("Ammo"))
        {
            pickup = other.gameObject.GetComponentInParent<pickups>();
            ammo_after_pickup = Weapons[WeaponType]["Bullets"]+Weapons[WeaponType]["Bullet Pack"];
            if(Weapons[WeaponType]["Bullets"] == Weapons[WeaponType]["Max Ammo"])
            {
                pickup.pickupAnim.SetBool("Item",true);
            }
            else if(ammo_after_pickup >= Weapons[WeaponType]["Max Ammo"])
            {
                Destroy(other.gameObject);
                Weapons[WeaponType]["Bullets"] = Weapons[WeaponType]["Max Ammo"];
                pickup.pickupAnim.SetBool("Item",false);
                pickup.pickupCount -= 1;
                pickup.ExtendNextSpawn();
            }
            else
            {
                Destroy(other.gameObject);
                Weapons[WeaponType]["Bullets"] = ammo_after_pickup;
                pickup.pickupAnim.SetBool("Item",false);
                pickup.pickupCount -= 1;
                pickup.ExtendNextSpawn();
            }
        }

        if(other.CompareTag("Pistol"))
        {
            pistolPickup.SetBool("pickup",true);
            Destroy(other.gameObject);
            pistolUnlock.hasGun = true;
        }

        if (other.CompareTag("Health"))
        {
            pickup = other.gameObject.GetComponentInParent<pickups>();
            health_after_pickup = health+25;

            if(health_after_pickup >= MaxHealth)
            {
                health = MaxHealth;
                Destroy(other.gameObject);
                pickup.pickupAnim.SetBool("Item",false);
                pickup.pickupCount -= 1;
                pickup.ExtendNextSpawn();
            }
            else if (health_after_pickup <= MaxHealth)
            {
                health = health_after_pickup;
                Destroy(other.gameObject);
                pickup.pickupAnim.SetBool("Item",false);
                pickup.pickupCount -= 1;
                pickup.ExtendNextSpawn();
            }
            
        }

        if (other.CompareTag("Door")) {
            doorCon = other.gameObject.GetComponent<DoorControls>();
            if (!doorCon.doorLocked) {
                LoadByIndex(doorCon.nextLevelIndex);
            }
        }
    }
}
