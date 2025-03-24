using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState //Where all player states are kept
{
    Idle,
    Walking,
    BasicPunch,//Start Of Combo 1 & 3
    BasicPunchB,//Combo 1
    ComboPunch1,//Combo 1
    ComboPunch1B,//Combo 1
    ComboKick1,//End Of Combo 1
    BasicKick,//Start Of Combo 2
    BasicKickB,//Combo 2
    ComboKick2,//Combo 2
    ComboKick2B,//Combo 2
    ComboPunch2,//End Of Combo 2
    ComboKick3,//Combo 3
    ComboKick3B,//Combo 3
    ComboPunch3,//Combo 3
    ComboPunch3B,//Combo 3
    Jump2Combo3,//Combo 3
    Jump2Combo3B,//Combo 3
    JumpKick2,//End Of Combo 3
    Jump1,
    Jump1B,
    JumpKick1,
    Blocking,
    Spray,
    Throw
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
    public bool ability1 = false;
    public bool ability2 = false;

    [Header("Player Attack Hitboxes")]
    public GameObject BasicPunch;
    public GameObject ComboPunch1;
    public GameObject ComboKick1;
    public GameObject BasicKick;
    public GameObject ComboKick2;
    public GameObject ComboPunch2;
    public GameObject ComboKick3;
    public GameObject ComboPunch3;
    public GameObject JumpKick2;
    public GameObject JumpKick1;
    public GameObject Spray;
    public GameObject Throw;

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
        ComboKick3.SetActive(false);
        ComboPunch3.SetActive(false);
        JumpKick2.SetActive(false);
        JumpKick1.SetActive(false);
        Spray.SetActive(false);
        Throw.SetActive(false);
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
                    ChangeState(PlayerState.Jump1);
                }
                if (Input.GetMouseButtonDown(0) /*&& combo1 == false && combo2 == false && combo3 == false*/)
                {
                    ChangeState(PlayerState.BasicPunch);
                }
                if (Input.GetKeyDown(KeyCode.E) /*&& combo1 == false && combo2 == false && combo3 == false*/)
                {
                    ChangeState(PlayerState.BasicKick);
                }
                if (Input.GetMouseButton(1))
                {
                    ChangeState(PlayerState.Blocking);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {

                }
                if (Input.GetKeyDown(KeyCode.G))
                {

                }
                break;

            case PlayerState.Walking:
                if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
                {
                    ChangeState(PlayerState.Idle);
                }
                if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
                {
                    ChangeState(PlayerState.Jump1);
                }
                if (Input.GetMouseButtonDown(0) /*&& combo1 == false && combo2 == false && combo3 == false*/)
                {
                    ChangeState(PlayerState.BasicPunch);
                }
                if (Input.GetKeyDown(KeyCode.E) /*&& combo2 == false && combo1 == false && combo3 == false*/)
                {
                    ChangeState(PlayerState.BasicKick);
                }
                if (Input.GetMouseButton(1))
                {
                    ChangeState(PlayerState.Blocking);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ChangeState(PlayerState.Spray);
                }
                if (Input.GetKeyDown(KeyCode.G))
                {
                    ChangeState(PlayerState.Throw);
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

            case PlayerState.BasicPunchB:
            {
                if (Input.GetMouseButtonDown(0))
                {
                    animator.SetBool("isBasicPunch", false);
                    ChangeState(PlayerState.ComboPunch1);
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    animator.SetBool("isBasicPunch", false);
                    ChangeState(PlayerState.ComboKick3);
                }
                break;
            }

            case PlayerState.ComboPunch1B:
            {
               if (Input.GetKeyDown(KeyCode.E))
               {
                    animator.SetBool("isComboPunch1", false);
                    ChangeState(PlayerState.ComboKick1);
               }
               break;
            }

            case PlayerState.BasicKickB:
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    animator.SetBool("isBasicKick", false);
                    ChangeState(PlayerState.ComboKick2);
                }
                break;
            }

            case PlayerState.ComboKick2B:
            {
                if (Input.GetMouseButtonDown(0))
                {
                    animator.SetBool("isComboKick2", false);
                    ChangeState(PlayerState.ComboPunch2);
                }
                break;
            }
            
            case PlayerState.ComboKick3B:
            {
                if (Input.GetMouseButtonDown(0))
                {
                    animator.SetBool("isComboKick3", false);
                    ChangeState(PlayerState.ComboPunch3);
                }
                break;
            }

            case PlayerState.ComboPunch3B:
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    animator.SetBool("isComboPunch3", false);
                    ChangeState(PlayerState.Jump2Combo3);
                }
                break;
            }

            case PlayerState.Jump2Combo3B:
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    animator.SetBool("isJump2Combo3", false);
                    ChangeState(PlayerState.JumpKick2);
                }
                break;
            }

            case PlayerState.Jump1B:
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    animator.SetBool("isJump1", false);
                    ChangeState(PlayerState.JumpKick1);
                }
                break;
            }
        }
    }
    public void ChangeState(PlayerState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case PlayerState.BasicPunch:
                animator.SetBool("isBasicPunch", true);
                break;

            case PlayerState.ComboPunch1:
                animator.SetBool("isComboPunch1", true);
                break;

            case PlayerState.ComboKick1:
                animator.SetBool("isComboKick1", true);
                break;

            case PlayerState.BasicKick:
                animator.SetBool("isBasicKick", true);
                break;

            case PlayerState.ComboKick2:
                animator.SetBool("isComboKick2", true);
                break;

            case PlayerState.ComboPunch2:
                animator.SetBool("isComboPunch2", true);
                break;

            case PlayerState.ComboKick3:
                animator.SetBool("isComboKick3", true);
                break;

            case PlayerState.ComboPunch3:
                animator.SetBool("isComboPunch3", true);
                break;

            case PlayerState.Jump2Combo3:
                animator.SetBool("isJump2Combo3", true);
                break;

            case PlayerState.JumpKick2:
                animator.SetBool("isJumpKick2", true);
                break;

            case PlayerState.Jump1:
                animator.SetBool("isJump1", true);
                break;

            case PlayerState.JumpKick1:
                animator.SetBool("isJumpKick1", true);
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
    //                           Player Combos                              //   
    //======================================================================//
    void BackToIdle(PlayerState s)
    {
        if(currentState != s && s != PlayerState.Idle)
        {
            Debug.Log("CS: " + currentState + " / " + s);
            return;
        }
        animator.SetBool("isBasicPunch", false);
        animator.SetBool("isComboPunch1", false);
        animator.SetBool("isComboKick1", false);
        animator.SetBool("isBasicKick", false);
        animator.SetBool("isComboKick2", false);
        animator.SetBool("isComboPunch2", false);
        animator.SetBool("isComboKick3", false);
        animator.SetBool("isComboPunch3", false);
        animator.SetBool("isJump2Combo3", false);
        animator.SetBool("isJumpKick2", false);
        animator.SetBool("isJumpKick1", false);
        animator.SetBool("isJump1", false);
        isJumping = false;
        ChangeState(PlayerState.Idle);
        
    }

    //COMBO 1 & Start Of COMBO 3//
    void BasicPunchBoxOn()
    {
        BasicPunch.SetActive(true);
    }
    void BasicPunchBoxOff()
    {
        BasicPunch.SetActive(false);
    }
    void ComboPunch1BoxOn()
    {
        ComboPunch1.SetActive(true);
    }
    void ComboPunch1BoxOff()
    {
        ComboPunch1.SetActive(false);
    }
    void ComboKick1BoxOn()
    {
        ComboKick1.SetActive(true);
    }
    void ComboKick1BoxOff()
    {
        ComboKick1.SetActive(false);
    }
    //COMBO 1 & Start Of COMBO 3//
    //---------------------------//
    //          COMBO 2          //
    void BasicKickBoxOn()
    {
        BasicKick.SetActive(true);
    }
    void BasicKickBoxOff()
    {
        BasicKick.SetActive(false);
    }
    void ComboKick2BoxOn()
    {
        ComboKick2.SetActive(true);
    }
    void ComboKick2BoxOff()
    {
        ComboKick2.SetActive(false);
    }
    void ComboPunch2BoxOn()
    {
        ComboPunch2.SetActive(true);
    }
    void ComboPunch2BoxOff()
    {
        ComboPunch2.SetActive(false);
    }
    //          COMBO 2          //
    //---------------------------//
    //          COMBO 3          //
    void ComboKick3BoxOn()
    {
        ComboKick3.SetActive(true);
    }
    void ComboKick3BoxOff()
    {
        ComboKick3.SetActive(false);
    }
    void ComboPunch3BoxOn()
    {
        ComboPunch3.SetActive(true);
    }
    void ComboPunch3BoxOff()
    {
        ComboPunch3.SetActive(false);
    }
    void Jumping2Combo3()
    {
        if (!isJumping)
        {
            isJumping = true;

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, Mathf.Sqrt(jumpForce * -2 * Physics.gravity.y * gravityScale), rb.linearVelocity.z);
        }
    }
    void JumpKick2BoxOn()
    {
        JumpKick2.SetActive(true);
    }
    void JumpKick2BoxOff()
    {
        JumpKick2.SetActive(false);
    }


    //======================================================================//
    //                             Player Jump                              //   
    //======================================================================//
    void Jumping()
    {
        if (!isJumping)
        {
            isJumping = true;

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, Mathf.Sqrt(jumpForce * -2 * Physics.gravity.y * gravityScale), rb.linearVelocity.z);
        }
    }
    void JumpKick1BoxOn()
    {
        JumpKick1.SetActive(true);
    }
    void JumpKick1BoxOff()
    {
        JumpKick1.SetActive(false);
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
