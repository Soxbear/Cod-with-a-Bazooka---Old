using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables {

[SerializeField]
public abstract class Toggleable : MonoBehaviour
{
    public abstract void Trigger();
    public abstract float TriggerTime();
}

}