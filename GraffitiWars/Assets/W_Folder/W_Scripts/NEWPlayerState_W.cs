using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    JumpKick1,
    Blocking
}

public class NEWPlayerState_W : MonoBehaviour
{
    [Header("Player Variables")]
    public float speed; //Player Speed
    public float jumpForce;
    public float gravityScale;
    public int damage;
    private Rigidbody rb;

    [Header("Player Bools")]
    public bool combo1 = false;
    public bool combo2 = false;
    public bool combo3 = false;
    public bool isJumping = false;
    public bool isBlocking = false;

    [Header("Player Attack Hitboxes")]
    public GameObject BasicPunch;
    public GameObject ComboPunch1;
    public GameObject ComboKick1;
    public GameObject BasicKick;
    public GameObject ComboKick2;
    public GameObject ComboPunch2;
    public GameObject JumpKick1;

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
        JumpKick1.SetActive(false);
    }
    void Start()
    { 
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        HandleState();

        if (currentState == PlayerState.Walking)
        {
            PlayerMovement();
        }

        if (HP.currentHealth <= 25f)
        {
            SceneManager.LoadScene(3);
        }
        
    }

    //======================================================================//
    //                            State Machine                             //   
    //======================================================================//
    void HandleState()
    {
        switch (currentState)
        {
            case PlayerState.Idle:
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    ChangeState(PlayerState.Walking);
                }
                if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
                {
                    ChangeState(PlayerState.Jump);
                }
                if (Input.GetMouseButtonDown(0) && combo1 == false && combo2 == false && combo3 == false)
                {
                    ChangeState(PlayerState.BasicPunch);
                }
                if (Input.GetKeyDown(KeyCode.E) && combo2 == false && combo1 == false && combo3 == false)
                {
                    ChangeState(PlayerState.BasicKick);
                }
                if (Input.GetMouseButton(1))
                {
                    ChangeState(PlayerState.Blocking);
                }
                break;

            case PlayerState.Walking:
                if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
                {
                    ChangeState(PlayerState.Idle);
                }
                if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
                {
                    ChangeState(PlayerState.Jump);
                }
                if (Input.GetMouseButtonDown(0) && combo1 == false && combo2 == false && combo3 == false)
                {
                    ChangeState(PlayerState.BasicPunch);
                }
                if (Input.GetKeyDown(KeyCode.E) && combo2 == false && combo1 == false && combo3 == false)
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
            case PlayerState.BasicPunch:
                StartCoroutine(BasicPunchAttack());
                break;

            case PlayerState.ComboPunch1:
                StartCoroutine(ComboPunchAttack1());
                break;

            case PlayerState.ComboKick1:
                StartCoroutine(ComboKickAttack1());
                break;

            case PlayerState.BasicKick:
                StartCoroutine(BasicKickAttack());
                break;

            case PlayerState.ComboKick2:
                StartCoroutine(ComboKickAttack2());
                break;

            case PlayerState.ComboPunch2:
                StartCoroutine(ComboPunchAttack2());
                break;
            
            case PlayerState.Jump:
                StartCoroutine(Jumping());
                break;

            case PlayerState.JumpKick1:
                StartCoroutine(JumpKickAttack1());
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
            animator.SetTrigger("isComboPunch1"); 
        }
        if (newState == PlayerState.ComboKick1)
        {
            animator.SetTrigger("isComboKick1"); 
        }
        if (newState == PlayerState.BasicKick)
        {
            animator.SetTrigger("isBasicKick"); 
        }
        if (newState == PlayerState.ComboKick2)
        {
            animator.SetTrigger("isComboKick2"); 
        }
        if (newState == PlayerState.ComboPunch2)
        {
            animator.SetTrigger("isComboPunch2"); 
        }
        if (newState == PlayerState.Jump)
        {
            animator.SetTrigger("isJumping"); //Triggers the jump animation based on the trigger that was made in the animator
        }
        if (newState == PlayerState.JumpKick1)
        {
            animator.SetTrigger("isJumpKick1"); 
        }
    }

    //======================================================================//
    //                            Player Movement                           //   
    //======================================================================//
    public void PlayerMovement()
    {
        float z = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3(x * speed, 0, z * speed) * Time.deltaTime);
     
    }

    //======================================================================//
    //                           Player Attacks                             //   
    //======================================================================//
    //COMBO 1 & Start Of COMBO 3//
    IEnumerator BasicPunchAttack()
    {
        float timer = 1f;

        BasicPunch.SetActive(true);

        yield return null;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
            {
                combo1 = true;
                BasicPunch.SetActive(false);
                ChangeState(PlayerState.ComboPunch1);  
                yield break;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                combo3 = true;
                BasicPunch.SetActive(false);
                ChangeState(PlayerState.BasicKick);
                yield break;
            }

            yield return null;
        }
        
        BasicPunch.SetActive(false);
        ChangeState(PlayerState.Idle);
    }
    IEnumerator ComboPunchAttack1()
    {
        float timer = 1f;

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
            else if (Input.GetKey(KeyCode.Space) && combo3 == true)
            {
                ComboPunch1.SetActive(false);
                ChangeState(PlayerState.Jump);
                yield break;
            }

            yield return null;
        }
        ComboPunch1.SetActive(false);
        combo1 = false;
        combo3 = false;
        ChangeState(PlayerState.Idle);
    }
    IEnumerator ComboKickAttack1()
    {
        ComboKick1.SetActive(true);

        yield return new WaitForSeconds(1f);

        ComboKick1.SetActive(false);
        combo1 = false;
        ChangeState(PlayerState.Idle);
    }
    //COMBO 1 & Start Of COMBO 3//
    //---------------------------//
    //COMBO 2 & Middle Of COMBO 3//
    IEnumerator BasicKickAttack()
    {
        float timer = 1f;

        BasicKick.SetActive(true);
        combo2 = true;

        yield return null;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.E))
            {
                combo2 = true;
                BasicKick.SetActive(false);
                ChangeState(PlayerState.ComboKick2);
                yield break;
            }
            else if (Input.GetMouseButtonDown(0) && combo3 == true)
            {
                BasicKick.SetActive(false);
                ChangeState(PlayerState.ComboPunch1);
            }
            yield return null;
        }
        BasicKick.SetActive(false);
        combo2 = false;
        combo3 = false;
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
    //COMBO 2 & Middle Of COMBO 3//
    //---------------------------//
    //          COMBO 3          //


    //======================================================================//
    //                             Player Jump                              //   
    //======================================================================//
    IEnumerator Jumping()
    {
        yield return new WaitForSeconds(.48f);
        if (!isJumping)
        {
            float timer = 1f;
            isJumping = true;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, Mathf.Sqrt(jumpForce * -2 * Physics.gravity.y * gravityScale), rb.linearVelocity.z);

            yield return null;

            while (timer > 0)
            {
                timer -= Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ChangeState(PlayerState.JumpKick1);
                    yield break;
                }
                yield return null;
            }

            combo3 = false;
        }
    }
    IEnumerator JumpKickAttack1()
    {
        JumpKick1.SetActive(true);

        yield return new WaitForSeconds(1f);

        combo3 = false;
        JumpKick1.SetActive(false);
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
        if (other.gameObject.CompareTag("NormalEnemy") && isBlocking == false)
        {
            Debug.Log("Player Took 20 Damage");
            damage = 20;
            HP.TakeDamage(damage);
        }
        else if (other.gameObject.CompareTag("TankEnemy") && isBlocking == false)
        {
            Debug.Log("Player Took 10 Damage");
            damage = 10;
            HP.TakeDamage(damage);
        }
        else if (other.gameObject.CompareTag("MouseEnemy") && isBlocking == false)
        {
            Debug.Log("Player Took 5 Damage");
            damage = 5;
            HP.TakeDamage(damage);
        }
        else if (isBlocking == true)
        {
            Debug.Log("Player Blocked Incoming Damage");
            damage = 0;
        }
    }

}
