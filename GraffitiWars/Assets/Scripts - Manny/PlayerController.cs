using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the player
    private Rigidbody rb;
    private Vector3 movement;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    void HandleMovement()
    {
        if (isAttacking) return; // Prevent movement while attacking

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        movement = new Vector3(moveX, 0, moveZ).normalized * moveSpeed;

        if (movement.magnitude > 0)
        {
            Debug.Log("Player is moving");
        }
        else
        {
            Debug.Log("Player is idle");
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    void HandleAttack()
    { 
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            isAttacking = true;
            Debug.Log("Player is attacking");
            Invoke("EndAttack", 0.5f); // Simulate attack duration of 0.5 seconds
        }
    }

    void EndAttack()
    {
        isAttacking = false;
        Debug.Log("Player finished attacking");
    }
}