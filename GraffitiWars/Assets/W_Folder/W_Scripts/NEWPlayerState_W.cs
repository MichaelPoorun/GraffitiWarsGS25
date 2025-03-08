using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState //Where all player states are kept
{
    Idle,
    Walking,
    BasicPunch,//Combo 1
    ComboPunch1,//Combo 1
    ComboKick1,//Combo 1
    BasicKick,//Combo 2
    ComboKick2,//Combo 2
    ComboPunch2,//Combo 2
    Jump,
    Blocking
}

public class NEWPlayerState_W : MonoBehaviour
{
    [Header("Player Variables")]
    public float speed; //Player Speed
    public float jumpForce;
    public float gravityScale;
    private Rigidbody rb;

    [Header("Player Bools")]
    public bool combo1 = false;
    public bool combo2 = false;
    public bool isJumping = false;
    public bool isBlocking = false;

    [Header("Player Attack Hitboxes")]
    public GameObject BasicPunch;
    public GameObject ComboPunch1;
    public GameObject ComboKick1;
    public GameObject BasicKick;
    public GameObject ComboKick2;
    public GameObject ComboPunch2;

    [Header("References")]
    public HealthSystem HP;

    [Header("Misc")]
    
    public Animator animator;
    public PlayerState currentState; //Variable that stores current state

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        BasicPunch.SetActive(false);
        ComboPunch1.SetActive(false);
        ComboKick1.SetActive(false);
        BasicKick.SetActive(false);
        ComboKick2.SetActive(false);
        ComboPunch2.SetActive(false);
    }
    void Start()
    { 
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
                if (Input.GetMouseButtonDown(0) && combo1 == false && combo2 == false)
                {
                    ChangeState(PlayerState.BasicPunch);
                }
                if (Input.GetKeyDown(KeyCode.E) && combo2 == false && combo1 == false)
                {
                    ChangeState(PlayerState.BasicKick);
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
                if (Input.GetMouseButtonDown(0) && combo1 == false && combo2 == false)
                {
                    ChangeState(PlayerState.BasicPunch);
                }
                if (Input.GetKeyDown(KeyCode.E) && combo2 == false && combo1 == false)
                {
                    ChangeState(PlayerState.BasicKick);
                }
                if (Input.GetMouseButton(1))
                {
                    ChangeState(PlayerState.Blocking);
                }
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

        switch (currentState)
        {
            case PlayerState.Jump:
                StartCoroutine(Jumping());
                break;

            case PlayerState.BasicPunch:
                Debug.Log("In BasicPunch State");
                StartCoroutine(BasicPunchAttack());
                break;

            case PlayerState.ComboPunch1:
                Debug.Log("In ComboPunch State");
                StartCoroutine(ComboPunchAttack1());
                break;

            case PlayerState.ComboKick1:
                Debug.Log("In Kick State");
                StartCoroutine(ComboKickAttack1());
                break;

            case PlayerState.BasicKick:
                Debug.Log("In Kick State");
                StartCoroutine(BasicKickAttack());
                break;

            case PlayerState.ComboKick2:
                Debug.Log("In Kick State");
                StartCoroutine(ComboKickAttack2());
                break;

            case PlayerState.ComboPunch2:
                Debug.Log("In Kick State");
                StartCoroutine(ComboPunchAttack2());
                break;
        }

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
        if (newState == PlayerState.ComboPunch1)
        {
            animator.SetTrigger("isComboPunch1"); //Triggers the punch animation based on the trigger that was made in the animator
        }
        if (newState == PlayerState.ComboKick1)
        {
            animator.SetTrigger("isComboKick1"); //Triggers the punch animation based on the trigger that was made in the animator
        }
        if (newState == PlayerState.BasicKick)
        {
            animator.SetTrigger("isBasicKick"); //Triggers the punch animation based on the trigger that was made in the animator
        }
        if (newState == PlayerState.ComboKick2)
        {
            animator.SetTrigger("isComboKick2"); //Triggers the punch animation based on the trigger that was made in the animator
        }
        if (newState == PlayerState.ComboPunch2)
        {
            animator.SetTrigger("isComboPunch2"); //Triggers the punch animation based on the trigger that was made in the animator
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
    //COMBO 1//
    IEnumerator BasicPunchAttack()
    {
        Debug.Log("Player Has Punched");

        float timer = .75f;

        BasicPunch.SetActive(true);
        combo1 = true;

        yield return null;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && combo1 == true)
            {
                BasicPunch.SetActive(false);
                ChangeState(PlayerState.ComboPunch1);  
                yield break;
            }
            yield return null;
        }
        BasicPunch.SetActive(false);
        combo1 = false;
        ChangeState(PlayerState.Idle);
    }
    IEnumerator ComboPunchAttack1()
    {
        Debug.Log("Player Has Comboed");

        float timer = 1.15f;

        ComboPunch1.SetActive(true);

        yield return null;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.E) && combo1 == true)
            {
                ComboPunch1.SetActive(false);
                ChangeState(PlayerState.ComboKick1);
                yield break;
            }
            yield return null;
        }
        ComboPunch1.SetActive(false);
        combo1 = false;
        ChangeState(PlayerState.Idle);
    }
    IEnumerator ComboKickAttack1()
    {
        Debug.Log("Player Has Kicked");

        ComboKick1.SetActive(true);

        yield return new WaitForSeconds(1f);

        ComboKick1.SetActive(false);
        combo1 = false;
        ChangeState(PlayerState.Idle);
    }
    //COMBO 1//
    //-------//
    //COMBO 2//
    IEnumerator BasicKickAttack()
    {
        float timer = 1f;

       BasicKick.SetActive(true);
        combo2 = true;

        yield return null;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.E) && combo2 == true)
            {
                BasicKick.SetActive(false);
                ChangeState(PlayerState.ComboKick2);
                yield break;
            }
            yield return null;
        }
        BasicKick.SetActive(false);
        combo2 = false;
        ChangeState(PlayerState.Idle);
    }
    IEnumerator ComboKickAttack2()
    {
        float timer = 1f;

        ComboKick2.SetActive(true);
        combo2 = true;

        yield return null;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && combo2 == true)
            {
                ComboKick2.SetActive(false);
                ChangeState(PlayerState.ComboPunch2);
                yield break;
            }
            yield return null;
        }
        ComboKick2.SetActive(false);
        combo2 = false;
        ChangeState(PlayerState.Idle);
    }
    IEnumerator ComboPunchAttack2()
    {
        ComboPunch2.SetActive(true);
        combo2 = true;

        yield return new WaitForSeconds(1f);

        ComboPunch2.SetActive(false);
        combo2 = false;
        ChangeState(PlayerState.Idle);
    }
    //COMBO 2//
    //-------//
    //COMBO 3//


    //======================================================================//
    //                             Player Jump                              //   
    //======================================================================//
    IEnumerator Jumping()
    {
        yield return new WaitForSeconds(.5f);
        if (!isJumping)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, Mathf.Sqrt(jumpForce * -2 * Physics.gravity.y * gravityScale), rb.linearVelocity.z);
            isJumping = true;
        }
    }
    void OnCollisionEnter(Collision other)
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
