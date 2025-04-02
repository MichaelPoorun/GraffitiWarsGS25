using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    public NEWPlayerState_W target;
    private EnemyReferences enemyReferences;

    private float pathUpdateDeadline; //tracks when the path can be updated
    private float punchingDistance;

    [Header("Punch Settings")]
    public GameObject punchHitbox;
    /*public float punchActivationDelay = 0.2f;*/ //delay before enabling the hitbox during a punch
    public float punchDuration = 0.3f;//how long the hitbox will stay active

    private void Awake()
    {
        enemyReferences = GetComponent<EnemyReferences>();
    }

    void Start()
    {
        punchingDistance = enemyReferences.navMeshAgent.stoppingDistance;
        target = GameObject.FindFirstObjectByType<NEWPlayerState_W>();

        if (punchHitbox != null)
        {
            punchHitbox.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Punch hitbox not assigned");
        }
    }

    void Update()
    {
        if (target != null) 
        {
            //computes the distance between the enemy and target
            bool inRange = Vector3.Distance(transform.position, target.transform.position) <= punchingDistance; 
            
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
        enemyReferences.anim.SetFloat("Speed", enemyReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
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

    public void ChangeState()
    {
        Debug.Log("ChangeState event triggered");
    }

    public void BasicPunchBoxOn()
    {
        if (punchHitbox != null)
        {
            punchHitbox.SetActive(true); // enables the punch hitbox
            Invoke(nameof(BasicPunchBoxOff), punchDuration); // automatically turns off the hitbox after punch duration
        }
    }

    public void BasicPunchBoxOff()
    {
        if (punchHitbox != null)
        {
            punchHitbox.SetActive(false);
        }
    }
}
