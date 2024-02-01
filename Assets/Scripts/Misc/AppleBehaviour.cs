using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppleBehaviour : MonoBehaviour
{
    GameObject arrow;
    bool flag = false;

    private void Update()
    {
        if(flag)
        {
            transform.position = arrow.transform.position;
            transform.rotation = arrow.transform.rotation;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Arrow"))
        {
            arrow = collision.gameObject;
            gameObject.GetComponent<Collider2D>().enabled = false; 
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            flag = true;
        }
    }
}
