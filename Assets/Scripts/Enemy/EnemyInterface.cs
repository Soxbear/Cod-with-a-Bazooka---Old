using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInterface : MonoBehaviour
{
}

public abstract class Enemy : MonoBehaviour
{
    public int Health;
    public float DetectionRadius;
    public int Damage;

    [HideInInspector]
    public bool Dead;

    public int DNA;
    public int Tech;

    [SerializeField]
    protected LayerMask PlayerMask;
    [SerializeField]
    protected LayerMask DetectionMask;

    protected Rigidbody2D Rigidbody;

    protected Transform Player;

    protected bool Invulnerable;

    public void TakeDamage(int Amount, float Stun) {
        if (Dead || Invulnerable)
            return;

        Health -= Amount;
        if (Health <= 0)
        {
            Dead = true;
            Damage = 0;
            DetectionRadius = 0f;
            if (Rigidbody) {
                Rigidbody.gravityScale = 0.5f;
                Rigidbody.constraints = RigidbodyConstraints2D.None;
                if (Mathf.Abs(Rigidbody.angularVelocity) < 0.05f)
                    Rigidbody.AddTorque(2.5f);
            }            
            if (FindObjectOfType<EventHandler>())
                FindObjectOfType<EventHandler>().RegisterEvent(Objectives.ObjectiveEvent.Kill, gameObject);

            if (DNA != 0) {
                ResourceCollectable Rc = Instantiate(FindObjectOfType<EventHandler>().Constants.DNA, transform.position, new Quaternion()).GetComponent<ResourceCollectable>();
                Rc.Amount = DNA;
            }
            if (Tech != 0) {
                Debug.Log(FindObjectOfType<EventHandler>());
                Debug.Log(transform.position);
                ResourceCollectable Rc = Instantiate(FindObjectOfType<EventHandler>().Constants.Tech, transform.position, new Quaternion()).GetComponent<ResourceCollectable>();
                Rc.Amount = Tech;
            }

            SendMessage("OnDeath", 0, SendMessageOptions.DontRequireReceiver);
        }

        SendMessage("OnTakeDamage", new EnemyDamageInfo(Amount, Stun), SendMessageOptions.DontRequireReceiver);
    }

    public void Destroy() {
        Destroy(gameObject);
    }
}

public struct EnemyDamageInfo {
    public int Amount;
    public float Stun;

    public EnemyDamageInfo(int Amnt, float Stn) {
        Amount = Amnt;
        Stun = Stn;
    }
}