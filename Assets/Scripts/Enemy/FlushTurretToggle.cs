using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;

public class FlushTurretToggle : Toggleable
{
    bool On {
        get { return Power; }
        set {
            Power = value;
            if (value) {
                Animator.enabled = true;
                Animator.Play("Activate");
                Retracted = false;
                EnemyComponent.SetInvulerable(false);
                OpenSound.Play();
                //Lights.color = OnColor;
            }
            else {
                CanShoot = false;
                //Lights.color = OffColor;
                if (Flashlight)
                    Flashlight.enabled = false;
            }
        }
    }

    [SerializeField]
    bool Power;

    bool CanShoot;

    [HideInInspector]
    public bool Retracted;

    public int Health;

    public float Range;

    [Tooltip("Maximumn degree change per second while aiming")]
    public float AimSpeed;

    [Tooltip("Time between shots")]
    public float ShootSpeed;

    [Tooltip("Projectile spread, in Degrees")]
    public float Spread;

    [Tooltip("Minimumn angle of the turret relative to the target that is needed to shot")]
    public float Certainty;

    public int TechPoints;

    public bool ToggleByRange;
    public float ToggleRange;

    public GameObject Projectile;

    public Transform Swivel;

    public List<Transform> BarrelPositions;

    //public SpriteRenderer Lights;

    //public Color OffColor;
    //public Color OnColor;

    public UnityEngine.Experimental.Rendering.Universal.Light2D Flashlight;

    public GameObject DeadTurret;

    AudioSource OpenSound;

    Animator Animator;

    FlushTurretEnemy EnemyComponent;

    AudioSource ShotNoise;

    Transform Player;

    float TargetAngle;


    float TimeSinceShot;

    int ShootIndex;


    void FixedUpdate() {
        if (!Power) {
            if (ToggleByRange && new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y).magnitude < ToggleRange)
                On = true;
            else
                return;
        }
        
        Vector2 RelPlyPos = new Vector2(Player.transform.position.x - Swivel.transform.position.x, Player.transform.position.y - Swivel.transform.position.y);

        if (CanShoot && RelPlyPos.magnitude < Range) {
            TargetAngle = Vector2.SignedAngle(Vector2.down, RelPlyPos);
            //if (TargetAngle < 0)
                //TargetAngle += 360;
        }
        else
            TargetAngle = 0;
    }

    void Update() {
        if (!Power) {
            if (!Retracted) {
                Swivel.Rotate(new Vector3(0, 0, Mathf.MoveTowardsAngle(Swivel.rotation.eulerAngles.z, TargetAngle, AimSpeed * Time.deltaTime)));
                if (Swivel.eulerAngles.z == 0) {
                    Animator.enabled = true;
                    Animator.Play("Retract");
                    Retracted = true;
                    EnemyComponent.SetInvulerable(true);
                }
            }
            else
                return;
        }

        //Rotate Turret
        Swivel.eulerAngles = new Vector3(0, 0, Mathf.MoveTowardsAngle(Swivel.rotation.eulerAngles.z, TargetAngle, AimSpeed * Time.deltaTime));

        //Shoot
        TimeSinceShot += Time.deltaTime;

        Vector2 RelPlyPos = new Vector2(Player.transform.position.x - Swivel.position.x, Player.transform.position.y - Swivel.position.y);

        if (TimeSinceShot >= ShootSpeed && Vector2.Angle(Vector2.down, RelPlyPos) - Swivel.eulerAngles.z <= Certainty && CanShoot) {
            Instantiate(Projectile, BarrelPositions[ShootIndex].position, Quaternion.Euler(Swivel.rotation.eulerAngles.x, Swivel.rotation.eulerAngles.y, Swivel.rotation.eulerAngles.z + Random.Range(-Spread, Spread)));
            TimeSinceShot = 0;
            ShotNoise.Play();
            ShootIndex++;
            if (ShootIndex >= BarrelPositions.Count)
                ShootIndex = 0;
        }
    }

    public void OnDeath() {
        Instantiate(DeadTurret, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void Active() {
        CanShoot = true;
        
        if (Flashlight)
            Flashlight.enabled = true;
    }

    public void DisableAnimator() {
        Animator.enabled = false;
    }

    void Awake() {
        Animator = GetComponent<Animator>();
        Player = FindObjectOfType<Player>().transform;
        EnemyComponent = GetComponentInChildren<FlushTurretEnemy>();
        EnemyComponent.SetToggle(this);
        ShotNoise = GetComponent<AudioSource>();
        OpenSound = transform.GetChild(0).GetComponent<AudioSource>();
    }

    public override void Trigger() {
        On = !On;
    }

    public override float TriggerTime() {
        return 0.5f;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ToggleRange);
    }
}
