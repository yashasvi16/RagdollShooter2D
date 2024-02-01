using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Animator _animator;

    private Camera camera;
    Vector3 bottomLeftWorld;
    Vector3 topRightWorld;
    private void Start()
    {
        _animator = GetComponent<Animator>();

        camera = Camera.main;
        bottomLeftWorld = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        topRightWorld = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
    }

    private void Update()
    {
        if(transform.position.x <= bottomLeftWorld.x || transform.position.y <= bottomLeftWorld.y || transform.position.y >= topRightWorld.y)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = gameObject.GetComponent<Collider2D>();
        col.enabled = false;

        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject hit = collision.gameObject;
            PlayerHealth health = hit.GetComponentInParent<PlayerHealth>();

            if (health != null)
            {
                health.TakeDamage();
            }
        }

        _animator.SetTrigger("explode");
        Destroy(gameObject, 0.25f);
    }
}
