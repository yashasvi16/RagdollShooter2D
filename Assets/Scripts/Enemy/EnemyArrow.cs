using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    Rigidbody2D rb;
    bool hasHit = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = gameObject.GetComponent<Collider2D>();
        col.enabled = false;

        hasHit = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        
        if(collision.gameObject.CompareTag("Player"))
        {
            GameObject hit = collision.gameObject;
            PlayerHealth health = hit.GetComponentInParent<PlayerHealth>();

            if(health != null)
            {
                health.TakeDamage();
            }   
        }

        gameObject.transform.SetParent(collision.transform);
        Destroy(gameObject, 3f);
    }
}
