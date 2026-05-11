using Enemy;
using Interfaces;
using Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

enum EnemyState
{
    IDLE,
    CHASE,
    ATTACKING,
    DEAD,
    SEARCHING
}

public class BasicSkeletonEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject _target;
    [SerializeField] private EnemyState defaultState = EnemyState.IDLE;
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private VisionSensor  visionSensor;
    [SerializeField] private PlayerProximityDetectorSensor playerProximityDetectorSensor;
    
    private int _currentHealth;
    private float turnSpeed = 3.0f;
    private EnemyState _currentState;
    private bool _isActive;
    
    private void Start()
    {
        _currentState = defaultState;
        _currentHealth = maxHealth;
    }
    
    private void Update()
    {
        switch (_currentState)
        {
            case EnemyState.IDLE: DoIdle(); break;
            case EnemyState.CHASE:  DoChase(); break;
            case EnemyState.ATTACKING: DoAttacking(); break;
            case EnemyState.DEAD: DoDeath(); break;
            case EnemyState.SEARCHING: DoSearch(); break;
            default: DoIdle(); break;
        }
    }

    private bool CanSeePlayer()
    {
        return visionSensor.VisibleObjects.Exists(go => go.CompareTag("Player"));
    }
    
    private bool AmNextToTarget()
    {
        if (!navMeshAgent.isStopped)
        {
            return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
        }
        return playerProximityDetectorSensor.NearPlayer;
    }

    private void DoIdle()
    {
        animator.SetFloat("Speed", 0);
        navMeshAgent.isStopped = true;

        if (CanSeePlayer())
        {
            FacePlayer();
        }
        
        if (CanSeePlayer() && !AmNextToTarget())
        {
            _currentState = EnemyState.CHASE;
        }
        
        if (CanSeePlayer() && AmNextToTarget())
        {
            _currentState = EnemyState.ATTACKING;
        }
        
    }

    private void DoChase()
    {
        animator.SetFloat("Speed", 0.5f);
        navMeshAgent.SetDestination(_target.gameObject.transform.position);
        navMeshAgent.isStopped = false;
        
        if (CanSeePlayer() && AmNextToTarget())
        {
            _currentState = EnemyState.ATTACKING;
        }
    }

    
    
    private void DoAttacking()
    {
        navMeshAgent.isStopped = true;
        animator.SetFloat("Speed", 0.0f);
        FacePlayer();
        
        if (!AmNextToTarget())
        {
            _currentState = EnemyState.CHASE;
            return;
        }
        animator.SetTrigger("DoAttack");
    }

    private void DoSearch()
    {
        
    }
    
    private void DoDeath()
    {
        navMeshAgent.isStopped = true;
        animator.SetBool("IsDead", true);
    }
    
    public void TakeDamage(int amount, PainTypes painType = PainTypes.Hit)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            _currentState = EnemyState.DEAD;
            return;
        }
        animator.SetTrigger("Hit");
    }

    private void FacePlayer()
    {
        GameObject player = visionSensor.VisibleObjects.Find(go => go.CompareTag("Player"));
        if (!player) return;
            
        // determine vector to the player
        var vectorToPlayer = player.transform.position - transform.position;
        // rotate towards
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, vectorToPlayer, Time.fixedDeltaTime/turnSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
