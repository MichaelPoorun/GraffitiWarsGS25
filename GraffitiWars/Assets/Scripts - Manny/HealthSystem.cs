using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log(gameObject.name + " Health: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log(gameObject.name + " healed " + amount + ". Current health: " + currentHealth);
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        // You can add death animations, effects, or destroy the object here
        Destroy(gameObject);
    }
}

