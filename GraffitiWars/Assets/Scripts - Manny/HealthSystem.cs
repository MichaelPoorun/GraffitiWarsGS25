using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100; // Max HP
    private int currentHealth;  // Current HP

    public Image healthBar; // Assign this in the Inspector for UI
    public GameObject deathEffect; // Assign a particle effect for death (optional)

    private Renderer objectRenderer; // For flashing effect

    void Start()
    {
        currentHealth = maxHealth;
        objectRenderer = GetComponent<Renderer>();
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage! HP: " + currentHealth);

        FlashRed(); // Visual feedback on hit
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log(gameObject.name + " healed " + amount + " HP!");
        UpdateHealthBar();
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // Remove the object from the game
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    private void FlashRed()
    {
        if (objectRenderer != null)
        {
            StartCoroutine(FlashEffect());
        }
    }

    private IEnumerator FlashEffect()
    {
        objectRenderer.material.color = Color.red; // Change to red
        yield return new WaitForSeconds(0.2f); // Short delay
        objectRenderer.material.color = Color.white; // Reset color
    }
}

