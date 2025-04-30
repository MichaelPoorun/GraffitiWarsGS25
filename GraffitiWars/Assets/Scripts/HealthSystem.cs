using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health
    public int currentHealth;
    public int damage = 25;

    [SerializeField] FloatingHealthBar healthBar;
    [SerializeField] PlayerHealthBar playerHealthBar;
    [SerializeField] BossHealthBar_W bossHealthBar;

    private bool isPlayer;
    private bool isBoss;

    private void Awake()
    {
        isPlayer = CompareTag("Player");
        isBoss = CompareTag("BossH");

        if (isPlayer)
        {
            playerHealthBar = Object.FindFirstObjectByType<PlayerHealthBar>(); // finds the player's healthbar in UI
        }
        else
        {
            healthBar = GetComponentInChildren<FloatingHealthBar>();
        }

        if (isBoss)
        {
            bossHealthBar = Object.FindFirstObjectByType<BossHealthBar_W>(); // finds the player's healthbar in UI
        }
        else
        {
            healthBar = GetComponentInChildren<FloatingHealthBar>();
        }

    }

    void Start()
    {
        currentHealth = maxHealth;

        if (isPlayer && playerHealthBar != null)
        {
           playerHealthBar.UpdatePlayerHealthBar(currentHealth, maxHealth);
        }
        else if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }

        if (isBoss && bossHealthBar != null)
        {
            bossHealthBar.UpdateBossHealthBar(currentHealth, maxHealth);
        }
        else if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
        /*healthBar.UpdateHealthBar(currentHealth, maxHealth);*/
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth); // prevents health from going under 0

        if (isPlayer && playerHealthBar != null)
        {
            playerHealthBar.UpdatePlayerHealthBar(currentHealth, maxHealth);
        }
        else if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }

        if (isBoss && bossHealthBar != null)
        {
            bossHealthBar.UpdateBossHealthBar(currentHealth, maxHealth);
        }
        else if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
            /*
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
            playerHealthBar.UpdatePlayerHealthBar(currentHealth, maxHealth);*/
            

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(maxHealth, currentHealth); // Prevent overhealing

        if (isPlayer && playerHealthBar != null)
        {
            playerHealthBar.UpdatePlayerHealthBar(currentHealth, maxHealth);
        }
        else if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
    }

    private void Die()
    {
        // Player death logic (e.g., game over) should be handled separately
        if (!isPlayer)
        {
            Destroy(gameObject);
        }
    }
}

