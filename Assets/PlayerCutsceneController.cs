using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCutsceneController : MonoBehaviour
{
    PolygonCollider2D Collider;

    PlayerMode Mode;

    void Start()
    {
        Collider = transform.GetChild(0).GetChild(0).GetComponent<PolygonCollider2D>();
    }

    public void ColliderOff() {
        Collider.enabled = false;
    }
    public void ColldierOn() {
        Collider.enabled = true;
    }
}
