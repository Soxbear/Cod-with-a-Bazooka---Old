using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMousePos : MonoBehaviour
{    
    void Update()
    {
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TargetPos.z = 0;
        transform.position = TargetPos;
    }
}
