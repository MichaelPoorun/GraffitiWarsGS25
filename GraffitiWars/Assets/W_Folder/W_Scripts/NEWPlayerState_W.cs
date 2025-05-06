using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
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
    public float sprayTimer;
    public float sprayCooldown;
    public float throwTimer;
    public float throwCooldown;
    private Rigidbody rb;
    AbilitiesTimer_W TimerOn1;
    public GameObject objectToThrow;
    public Transform cam;
    public Transform attackPoint;
    public float throwForce;
    public float throwUpwardForce;
    ThrowTimer_W TimerOn2;

    [Header("Player Bools")]
    public bool combo1 = false;
    public bool combo2 = false;
    public bool combo3 = false;
    public bool isJumping = false;
    public bool isBlocking = false;
    public bool canSpray = true;
    public bool canThrow = true;
    public bool BossTime = false;

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

        TimerOn1 = GetComponent<AbilitiesTimer_W>();
        TimerOn1.enabled = false;
        TimerOn1.TimerTxt.enabled = false;

        TimerOn2 = GetComponent<ThrowTimer_W>();
        TimerOn2.enabled = false;
        TimerOn2.throwTimerTxt.enabled = false;

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

        BossTime = false;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        sprayTimer = sprayCooldown;
        throwTimer = throwCooldown;
    }
    void Update()
    {
        string[] joystickNames = Input.GetJoystickNames();
        foreach (var joystick in joystickNames)
        {
            Debug.Log("Joystick: " + joystick);
        }

        HandleState();

        if (currentState == PlayerState.Walking)
        {
            PlayerMovement();
        }

        if (HP.currentHealth <= 25f)
        {
            SceneManager.LoadScene(3);
        }

        if (!canSpray)
        {
            sprayTimer -= Time.deltaTime;
            TimerOn1.TimeLeft = sprayTimer;

            if (sprayTimer <= 0)
            {
                canSpray = true;
                TimerOn1.enabled = false;
                TimerOn1.TimerTxt.enabled = false;
            }
        }

        if (!canThrow)
        {
            throwTimer -= Time.deltaTime;
            TimerOn2.TimeLeft = throwTimer;

            if (throwTimer <= 0)
            {
                canThrow = true;
                TimerOn2.enabled = false;
                TimerOn2.throwTimerTxt.enabled = false;
            }
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
                float z = Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("Vertical_Controller");
                float x = Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Horizontal_Controller");

                if (Mathf.Abs(z) > 0.1f || Mathf.Abs(x) > 0.1f)
                {
                    ChangeState(PlayerState.Walking);
                }
                if (Input.GetButtonDown("Jump") && isJumping == false)
                {
                    ChangeState(PlayerState.Jump1);
                }
                if (Input.GetButtonDown("Punch") /*&& combo1 == false && combo2 == false && combo3 == false*/)
                {
                    ChangeState(PlayerState.BasicPunch);
                }
                if (Input.GetButtonDown("Kick") /*&& combo1 == false && combo2 == false && combo3 == false*/)
                {
                    ChangeState(PlayerState.BasicKick);
                }
                if (Input.GetButton("Block"))
                {
                    ChangeState(PlayerState.Blocking);
                }
                if (Input.GetButtonDown("Spray") && canSpray)
                {
                    ChangeState(PlayerState.Spray);
                    canSpray = false;
                    sprayTimer = sprayCooldown;
                    TimerOn1.enabled = true;
                    TimerOn1.TimerTxt.enabled = true;
                }
                if (Input.GetButtonDown("Throw") && canThrow)
                {
                    ChangeState(PlayerState.Throw);
                    canThrow = false;
                    throwTimer = throwCooldown;
                    TimerOn2.enabled = true;
                    TimerOn2.throwTimerTxt.enabled = true;

                }
                break;

            case PlayerState.Walking:
                float c = Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("Vertical_Controller");
                float v = Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Horizontal_Controller");

                if (Mathf.Abs(c) < 0.1f && Mathf.Abs(v) < 0.1f)
                {
                    ChangeState(PlayerState.Idle);
                }
                if (Input.GetButtonDown("Jump") && isJumping == false)
                {
                    ChangeState(PlayerState.Jump1);
                }
                if (Input.GetButtonDown("Punch") /*&& combo1 == false && combo2 == false && combo3 == false*/)
                {
                    ChangeState(PlayerState.BasicPunch);
                }
                if (Input.GetButtonDown("Kick") /*&& combo2 == false && combo1 == false && combo3 == false*/)
                {
                    ChangeState(PlayerState.BasicKick);
                }
                if (Input.GetButton("Block"))
                {
                    ChangeState(PlayerState.Blocking);
                }
                if (Input.GetButtonDown("Spray") && canSpray)
                {
                    ChangeState(PlayerState.Spray);
                    canSpray = false;
                    sprayTimer = sprayCooldown;
                    TimerOn1.enabled = true;
                    TimerOn1.TimerTxt.enabled = true;
                }
                if (Input.GetButtonDown("Throw") && canThrow)
                {
                    ChangeState(PlayerState.Throw);
                    canThrow = false;
                    throwTimer = throwCooldown;
                    TimerOn2.enabled = true;
                    TimerOn2.throwTimerTxt.enabled = true;
                }
                break;

            case PlayerState.Blocking:
                Blocking();
                if (Input.GetButtonUp("Block"))
                {
                    isBlocking = false;
                    ChangeState(PlayerState.Idle);
                }
                break;

            case PlayerState.BasicPunchB:
                {
                    if (Input.GetButtonDown("Punch"))
                    {
                        animator.SetBool("isBasicPunch", false);
                        ChangeState(PlayerState.ComboPunch1);
                    }
                    else if (Input.GetButton("Kick"))
                    {
                        animator.SetBool("isBasicPunch", false);
                        ChangeState(PlayerState.ComboKick3);
                    }
                    break;
                }

            case PlayerState.ComboPunch1B:
                {
                    if (Input.GetButtonDown("Kick"))
                    {
                        animator.SetBool("isComboPunch1", false);
                        ChangeState(PlayerState.ComboKick1);
                    }
                    break;
                }

            case PlayerState.BasicKickB:
                {
                    if (Input.GetButtonDown("Kick"))
                    {
                        animator.SetBool("isBasicKick", false);
                        ChangeState(PlayerState.ComboKick2);
                    }
                    break;
                }

            case PlayerState.ComboKick2B:
                {
                    if (Input.GetButtonDown("Punch"))
                    {
                        animator.SetBool("isComboKick2", false);
                        ChangeState(PlayerState.ComboPunch2);
                    }
                    break;
                }

            case PlayerState.ComboKick3B:
                {
                    if (Input.GetButtonDown("Punch"))
                    {
                        animator.SetBool("isComboKick3", false);
                        ChangeState(PlayerState.ComboPunch3);
                    }
                    break;
                }

            case PlayerState.ComboPunch3B:
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        animator.SetBool("isComboPunch3", false);
                        ChangeState(PlayerState.Jump2Combo3);
                    }
                    break;
                }

            case PlayerState.Jump2Combo3B:
                {
                    if (Input.GetButtonDown("Kick"))
                    {
                        animator.SetBool("isJump2Combo3", false);
                        ChangeState(PlayerState.JumpKick2);
                    }
                    break;
                }

            case PlayerState.Jump1B:
                {
                    if (Input.GetButtonDown("Kick"))
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
        if (currentState == newState) return;

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

            case PlayerState.Spray:
                animator.Play("Spray_T");
                break;

            case PlayerState.Throw:
                Debug.Log("Step2 - Animation Plays");
                animator.Play("Throw_T");
                break;
        }

        animator.SetBool("isWalkingUp", false);
        animator.SetBool("isWalkingLeft", false);
        animator.SetBool("isWalkingDown", false);
        animator.SetBool("isWalkingRight", false);
        // animator.SetBool("isWalkingUp", newState == PlayerState.X); - Sets the Bool makes the animator bool T or F. Same as doing if isWalking == true {play animation} else {iswalking == false}
        if (newState == PlayerState.Walking)
        {
            float z = Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("Vertical_Controller");
            float x = Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Horizontal_Controller");

            if (z > 0.1f)
            {
                animator.SetBool("isWalkingRight", true);
            }
            else if (z < -0.1f)
            {
                animator.SetBool("isWalkingRight", true);
            }

            if (x < -0.1f)
            {
                animator.SetBool("isWalkingRight", true);
            }
            else if (x > 0.1f)
            {
                animator.SetBool("isWalkingRight", true);
            }
        }

        animator.SetBool("isBlocking", newState == PlayerState.Blocking); //Set isBlocking to true in the animator
    }

    //======================================================================//
    //                            Player Movement                           //   
    //======================================================================//
    public void PlayerMovement()
    {
        float z = Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("Vertical_Controller");
        float x = Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Horizontal_Controller");

        Vector3 moveDirection = new Vector3(-z, 0, x).normalized;

        if (moveDirection.magnitude > 0)
        {
            transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    //======================================================================//
    //                           Player Combos                              //   
    //======================================================================//
    void BackToIdle(PlayerState s)
    {
        if (currentState != s && s != PlayerState.Idle)
        {
            /*Debug.Log("CS: " + currentState + " / " + s);*/
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
    //          COMBO 3          //
    //---------------------------//
    //          Ablilites        //
    public void updateTimer(float currentTime)
    {
        float seconds = Mathf.FloorToInt(currentTime % 60);
        TimerOn1.TimerTxt.text = string.Format("{00}, seconds");
        TimerOn2.throwTimerTxt.text = string.Format("{00}, seconds");
    }
    void SprayBoxOn()
    {
        Spray.SetActive(true);
    }
    IEnumerator SprayCooldown()
    {
        while (sprayTimer > 0)
        {
            sprayTimer -= Time.deltaTime;
            yield return null;
        }
        canSpray = true;
        TimerOn1.sprayColor.color = Color.white;
    }
    void SprayBoxOff()
    {
        Spray.SetActive(false);
    }
    void ThrowBoxOn()
    {
        Throw.SetActive(true);
    }
    void ThrowObj()
    {
        GameObject projecile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);
        Rigidbody projectileRB = projecile.GetComponent<Rigidbody>();
        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;
        projectileRB.AddForce(forceToAdd, ForceMode.Impulse);
    }
    IEnumerator ThrowCooldown()
    {
        while (throwTimer > 0)
        {
            throwTimer -= Time.deltaTime;
            yield return null;
        }
        canThrow = true;
        TimerOn2.throwColor.color = Color.white;
    }
    void ThrowBoxOff()
    {
        Throw.SetActive(false);
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
            damage = 20;
            HP.TakeDamage(damage);
        }
        else if (other.gameObject.CompareTag("TankEnemy") && isBlocking == false)
        {
            damage = 10;
            HP.TakeDamage(damage);
        }
        else if (other.gameObject.CompareTag("MouseEnemy") && isBlocking == false)
        {
            damage = 5;
            HP.TakeDamage(damage);
        }
        else if (other.gameObject.CompareTag("BossEnemy") && isBlocking == false)
        {
            damage = 50;
            HP.TakeDamage(damage);
        }
        else if (isBlocking == true)
        {
            damage = 0;
        }

        if (other.gameObject.CompareTag("Boss"))
        {
            BossTime = true;
        }

        if (other.gameObject.CompareTag("TheEnd"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
