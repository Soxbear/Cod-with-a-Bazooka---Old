using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Props {

public interface Toggleable {
    public void Toggle();
}











public class Prop : MonoBehaviour
{
    Transform PlayerPosition;
    Rigidbody2D Rigidbody;
    SpriteRenderer Renderer;

    void Start()
    {
        PlayerPosition = FindObjectOfType<Player>().transform;
        Rigidbody = GetComponent<Rigidbody2D>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Vector2 Distance = new Vector2(transform.position.x - PlayerPosition.position.x, transform.position.y - PlayerPosition.position.y);
        if (Distance.magnitude > 12)
        {
            Rigidbody.Sleep();
            Renderer.enabled = false;
        }
        else
        {
            Rigidbody.WakeUp();
            Renderer.enabled = true;
        }
    }
}

public interface ChangableObject
{
    public void Change();
}

}