using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState //Where all player states are kept
{
    Idle,
    Walking,
    BasicPunch,
    ComboPunch,
    BasicKick,
    Jump,
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
    public GameObject Punch1;
    public GameObject Punch2;
    public GameObject Kick1;

    [Header("References")]
    public HealthSystem HP;

    [Header("Misc")]
    private Rigidbody rb;
    public Animator animator;
    public PlayerState currentState; //Variable that stores current state

    void Awake()
    {
        Punch1.SetActive(false);
        Punch2.SetActive(false);
        Kick1.SetActive(false);
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
                Debug.Log("In Idle State");
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    ChangeState(PlayerState.Walking);
                }
                if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
                {
                    ChangeState(PlayerState.Jump);
                }
                if (Input.GetMouseButtonDown(0) && currentlyAttacking == false)
                {
                    ChangeState(PlayerState.BasicPunch);
                }
                if (Input.GetMouseButton(1))
                {
                    ChangeState(PlayerState.Blocking);
                }
                break;

            case PlayerState.Walking:
                PlayerMovement();
                if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
                {
                    ChangeState(PlayerState.Idle);
                }
                if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
                {
                    ChangeState(PlayerState.Jump);
                }
                if (Input.GetMouseButtonDown(0) && currentlyAttacking == false)
                {
                    ChangeState(PlayerState.BasicPunch);
                }
                if (Input.GetMouseButton(1))
                {
                    ChangeState(PlayerState.Blocking);
                }
                break;

            case PlayerState.BasicPunch:
                Debug.Log("In BasicPunch State");
                StartCoroutine(BasicPunchAttack());
                break;
            
            case PlayerState.ComboPunch:
                Debug.Log("In ComboPunch State");
                StartCoroutine(ComboPunchAttack1());
                break;

            case PlayerState.BasicKick:
                Debug.Log("In Kick State");
                StartCoroutine(BasicKickAttack());
                break;

            case PlayerState.Jump:
                Debug.Log("In Jump State");
                isJumping = true;
                Jumping();
                break;

            case PlayerState.Blocking:
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

        animator.SetBool("isWalkingUp", false);
        animator.SetBool("isWalkingLeft", false);
        animator.SetBool("isWalkingDown", false);
        animator.SetBool("isWalkingRight", false);
        // animator.SetBool("isWalkingUp", newState == PlayerState.X); - Sets the Bool makes the animator bool T or F. Same as doing if isWalking == true {play animation} else {iswalking == false}
        if (newState == PlayerState.Walking)
        {
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("isWalkingUp", true);
            }
            if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("isWalkingDown", true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                animator.SetBool("isWalkingRight", true);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool("isWalkingLeft", true);
            }
        }
       
        animator.SetBool("isBlocking", newState == PlayerState.Blocking); //Set isBlocking to true in the animator

        if (newState == PlayerState.BasicPunch)
        {
            animator.SetTrigger("isBasicPunch"); //Triggers the punch animation based on the trigger that was made in the animator
        }
        if (newState == PlayerState.ComboPunch)
        {
            animator.SetTrigger("isComboPunch"); //Triggers the punch animation based on the trigger that was made in the animator
        }
        if (newState == PlayerState.BasicKick)
        {
            animator.SetTrigger("isBasicKick"); //Triggers the punch animation based on the trigger that was made in the animator
        }
        if (newState == PlayerState.Jump)
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
    //                           Player Attacks                             //   
    //======================================================================//
    IEnumerator BasicPunchAttack()
    {
        Debug.Log("Player Has Punched");

        float timer = .8f;

        Punch1.SetActive(true);
        currentlyAttacking = true;

        yield return null;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && currentlyAttacking == true)
            {
                Punch1.SetActive(false);
                ChangeState(PlayerState.ComboPunch);  
                yield break;
            }
            yield return null;
        }
        Punch1.SetActive(false);
        currentlyAttacking = false;
        ChangeState(PlayerState.Idle);
    }
    IEnumerator ComboPunchAttack1()
    {
        Debug.Log("Player Has Comboed");

        float timer = 1f;

        Punch2.SetActive(true);

        yield return null;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && currentlyAttacking == true)
            {
                Punch2.SetActive(false);
                ChangeState(PlayerState.BasicKick);
                yield break;
            }
            yield return null;
        }
        Punch2.SetActive(false);
        currentlyAttacking = false;
        ChangeState(PlayerState.Idle);
    }
    IEnumerator BasicKickAttack()
    {
        Debug.Log("Player Has Kicked");

        float timer = 1.2f;

        Kick1.SetActive(true);

        yield return null;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && currentlyAttacking == true)
            {
                Kick1.SetActive(false);
                //ChangeState(PlayerState.X);
                yield break;
            }
            yield return null;
        }
        Kick1.SetActive(false);
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
            Debug.Log("Player Took 25 Damage");
            HP.TakeDamage(HP.damage);
        }
        else if (isBlocking == true)
        {
            Debug.Log("Player Blocked Incoming Damage");
            HP.damage = 0;
        }
    }

}
