using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health
    public int currentHealth;
    public int damage = 25;

    //Misha inserted this, talk to him about it
    public DuHealthBar HB;
    
 

    void Start()
    {
        currentHealth = maxHealth;
        HB.Updatehp(maxHealth, currentHealth);//Misha added this
        Debug.Log(gameObject.name + " Health: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Current health: " + currentHealth);
        HB.Updatehp(maxHealth, currentHealth);//Misha added this
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
        HB.Updatehp(maxHealth, currentHealth);//Misha added this
        Debug.Log(gameObject.name + " healed " + amount + ". Current health: " + currentHealth);
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        // You can add death animations, effects, or destroy the object here
        Destroy(gameObject);
    }
    
}

