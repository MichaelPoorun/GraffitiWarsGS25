using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public enum EnemyState //Where all player states are kept
{
    Sitting,
    SitToStand,
    Roar,
    Stomp,
    Cooldown
}

public class EnemyStatemachine_W : MonoBehaviour
{
    [Header("Player Variables")]
    private Rigidbody rb;
    public int damage;
    public NEWPlayerState_W target;
    public NEWPlayerState_W player;
    public GameObject Chair;
    public GameObject objectToThrow;
    public Transform cam;
    public Transform attackPoint;
    public float throwForce;
    public float throwUpwardForce;

    [Header("Player Bools")]
    public bool timeToLook = false;

    [Header("Player Attack Hitboxes")]

    [Header("References")]
    public HealthSystem HP;

    [Header("Misc")]
    public Animator animator;
    public EnemyState currentState; //Variable that stores current state

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindFirstObjectByType<NEWPlayerState_W>();
    }
    void Update()
    {
        HandleState();
        if (timeToLook == true)
        {
            LookAtTarget();
        }
    }

    //======================================================================//
    //                            State Machine                             //   
    //======================================================================//
    void HandleState()
    {
        switch (currentState)
        {
            case EnemyState.Sitting:
                if (player.BossTime == true)
                {
                    ChangeState(EnemyState.SitToStand);
                }
                break;
        }
    }
    public void ChangeState(EnemyState newState)
    {
        if (currentState == newState) return;

        currentState = newState;

        switch (currentState)
        {
            case EnemyState.SitToStand:
                animator.SetBool("GoingToStand", true);
                break;

            case EnemyState.Roar:
                Chair.SetActive(false);
                animator.SetBool("isRoar", true);
                break;

            case EnemyState.Stomp:
                timeToLook = true;
                animator.SetBool("isStomp", true);
                animator.SetBool("isCooldown", false);
                break;

            case EnemyState.Cooldown:
                animator.SetBool("isStomp", false);
                animator.SetBool("isCooldown", true);
                break;
        }

        // animator.SetBool("isWalkingUp", newState == PlayerState.X); - Sets the Bool makes the animator bool T or F. Same as doing if isWalking == true {play animation} else {iswalking == false}
    }

    //======================================================================//
    //                                Misc                                  //   
    //======================================================================//
    void BackToCooldown(EnemyState s)
    {
        if (currentState != s && s != EnemyState.Cooldown)
        {
            Debug.Log("CS: " + currentState + " / " + s);
            return;
        }
        animator.SetBool("isBasicPunch", false);
        ChangeState(EnemyState.Cooldown);

    }
    private void LookAtTarget()
    {
        Vector3 lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    void ThrowObj()
    {
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        ProjectileMover_W mover = projectile.AddComponent<ProjectileMover_W>(); // Attach movement script
        mover.SetSpeed(throwForce);
    }

    //======================================================================//
    //                             Enemy Health                             //   
    //======================================================================//
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hit_Enemy"))
        {
            Debug.Log("Enemy Took Damage");
            damage = 20;
            HP.TakeDamage(damage);
        }
        if (other.gameObject.CompareTag("Ability"))
        {
            Debug.Log("Enemy Took Damage");
            damage = 60;
            HP.TakeDamage(damage);
        }
    }
}
