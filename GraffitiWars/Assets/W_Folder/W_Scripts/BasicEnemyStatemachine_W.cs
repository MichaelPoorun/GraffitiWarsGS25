using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum BEnemyState //Where all player states are kept
{
    Idle,
    Punch
}

public class BasicEnemyStatemachine_W : MonoBehaviour
{ 
    [Header("Player Variables")]
    private Rigidbody rb;
    public int damage;
    public NEWPlayerState_W target;
    public NEWPlayerState_W player;
    private EnemyReferences enemyReferences;

    private float pathUpdateDeadline; //tracks when the path can be updated
    private float punchingDistance;

    public GameObject EPunch;

    [Header("Player Bools")]
    private bool inRange;

    [Header("Player Attack Hitboxes")]

    [Header("References")]
    public HealthSystem HP;

    [Header("Misc")]
    public Animator animator;
    public BEnemyState currentState; //Variable that stores current state

    void Awake()
    {
        EPunch.SetActive(false);
        enemyReferences = GetComponent<EnemyReferences>();
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        punchingDistance = enemyReferences.navMeshAgent.stoppingDistance;
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
            inRange = Vector3.Distance(transform.position, target.transform.position) <= punchingDistance;

            if (inRange)
            {
                LookAtTarget();
            }
            else
            {
                UpdatePath();
            }
            /*enemyReferences.anim.SetBool("punching", inRange);*/

        }
        /*enemyReferences.anim.SetFloat("Speed", enemyReferences.navMeshAgent.desiredVelocity.sqrMagnitude);*/
    }

    //======================================================================//
    //                            State Machine                             //   
    //======================================================================//
    void HandleState()
    {
        switch (currentState)
        {
            case BEnemyState.Idle:
                animator.SetBool("isIdle", true);
                animator.SetBool("isIdle", false);
                if (inRange)
                {
                    ChangeState(BEnemyState.Punch);
                }
                    break;
        }
    }
    public void ChangeState(BEnemyState newState)
    {
        if (currentState == newState) return;

        currentState = newState;

        switch (currentState)
        {
            case BEnemyState.Punch:
                animator.SetBool("isIdle", false);
                animator.SetBool("isPunch", true);
                break;
        }
   
    }

    //======================================================================//
    //                                Misc                                  //   
    //======================================================================//
    void BackToIdle(BEnemyState s)
    {
        if (currentState != s && s != BEnemyState.Idle)
        {
            Debug.Log("CS: " + currentState + " / " + s);
            return;
        }
        animator.SetBool("isPunch", false);
        ChangeState(BEnemyState.Idle);
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

    void EPunchBoxOn()
    {
        EPunch.SetActive(true);
    }
    void EPunchBoxOff()
    {
        EPunch.SetActive(false);
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
