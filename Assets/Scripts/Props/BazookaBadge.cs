using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaBadge : MonoBehaviour
{
    public GameObject Particles;
    public int ID;
    void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.tag == "Player")
        {
            PlayerPrefs.SetInt("BB" + ID.ToString(), 1);
            PlayerPrefs.SetInt("BBUnlocked", 1);
            PlayerPrefs.SetInt("BB", PlayerPrefs.GetInt("BB") + 1);
            Player Player = FindObjectOfType<Player>();
            FindObjectOfType<InGameMenu>().Music.GetComponent<Animator>().Play("BBMusicDim");
            PlayerPrefs.SetInt("Bazooka Badges", PlayerPrefs.GetInt("Bazooka Badges") + 1);
            Player.TakeDamage(-Player.MaxHealth);
            Instantiate(Particles, transform);
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<CircleCollider2D>());
            GetComponent<AudioSource>().Play();
            Destroy(gameObject, 5);
            Destroy(this);
        }        
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("BB" + ID.ToString()) == 1)
        {
            Destroy(gameObject);
        }
    }
}
