using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float floatForce = 5.0f;

    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * floatForce, ForceMode.Acceleration);
    }
}
