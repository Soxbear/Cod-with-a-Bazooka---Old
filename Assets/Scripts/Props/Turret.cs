using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    
    public float Speed;
    public bool DoubleBarrel;


    Player Player;
    AudioSource Audio;

    public LayerMask LM;

    public GameObject Bullet;

    bool Left;
    float Elapsed;

    void FixedUpdate()
    {
        if (new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y).magnitude < 9)
        {
            RaycastHit2D RC = Physics2D.Linecast(transform.position, Player.transform.position, LM);
            if (RC.transform.tag == "Player")
            {
                transform.up = -(Player.transform.position - transform.position);

                if (Elapsed >= Speed)
                {
                    if (Left && DoubleBarrel)
                        Instantiate(Bullet, transform.GetChild(1).position, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 90));
                    else
                        Instantiate(Bullet, transform.GetChild(0).position, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 90));
                                   

                    Left = !Left;
                    Elapsed = 0;
                    Audio.Play();
                }
                else
                    Elapsed += Time.deltaTime;
            }
        }        
    }

    void Start()
    {
        Player = FindObjectOfType<Player>();
        Audio = GetComponent<AudioSource>();
    }
}
