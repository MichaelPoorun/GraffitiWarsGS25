using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float moveSpeed = 5f; // Speed at which the enemy moves
    public float attackRange = 1.5f; // Distance at which the enemy attacks
    public float attackCooldown = 1f; // Time between attacks

    private bool isAttacking = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not assigned in EnemyAI script.");
            return;
        }
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            if (!isAttacking)
            {
                AttackPlayer();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);

        Debug.Log("Enemy is moving towards the player");
    }

    void AttackPlayer()
    {
        isAttacking = true;
        Debug.Log("Enemy is attacking the player");

        // Simulate attack duration
        Invoke("ResetAttack", attackCooldown);
    }

    void ResetAttack()
    {
        isAttacking = false;
        Debug.Log("Enemy is ready to attack again");
    }
}
