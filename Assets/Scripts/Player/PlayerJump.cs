using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerJump : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Jump()
    {
        rb.AddForce((Vector2.up + new Vector2(-0.04f, 0)) * 3500);
    }
}
