using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollectable : MonoBehaviour
{
    public Resource Resource;
    public int Amount = 1;

    void OnTriggerEnter2D(Collider2D col) {
        col.GetComponent<Player>().AddResource(Resource, Amount);
        Destroy(gameObject);
    }

    float RotateSpeed;
    void Start()
    {
        RotateSpeed = Random.Range(-45f, 45f);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, RotateSpeed * Time.deltaTime));
    }
}
