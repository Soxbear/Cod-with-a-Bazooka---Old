using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;

namespace Interactables.Components {
public class Door : Toggleable
{
    [SerializeField]
    private float OpenTime;
    private Animator Anim;
    private bool Active;

    public override void Trigger() {
        Anim.enabled = true;

        if (Active)
            Anim.Play("Close");
        else
            Anim.Play("Open");

        Active = !Active;
    }

    public void DisableAnimator() {
        Anim.enabled = false;
    }

    public override float TriggerTime() {
        return 1/OpenTime;
    }

    void Start() {
        Anim = GetComponent<Animator>();
        Anim.speed = 1/OpenTime;
        Anim.enabled = false;
    }
}

}