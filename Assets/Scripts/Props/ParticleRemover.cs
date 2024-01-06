using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRemover : MonoBehaviour
{
    public float TimeUntilDestruction;

    float ElapsedTime;

    void Update()
    {
        if (ElapsedTime >= TimeUntilDestruction)
            Destroy(gameObject);
        else
            ElapsedTime += Time.deltaTime;
    }
}
