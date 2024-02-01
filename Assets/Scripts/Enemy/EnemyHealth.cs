using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Balancing[] balance;
    public bool healthTrigger = false;
    public float maxHealth = 100;
    public float currentHealth;
    public float damage;

    public RectTransform healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage()
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            foreach (var item in balance)
            {
                item.enabled = false;
            }

            healthTrigger = true;
            currentHealth = 0;
        }

        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);;
    }
}
