using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirranah : Enemy
{
    public float Speed = 4f;
    public float Acceleration = 1f;

    public LayerMask LM;

    Animator Animator;

    float CurrentStun;

    float CurrentAttack;
    public void OnTakeDamage(EnemyDamageInfo Info)
    {   
        CurrentStun += Info.Stun;
    }

    public void OnDeath() {
        Animator.Play("PirranahDie");
        Speed = 0f;
        Acceleration = 0f;
    }

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Player = FindObjectOfType<Player>().transform;
    }

    void FixedUpdate()
    {        
        Vector2 Direction = Vector2.ClampMagnitude(new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y), 1f);

        if (Physics2D.OverlapCircle(transform.position, DetectionRadius, PlayerMask) && CurrentStun <= 0 && Physics2D.Linecast(transform.position, Player.transform.position, LM).transform.tag == "Player")
        {
            Rigidbody.AddForce(Direction * Acceleration, ForceMode2D.Impulse);
        }

        if (Rigidbody.velocity.magnitude > Speed && CurrentStun <= 0)
            Rigidbody.velocity = Vector2.ClampMagnitude(Rigidbody.velocity, Speed);

        Animator.SetFloat("Speed", Rigidbody.velocity.magnitude);

        //Flip Sprite
        if (Direction.x < -0.05 && !Dead)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Direction.x > 0.05 && !Dead)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }

        
    }

    void OnCollisionEnter2D(Collision2D Collider)
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

    void OnCollisionStay2D(Collision2D Collider)
    {
        if (Collider.transform.tag == "Player" && CurrentAttack > 1)
        {
            Collider.transform.GetComponent<Player>().TakeDamage(Damage);
            CurrentAttack = 0;
        }
    }
}
