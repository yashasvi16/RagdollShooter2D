using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    public float maxHealth = 100;
    public float currentHealth;
    public float damage;

    public RectTransform healthBar;

    private Camera camera;
    Vector3 bottomLeftWorld;
    Vector3 topRightWorld;

    private AudioSource _hit;
    private void Start()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(Instance);

        camera = Camera.main;
        bottomLeftWorld = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        topRightWorld = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

        _hit = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }
    private void Update()
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }
    public void TakeDamage()
    {
        if(transform.position.x >= topRightWorld.x || transform.position.y <= bottomLeftWorld.y ||
            transform.position.x <= bottomLeftWorld.x || transform.position.y >= topRightWorld.y)
        {
            currentHealth = 0;
        }
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            _hit.Play();
            currentHealth = 0;
            UIManager.Instance.playerDead = true;
        }  
    }
}
