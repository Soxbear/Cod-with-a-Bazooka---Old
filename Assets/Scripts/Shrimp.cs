using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrimp : MonoBehaviour
{
    public int HealthBoost;
    float RotateSpeed;
    void Start()
    {
        RotateSpeed = Random.Range(-45f, 45f);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, RotateSpeed * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D Col) {
        Col.gameObject.GetComponent<Player>().TakeDamage(-HealthBoost);
        Destroy(gameObject);
    }
}
