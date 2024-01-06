using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloud : MonoBehaviour
{    
    public int Frequency;
    public int Damage;
    public float Radius;
    public float Duration;

    public LayerMask PlayerMask;

    private int Counter;

    void FixedUpdate() {

        Counter++;

        if (Counter >= Frequency) {
            if (!Physics2D.OverlapCircle(transform.position, Radius, PlayerMask))
                return;
            
            Physics2D.OverlapCircle(transform.position, Radius, PlayerMask).GetComponent<Player>().TakeDamage(Damage);
            
            Counter = 0;
        }       
        
    }

    void Awake() {
        Destroy(gameObject, Duration);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
