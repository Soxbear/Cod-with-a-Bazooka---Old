using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWeightAdjuster : MonoBehaviour
{
    public float MouseInfluence;

    public float SafetyPercentage;

    Cinemachine.CinemachineTargetGroup Group;

    // Start is called before the first frame update
    void Start()
    {
        Group = GetComponent<Cinemachine.CinemachineTargetGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            Group.m_Targets[1].weight = (Mathf.Max(Vector2.ClampMagnitude(new Vector2(Input.mousePosition.x - (Screen.width/2), Input.mousePosition.y - (Screen.height/2)), Mathf.Min(Screen.width, Screen.height) / 2).magnitude - ((Mathf.Min(Screen.width, Screen.height) / 2) * SafetyPercentage), 0) / Mathf.Max((Mathf.Min(Screen.width, Screen.height) / 2) - ((Mathf.Min(Screen.width, Screen.height) / 2) * SafetyPercentage), 0)) * MouseInfluence;
        else
            Group.m_Targets[1].weight = 0;
    }
}
