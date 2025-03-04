using UnityEngine;

public enum PlayerState //Where all player states are kept
{
    Idle,
    Walking,
    Jumping
}

public class NEWPlayerState_W : MonoBehaviour
{
    public float speed; //Player Speed
    public float jumpPower;

    private Rigidbody rb;

    public bool isJumping = false;
    public Animator animator;

    public PlayerState currentState; //Variable that stores current state

    private void Start()
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
                Debug.Log("NEW Player is IDLE");
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    ChangeState(PlayerState.Walking);
                }
                
                if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
                {
                    animator.SetTrigger("isJumping"); //Triggers the animation based on the trigger that was made in the animator
                    ChangeState(PlayerState.Jumping);
                }
                    break;

            case PlayerState.Walking:
                Debug.Log("NEW Player is Walking");
                PlayerMovement();
                if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
                {
                    ChangeState(PlayerState.Idle);
                }
                
                if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
                {
                    animator.SetTrigger("isJumping"); //Triggers the animation based on the trigger that was made in the animator
                    ChangeState(PlayerState.Jumping);
                }
                break;

            case PlayerState.Jumping:
                Debug.Log("NEW PLAYER IS JUMPING");
                isJumping = true;
                Jumping();
                break;
        }
    }
    public void ChangeState(PlayerState newState)
    {
        currentState = newState;
        animator.SetBool("isWalking", newState == PlayerState.Walking); //SetBool makes the animator bool T or F. Same as doing if isWalking == true {play animation} else {iswalking == false}
    }

    //======================================================================//
    //                          Player Movement                             //   
    //======================================================================//
    public void PlayerMovement()
    {
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(x * speed, 0, z * speed) * Time.deltaTime);
    }

    //======================================================================//
    //                             Player Jump                              //   
    //======================================================================//
    void Jumping()
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
}
