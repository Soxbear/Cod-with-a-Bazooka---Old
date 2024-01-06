using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Triggers;

namespace Objectives {

public class KillEnemiesObjective : MonoBehaviour, Objectives.Objective
{
    public int NumberOfEnemiesToKill = 3;
    public void GiveEvent(ObjectiveEvent Event, GameObject Object) {
        if (Event == ObjectiveEvent.Kill)
            NumberOfEnemiesToKill--;

        if (NumberOfEnemiesToKill == 0)
            GetComponent<ObjectiveController>().Completed();
    }
}

}