using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public delegate void ObjEvnt(Objectives.ObjectiveEvent EventType, GameObject Object);

    public static event ObjEvnt ObjectiveEvent;

    public void RegisterEvent(Objectives.ObjectiveEvent EventType, GameObject Object) {
        ObjectiveEvent(EventType, Object);
    }

    public Constants Constants;
}

[System.Serializable]
[SerializeField]
public class Constants {
    [SerializeField]
    public GameObject DNA;
    [SerializeField]
    public GameObject Tech;
}