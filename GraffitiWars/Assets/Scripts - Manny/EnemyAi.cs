using UnityEngine;


public class EnemyAi : MonoBehaviour
{

    public GameObject AttackBox;
    public Transform player; // Reference to the player
    public float moveSpeed = 5f; // Speed at which the enemy moves
    public float attackRange = 1.5f; // Distance at which the enemy attacks
    public float attackCooldown = 1.5f; // Time between attacks
    

    public int damage = 25;

    private bool isAttacking = false;
    private Rigidbody rb;

    private Animator animator;

    public HealthSystem HP;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found! Ensure the player GameObject has the 'Player' tag.");
        }

        /*if (HP == null)
        {
            Debug.LogError("HealthSystem component not found on the player object!");
        }*/
    }

    /*void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("Player not found! Ensure the player GameObject has the 'Player' tag.");
            }
        }
        //player = W_PlayerStateManager.Main.transform;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.Play("Idle");
    }*/

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
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            animator.Play("ForwardWalk");
            MoveTowardsPlayer();
        }
        else if (!isAttacking)
        {
            AttackPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);


        //Debug.Log("Enemy is moving towards the player");
    }

    void AttackPlayer()
    {
        if (player == null) return;

        animator.Play("Punch");
        AttackBox.SetActive(true);
        isAttacking = true;
        Debug.Log("Enemy is attacking the player");

        // Simulate attack duration
        Invoke("ResetAttack", attackCooldown);
    }

    void ResetAttack()
    {
        AttackBox.SetActive(false);
        animator.Play("Idle");
        isAttacking = false;
        Debug.Log("Enemy is ready to attack again");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hit_Enemy"))
        {
            Debug.Log("Enemy Took 25 Damage");
            if (HP != null)
            {
                HP.TakeDamage(damage);
            }
            
        }
    }
}
