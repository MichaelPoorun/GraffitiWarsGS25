using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using System.Collections;

public enum REnemyState //Where all player states are kept
{
    Idle,
    Bite
}

public class BasicEnemyStatemachine_W1 : MonoBehaviour
{ 
    [Header("Player Variables")]
    private Rigidbody rb;
    public int damage;
    public NEWPlayerState_W target;
    public NEWPlayerState_W player;
    private EnemyReferences enemyReferences;

    private float pathUpdateDeadline; //tracks when the path can be updated
    private float bitingDistance;

    public GameObject EBite;

    public float attackCooldown = 2f; // 2 seconds between bites
    private float lastAttackTime = -Mathf.Infinity;

    [Header("Player Bools")]
    private bool inRange;

    [Header("Player Attack Hitboxes")]

    [Header("References")]
    public HealthSystem HP;

    [Header("Misc")]
    public Animator animator;
    public REnemyState currentState; //Variable that stores current state

    void Awake()
    {
        EBite.SetActive(false);
        enemyReferences = GetComponent<EnemyReferences>();
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        bitingDistance = enemyReferences.navMeshAgent.stoppingDistance;
        target = GameObject.FindFirstObjectByType<NEWPlayerState_W>();
        animator = GetComponent<Animator>();
        target = GameObject.FindFirstObjectByType<NEWPlayerState_W>();
    }
    void Update()
    {
        HandleState();
        if (target != null)
        {
            //computes the distance between the enemy and target
            inRange = Vector3.Distance(transform.position, target.transform.position) <= bitingDistance;

            if (inRange && Time.time >= lastAttackTime + attackCooldown)
            {
                LookAtTarget();
                AttackPlayer();
            }
            else
            {
                UpdatePath();
            }
        }
        enemyReferences.anim.SetFloat("Speed", enemyReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
    }

    private void AttackPlayer()
    {
        lastAttackTime = Time.time;
        animator.SetTrigger("Bite"); // Make sure you have a trigger called "Bite" in Animator
    }

    //======================================================================//
    //                            State Machine                             //   
    //======================================================================//
    void HandleState()
    {
        /*switch (currentState)
        {
            case BEnemyState.Idle:
                animator.SetBool("isIdle", true);
                animator.SetBool("isIdle", false);
                if (inRange)
                {
                    ChangeState(BEnemyState.Punch);
                }
                break;
        }*/
    }
    public void ChangeState(REnemyState newState)
    {
        if (currentState == newState) return;

        currentState = newState;

        /*switch (currentState)
        {
            case BEnemyState.Punch:
                animator.SetBool("isIdle", false);
                animator.SetBool("isPunch", true);
            break;
        }*/
    }

    //======================================================================//
    //                                Misc                                  //   
    //======================================================================//
    void BackToIdle(REnemyState s)
    {
        if (currentState != s && s != REnemyState.Idle)
        {
            Debug.Log("CS: " + currentState + " / " + s);
            return;
        }
        //animator.SetBool("punching", false);
        ChangeState(REnemyState.Idle);
    }
    private void LookAtTarget()
    {
        Vector3 lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }
    private void UpdatePath()
    {
        if (Time.time >= pathUpdateDeadline)
        {
            pathUpdateDeadline = Time.time + enemyReferences.pathUpdateDelay;
            enemyReferences.navMeshAgent.SetDestination(target.transform.position);
        }
    }

    void EBiteBoxOn()
    {
        EBite.SetActive(true);
    }
    void EBiteBoxOff()
    {
        EBite.SetActive(false);
    }
    void Test()
    {
        animator.SetBool("biting", false);
    }

    //======================================================================//
    //                             Enemy Health                             //   
    //======================================================================//
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hit_Enemy"))
        {
            damage = 25;
            HP.TakeDamage(damage);
        }
    }
}
