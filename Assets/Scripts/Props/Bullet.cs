using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D Rigidbody;

    public Vector2 MoveVector;
    public float MaxSpeed;

    void Update()
    {
        Rigidbody.AddRelativeForce(new Vector2(0, -100), ForceMode2D.Impulse);
        Rigidbody.velocity = Vector2.ClampMagnitude(Rigidbody.velocity, 30);
    }

    void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.transform.tag == "Player")
            Collider.transform.GetComponent<Player>().TakeDamage(1);
        else if (Collider.transform.tag == "Enemy")
            Collider.transform.GetComponent<Enemy>().TakeDamage(1, 0);
        Destroy(gameObject);
    }

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }
}
