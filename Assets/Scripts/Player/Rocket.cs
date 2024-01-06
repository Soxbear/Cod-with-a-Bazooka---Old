using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float Thrust = 1f;
    public float MaxLife = 3f;
    public GameObject Explosion;
    public float ExplosionSize = 5f;
    public float ExplosionForce = 10f;
    public int ExplosionDamage = 15;
    public bool DamagePlayer = false;
    public float Stun = 1f;
    public LayerMask EnemyMask;
    public LayerMask PlayerMask;
    Rigidbody2D Rigidbody;
    public ContactFilter2D EnemyFilter;
    Collider2D[] Enemies;
    Collider2D[] Props;
    bool HasCollided;
    float ElapsedTime;
    GameObject Player;

    public AudioClip[] Sounds;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<Player>().gameObject;
        Rigidbody.AddRelativeForce(new Vector2(Thrust * 10, 0), ForceMode2D.Impulse);   
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody.AddRelativeForce(new Vector2(Thrust, 0), ForceMode2D.Impulse);        
    }

    void Update()
    {
        if (HasCollided)
        {
            Player.GetComponent<Player>().ShakeScreen(0.2f, 2.5f);
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(Rigidbody);
            Destroy(gameObject, 2f);
            Destroy(this);
        }

        if (ElapsedTime >= MaxLife)
            Explode();
        else
            ElapsedTime += Time.deltaTime;
    }

    void OnCollisionEnter2D()
    {
        Explode();
    }

    void Explode()
    {
        
        
        GetComponent<AudioSource>().Play();

        Instantiate(Explosion, transform.position, transform.rotation);


        Enemies = Physics2D.OverlapCircleAll(transform.position, ExplosionSize/*, EnemyFilter, Enemies*/);
        foreach (Collider2D Enemy in Enemies)
        {
            if (Enemy.tag == "Enemy" || Enemy.tag == "Prop")
            {
                Vector2 Direction = Enemy.transform.position - transform.position;
                float Distance = Direction.magnitude;
                Mathf.Clamp(Distance, 1, ExplosionSize);
                if (Direction.magnitude < 1)
                    Direction *= 1f / Direction.magnitude;
                else
                    Direction = Vector2.ClampMagnitude(Direction, 1);
                Enemy.transform.GetComponent<Rigidbody2D>().AddForceAtPosition(Direction * ExplosionForce /* / Mathf.Clamp(Distance, 1, 50)*/, new Vector2(transform.position.x, transform.position.y), ForceMode2D.Impulse);

                if (Enemy.tag == "Enemy")
                    Enemy.GetComponent<Enemy>().TakeDamage(ExplosionDamage / Mathf.Clamp(Mathf.RoundToInt(Distance), 1, 50), Stun);

            }
            else if (Enemy.tag == "Breakable Prop")
            {
                Enemy.GetComponent<BreakProp>().Change();
            }

            //Enemy.GetComponent<Pirranah>().TakeDamage(ExplosionDamage / Mathf.RoundToInt(Distance));
        }

        
        if (Physics2D.OverlapCircle(transform.position, ExplosionSize, PlayerMask))
        {
            Vector2 Direction = Player.transform.position - transform.position;
            if (Direction.magnitude < 1)
                Direction *= 1f / Direction.magnitude;
            else
                Direction = Vector2.ClampMagnitude(Direction, 1);
            Player.GetComponent<Rigidbody2D>().AddForce(Direction * ExplosionForce, ForceMode2D.Impulse);

            if (DamagePlayer) {
                float Distance = Direction.magnitude;
                Mathf.Clamp(Distance, 1, ExplosionSize);
                if (Direction.magnitude < 1)
                    Direction *= 1f / Direction.magnitude;
                else
                    Direction = Vector2.ClampMagnitude(Direction, 1);
            
                Physics2D.OverlapCircle(transform.position, ExplosionSize, PlayerMask).GetComponent<Player>().TakeDamage(ExplosionDamage / Mathf.Clamp(Mathf.RoundToInt(Distance), 1, 50));
            }
        }

        HasCollided = true;
    }
}
