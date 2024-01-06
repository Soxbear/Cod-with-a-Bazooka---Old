using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public Animator FinAnim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FinAnim.gameObject.SetActive(true);
            FinAnim.Play("End");
            collision.GetComponent<Player>().MaxHealth = 100000;
            collision.GetComponent<Player>().TakeDamage(-100000);
            collision.GetComponent<Player>().MaxSpeed = 0;
            collision.GetComponent<Player>().CanShoot = false;
        }
    }
}
