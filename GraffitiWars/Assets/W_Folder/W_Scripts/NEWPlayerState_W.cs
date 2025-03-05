using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState //Where all player states are kept
{
    Idle,
    Walking,
    Attacking,
    Jumping,
    Blocking
}

public class NEWPlayerState_W : MonoBehaviour
{
    [Header("Player Variables")]
    public float speed; //Player Speed
    public float jumpPower;

    [Header("Player Bools")]
    public bool currentlyAttacking = false;
    public bool isJumping = false;
    public bool isBlocking = false;

    [Header("Player Attack Hitboxes")]
    public GameObject AttackBox1;

    [Header("References")]
    public HealthSystem HP;

    [Header("Misc")]
    private Rigidbody rb;
    public Animator animator;
    public PlayerState currentState; //Variable that stores current state

    void Awake()
    {
        AttackBox1.SetActive(false);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        HandleState();
    }

    //======================================================================//
    //                            State Machine                             //   
    //======================================================================//
    void HandleState()
    {
        switch (currentState)
        {
            case PlayerState.Idle:
                Debug.Log("NEW Player Is IDLE");
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    ChangeState(PlayerState.Walking);
                }
                if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
                {
                    ChangeState(PlayerState.Jumping);
                }
                if (Input.GetMouseButtonDown(0) && currentlyAttacking == false)
                {
                    ChangeState(PlayerState.Attacking);
                }
                if (Input.GetMouseButton(1))
                {
                    ChangeState(PlayerState.Blocking);
                }
                break;

            case PlayerState.Walking:
                Debug.Log("NEW Player Is Walking");
                PlayerMovement();
                if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
                {
                    ChangeState(PlayerState.Idle);
                }
                if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
                {
                    ChangeState(PlayerState.Jumping);
                }
                if (Input.GetMouseButtonDown(0) && currentlyAttacking == false)
                {
                    ChangeState(PlayerState.Attacking);
                }
                break;

            case PlayerState.Attacking:
                Debug.Log("NEW Player Is Attacking");
                StartCoroutine(Attack());
                break;

            case PlayerState.Jumping:
                Debug.Log("NEW Player Is Jumping");
                isJumping = true;
                Jumping();
                break;

            case PlayerState.Blocking:
                Debug.Log("NEW Player Is Blocking");
                Blocking();
                if (Input.GetMouseButtonUp(1))
                {
                    isBlocking = false;
                    ChangeState(PlayerState.Idle);
                }
                break;
        }
    }
    public void ChangeState(PlayerState newState)
    {
        currentState = newState;
        animator.SetBool("isWalking", newState == PlayerState.Walking); //SetBool makes the animator bool T or F. Same as doing if isWalking == true {play animation} else {iswalking == false}
        animator.SetBool("isBlocking", newState == PlayerState.Blocking); //Set isBlocking to true in the animator

        if (newState == PlayerState.Attacking)
        {
            animator.SetTrigger("isAttacking"); //Triggers the punch animation based on the trigger that was made in the animator
        }
        if (newState == PlayerState.Jumping)
        {
            animator.SetTrigger("isJumping"); //Triggers the jump animation based on the trigger that was made in the animator
        }
    }

    //======================================================================//
    //                            Player Movement                           //   
    //======================================================================//
    public void PlayerMovement()
    {
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(x * speed, 0, z * speed) * Time.deltaTime);
    }

    //======================================================================//
    //                            Player Attack                             //   
    //======================================================================//
    IEnumerator Attack()
    {
        Debug.Log("Player Has Attacked");
        AttackBox1.SetActive(true);
        currentlyAttacking = true;
        yield return new WaitForSeconds(.55f);
        AttackBox1.SetActive(false);
        currentlyAttacking = false;
        ChangeState(PlayerState.Idle);
    }   

    //======================================================================//
    //                             Player Jump                              //   
    //======================================================================//
    public void Jumping()
    {
        if (isJumping)
        {
            Debug.Log("Player Has Jumped");
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); //Makes player jump
        }
    }
    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
            ChangeState(PlayerState.Idle);
        }
    }

    //======================================================================//
    //                             Player Block                             //   
    //======================================================================//
    public void Blocking()
    {
        isBlocking = true;
    }

    //======================================================================//
    //                             Player Health                            //   
    //======================================================================//
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hit_Player") && isBlocking == false)
        {
            Debug.Log("NEW Player Took 25 Damage");
            HP.TakeDamage(HP.damage);
        }
        else if (isBlocking == true)
        {
            Debug.Log("NEW Player Negated Damage");
            HP.damage = 0;
        }
    }

}
