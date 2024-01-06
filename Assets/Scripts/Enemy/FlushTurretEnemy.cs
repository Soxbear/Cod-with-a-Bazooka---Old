using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlushTurretEnemy : Enemy
{
    FlushTurretToggle Toggle;

    public void SetToggle(FlushTurretToggle _Toggle) {
        Toggle = _Toggle;
    }

    public void Start() {
        Health = Toggle.Health;
        Tech = Toggle.TechPoints;
    }

    void OnDeath() {
        Toggle.OnDeath();
    }

    public void SetInvulerable(bool State) {
        Invulnerable = State;
    }
}
