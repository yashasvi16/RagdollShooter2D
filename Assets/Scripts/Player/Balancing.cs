using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Balancing : MonoBehaviour
{
    public Rigidbody2D rb;
    public float force;
    public float targetRotation;

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetRotation, force * Time.fixedDeltaTime));
    }
}
