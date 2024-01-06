using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticProp : MonoBehaviour
{
    Transform PlayerPosition;
    SpriteRenderer Renderer;

    void Start()
    {
        PlayerPosition = FindObjectOfType<Player>().transform;
    }

    void FixedUpdate()
    {
        Vector2 Distance = new Vector2(transform.position.x - PlayerPosition.position.x, transform.position.y - PlayerPosition.position.y);
        if (Distance.magnitude > 15)
        {
            Renderer.enabled = false;
        }
        else
        {
            Renderer.enabled = true;
        }
    }
}
