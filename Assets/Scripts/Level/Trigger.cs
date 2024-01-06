using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers {

[RequireComponent(typeof(Collider2D))]
public class Trigger : MonoBehaviour
{
    private int WaitForReturn;
    bool Activated = false;
    private List<float> times;
    void OnTriggerEnter2D(Collider2D Col) {

        if (Activated || !Col.transform.CompareTag("Player"))
            return;
        
        Activated = true;
        times = new List<float>();

        foreach (TriggerEffect Effect in GetComponents<TriggerEffect>()) {

            if (Effect.IsObjectiveEffect) continue;

            float Time = Effect.Activate();

            times.Add(Time);

            if (Time == -1)
                WaitForReturn++;                
        }
        if (WaitForReturn == 0)
            Destroy(gameObject, Mathf.Max(times.ToArray()));
    }

    public void Activate() {
        if (Activated)
            return;
        
        Activated = true;
        times = new List<float>();

        foreach (TriggerEffect Effect in GetComponents<TriggerEffect>()) {

            if (Effect.IsObjectiveEffect) continue;

            float Time = Effect.Activate();

            times.Add(Time);

            if (Time == -1)
                WaitForReturn++;                
        }
        if (WaitForReturn == 0)
            Destroy(gameObject, Mathf.Max(times.ToArray()));
    }
    
    public void ReturnCall() {
        WaitForReturn--;
        if (WaitForReturn == 0)
            Destroy(gameObject);
    }
}

public interface TriggerEffect {
    public bool IsObjectiveEffect{get; set;}
    public float Activate();
}

}