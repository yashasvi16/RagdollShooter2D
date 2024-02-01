using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    Rigidbody2D rb;
    bool hasHit = false;
    bool shootingFlag = false;

    private Camera camera;
    Vector3 bottomLeftWorld;
    Vector3 topRightWorld;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        bottomLeftWorld = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        topRightWorld = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = PlayerInput.instance.touch;
            if (touch.phase == TouchPhase.Ended)
            {
                shootingFlag = true;
            }
        }
        if (!hasHit && shootingFlag)
        {
            TrackMovement();

            if (transform.position.x >= topRightWorld.x || transform.position.y <= bottomLeftWorld.y)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject, 7f);
            }
            
        }
        
    }

    void TrackMovement()
    {
        Vector2 direction = rb.velocity;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Red") || collision.gameObject.CompareTag("Yellow") ||
            collision.gameObject.CompareTag("Green"))
        {
            if(collision.gameObject.CompareTag("Red"))
            {
                if (PlayerHealth.Instance.currentHealth <= 40)
                    PlayerHealth.Instance.currentHealth = PlayerHealth.Instance.currentHealth + 60;
                else
                    PlayerHealth.Instance.currentHealth = 100;
            }
            else if(collision.gameObject.CompareTag("Yellow"))
            {
                if (PlayerHealth.Instance.currentHealth <= 60)
                    PlayerHealth.Instance.currentHealth = PlayerHealth.Instance.currentHealth + 40;
                else
                    PlayerHealth.Instance.currentHealth = 100;

                if (PlayerInput.instance.stamina <= 40)
                    PlayerInput.instance.stamina = PlayerInput.instance.stamina + 60;
                else
                    PlayerInput.instance.stamina = 100;
            }
            else if(collision.gameObject.CompareTag("Green"))
            {
                if (PlayerInput.instance.stamina <= 40)
                    PlayerInput.instance.stamina = PlayerInput.instance.stamina + 60;
                else
                    PlayerInput.instance.stamina = 100;
            }
        }
        else if(collision.gameObject.CompareTag("Fireball"))
        {
            Destroy(gameObject);
        }
        else
        {
            hasHit = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            if (collision.gameObject.CompareTag("Enemy"))
            {
                GameObject hit = collision.gameObject;
                EnemyHealth health = hit.GetComponentInParent<EnemyHealth>();

                if (health != null)
                {
                    health.TakeDamage();
                }
            }

            Collider2D col = gameObject.GetComponent<Collider2D>();
            col.enabled = false;


            gameObject.transform.SetParent(collision.transform);
        }  
    }
}
