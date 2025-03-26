using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyReferences : MonoBehaviour
{
    [HideInInspector]public NavMeshAgent navMeshAgent;
    [HideInInspector]public Animator anim;

    [Header("Stats")]

    public float pathUpdateDelay = 0.2f; //updates destination

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
}
