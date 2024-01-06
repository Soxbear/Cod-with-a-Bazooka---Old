using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Triggers;

public class Toggle : MonoBehaviour, TriggerEffect
{
    [field : SerializeField]
    public bool IsObjectiveEffect{get; set;}

    [SerializeField]
    [SerializeReference]
    public Interactables.Toggleable Toggleable;

    public float Activate() {
        Toggleable.Trigger();
        return Toggleable.TriggerTime();
    }
}
