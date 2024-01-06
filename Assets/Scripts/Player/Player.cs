using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Important Settings
    [Header("Stats")]
    public PlayerMode Mode;
    public int Health = 50;

    public int DNACount;

    public int TechCount;


    [Header("Settings")]
    public int MaxHealth = 50;
    public float MaxSpeed = 3f;
    public float Acceleration = 1f;
    public float ShootSpeed = 0.7f;
    public int Damage = 15;
    public bool Animate = true;
    
    float TimeSinceShoot;
    bool Dead;
    bool Left;
    [HideInInspector]
    public bool CanShoot = true;

    float TimeSinceCollect;

    [Header("References")]
    //References
    public GameObject Rocket;
    public GameObject Canvas;    
    public GameObject HealthBar;
    public GameObject ResourcePanel;
    public TMPro.TextMeshProUGUI DNACounter;
    public TMPro.TextMeshProUGUI TechCounter;

    GameObject Bazooka;
    Transform ShootPoint;    
    Rigidbody2D Rigidbody;
    Animator Animator;
    public Camera Camera;

    public bool DebugMode;


    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Bazooka = transform.GetChild(1).gameObject;
        ShootPoint = Bazooka.transform.GetChild(0).transform;
        Canvas.SetActive(true);
        AudioListener.volume = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mode == PlayerMode.Weapon || Mode == PlayerMode.NoWeapon) {
            //Limit Speed
            if (Rigidbody.velocity.magnitude > MaxSpeed)
                Rigidbody.velocity = Vector2.ClampMagnitude(Rigidbody.velocity, MaxSpeed);

            //Flip Sprite
            if (Input.GetAxis("Horizontal") < -0.05 && !Dead)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
                Left = true;
            }
            else if (Input.GetAxis("Horizontal") > 0.05 && !Dead)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                Left = false;
            }
        }

        
        if (Mode == PlayerMode.Weapon) {

        Bazooka.SetActive(true);

        Vector3 SCP = Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 RPos = new Vector3(SCP.x, SCP.y, 0) - transform.position;

        //if (RPos.x < 0)
        //    RPos = -RPos;

        if (!Dead)
        {
            if (!Left)
            {
                RPos = new Vector3(Mathf.Clamp(RPos.x, 0, 100), RPos.y, 0);
                Bazooka.transform.right = RPos;
            }
            else
            {
                RPos = new Vector3(Mathf.Clamp(RPos.x, -100, 0), RPos.y, 0);
                Bazooka.transform.right = -RPos;
            }
        }

        Vector3 BRot = Bazooka.transform.localRotation.eulerAngles;


        if (Input.GetMouseButton(0) && TimeSinceShoot >= ShootSpeed && !Dead && CanShoot)
        {      
            GameObject Proj;
            if (transform.localScale == new Vector3(1, 1, 1))
                Proj = Instantiate(Rocket, ShootPoint.position, Bazooka.transform.rotation);
            else
                Proj = Instantiate(Rocket, ShootPoint.position, Quaternion.Euler(Bazooka.transform.rotation.eulerAngles + new Vector3(0, 0, 180)));

            TimeSinceShoot = 0;
            //Proj.GetComponent<Rigidbody2D>().velocity = Rigidbody.velocity;
        }

        TimeSinceShoot += Time.deltaTime;

        } else 
            Bazooka.SetActive(false);

        if (Input.GetKeyDown(KeyCode.P))
        {
            Canvas.GetComponent<InGameMenu>().ClearData();
            Debug.Log("Data Cleared");
        }


        if (TimeSinceCollect > 0) {
            TimeSinceCollect -= Time.unscaledDeltaTime;
            if (TimeSinceCollect <= 0) {
                ResourcePanel.SetActive(false);
            }
        }

    }

    public void ShakeScreen(float Duration, float Magnitude)
    {
        StartCoroutine(CamShake(Duration, Magnitude));
    }

    void FixedUpdate()
    {
        if (Mode != PlayerMode.Cutscene /*|| Rigidbody.velocity.magnitude > MaxSpeed*/)
        //Movement force
        Rigidbody.AddForce(Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1) * Acceleration, ForceMode2D.Impulse);

        if (Animate)
        //Set anim speed
        Animator.SetFloat("Speed", Rigidbody.velocity.magnitude / 1.5f);
    }

    public void TakeDamage(int Amount)
    {
        Health -= Amount;
        if (Health > MaxHealth)
            Health = MaxHealth;
        else if (Health <= 0)
        {
            Canvas.transform.GetChild(0).gameObject.SetActive(true);
            Canvas.transform.GetChild(1).gameObject.SetActive(false);
            Rigidbody.gravityScale = 0.5f;
            MaxSpeed = 0f;
            Acceleration = 0f;
            Dead = true;
            //Death
        }

        HealthBar.transform.localScale = new Vector3(Health * (1f/MaxHealth), 1, 1);
    }

    public IEnumerator CamShake(float Duration, float Magnitude)
    {
        float Elapsed = 0.0f;

        while (Elapsed < Duration)
        {
            float x = Random.Range(-0.1f, 0.1f) * Magnitude;
            float y = Random.Range(-0.1f, 0.1f) * Magnitude;

            Camera.transform.localPosition = new Vector3(x, y, -10);

            Elapsed += Time.deltaTime;

            yield return null;
        }

        Camera.transform.localPosition = new Vector3(0, 0, -10);
    }


    public void AddResource(Resource Resource, int Amount) {
        ResourcePanel.SetActive(true);
        
        switch (Resource) {
            case Resource.DNA:
                DNACount += Amount;
                DNACounter.text = DNACount.ToString();
                break;

            case Resource.Tech:       
                TechCount += Amount;         
                TechCounter.text = TechCount.ToString();
                break;
        }

        TimeSinceCollect = 3;
    }
}

public enum PlayerMode {
    Weapon,
    NoWeapon,
    Cutscene
}

public enum Resource {
    DNA,
    Tech,
    Badge
}

//Variables