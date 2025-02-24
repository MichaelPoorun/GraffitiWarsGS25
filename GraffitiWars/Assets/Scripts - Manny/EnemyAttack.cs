using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 10; // Damage per hit

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Player took damage!");
            }
        }
    }
}
