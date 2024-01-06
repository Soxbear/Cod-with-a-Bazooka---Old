using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pufferfish : Enemy
{
    public float PoisionRadius;
    public GameObject PoisonCloud;

    public float Speed = 4f;
    public float Acceleration = 1f;

    Animator Animator;

    float CurrentStun;

    float CurrentAttack;

    public bool Inflated;

    SpriteRenderer Renderer;
    SpriteRenderer SpikeRend;
    Transform Eye;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Player = FindObjectOfType<Player>().transform;
        Renderer = GetComponent<SpriteRenderer>();
        SpikeRend = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Eye = transform.GetChild(1);
    }

    public void OnDeath() {
            Speed = 0f;
            Acceleration = 0f;
            Instantiate(PoisonCloud, transform.position, new Quaternion());
            Animator.Play("Die");
    }

    public void OnTakeDamage(EnemyDamageInfo Info) {
        CurrentStun += Info.Stun;
    }

    void Puff() {
        if (!Dead)
            Instantiate(PoisonCloud, transform.position, new Quaternion());
    }

    void FixedUpdate()
    {        
        Vector2 Direction = Vector2.ClampMagnitude(new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y), 1f);

        if (Physics2D.OverlapCircle(transform.position, DetectionRadius, PlayerMask) && CurrentStun <= 0 && Physics2D.Linecast(transform.position, Player.transform.position, DetectionMask).transform.tag == "Player")
        {
            Rigidbody.AddForce(Direction * Acceleration, ForceMode2D.Impulse);
        }

        if (Rigidbody.velocity.magnitude > Speed && CurrentStun <= 0)
            Rigidbody.velocity = Vector2.ClampMagnitude(Rigidbody.velocity, Speed);

        if (Dead)
            Animator.SetFloat("Speed", 0);
        else
            Animator.SetFloat("Speed", Rigidbody.velocity.magnitude);

        //Flip Sprite
        if (Direction.x < -0.05 && !Dead)
        {
            Renderer.flipX = false;
            SpikeRend.flipX = false;
            Eye.localScale = new Vector3(1, 1, 1);
        }
        else if (Direction.x > 0.05 && !Dead)
        {
            Renderer.flipX = true;
            SpikeRend.flipX = true;
            Eye.localScale = new Vector3(-1, 1, 1);
        }

        if (CurrentStun > 0)
            CurrentStun -= Time.deltaTime;

        
        if (Physics2D.OverlapCircle(transform.position, PoisionRadius, PlayerMask) && !Inflated && !Dead) {
            Puff();
            Animator.SetBool("Inflated", true);
            Inflated = true;
            StartCoroutine(Deflate());
        }
    }

    public IEnumerator Deflate() {
        yield return new WaitForSeconds(10);

        Animator.SetBool("Inflated", false);

        yield return new WaitForSeconds(2);

        Inflated = false;

        StopCoroutine(Deflate());
    }

    void OnCollisionEnter2D(Collision2D Collider)
    {
        if (Collider.transform.tag == "Player" && CurrentAttack > 1)
        {
            Collider.transform.GetComponent<Player>().TakeDamage(Damage);
            CurrentAttack = 0;
        }
    }

    void OnCollisionStay2D(Collision2D Collider)
    {
        if (Collider.transform.tag == "Player" && CurrentAttack > 1)
        {
            Collider.transform.GetComponent<Player>().TakeDamage(Damage);
            CurrentAttack = 0;
        }
    }

    void Update()
    {
        CurrentAttack += Time.deltaTime;

        if (CurrentStun > 0)
            CurrentStun -= Time.deltaTime;
    }
}
