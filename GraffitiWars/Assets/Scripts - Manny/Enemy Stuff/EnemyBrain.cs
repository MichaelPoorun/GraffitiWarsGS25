using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    public NEWPlayerState_W target;
    private EnemyReferences enemyReferences;
    private BasicEnemyStatemachine_W State;

    private float pathUpdateDeadline; //tracks when the path can be updated
    public float punchingDistance;

    

    private void Awake()
    {
        State = GetComponent<BasicEnemyStatemachine_W>();
        enemyReferences = GetComponent<EnemyReferences>();
    }

    void Start()
    {
        punchingDistance = enemyReferences.navMeshAgent.stoppingDistance;
        target = GameObject.FindFirstObjectByType<NEWPlayerState_W>();
    }

    public void Update()
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
            enemyReferences.anim.SetBool("punching", inRange);
        }
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
}
