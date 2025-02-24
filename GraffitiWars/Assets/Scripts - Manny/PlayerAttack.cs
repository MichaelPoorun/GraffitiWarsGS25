using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 20; // Damage per hit

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Ensure enemies have the "Enemy" tag
        {
            HealthSystem enemyHealth = other.GetComponent<HealthSystem>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
                Debug.Log("Enemy took damage!");
            }
        }
    }
}
