using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers {

public class Audio : MonoBehaviour, TriggerEffect
{
    [field : SerializeField]
    public bool IsObjectiveEffect{get; set;}
    public AudioClip Clip;
    public float Activate() {
        AudioSource s = gameObject.AddComponent<AudioSource>();
        s.clip = Clip;
        s.Play();        
        return Clip.length;
    }
}

}