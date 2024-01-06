using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrigger : MonoBehaviour, Triggers.TriggerEffect
{
    [field : SerializeField]
    public bool IsObjectiveEffect{get; set;}

    public Triggers.Trigger Trigger;

    public float Activate() {
        Trigger.Activate();
        return 0;
    }
}
