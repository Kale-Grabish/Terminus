using Enemy;
using Interfaces;
using Player;
using UnityEngine;
using UnityEngine.AI;

enum EnemyState
{
    IDLE,
    CHASE,
    ATTACKING,
    DEAD
}

public class BasicSkeletonEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _target;
    [SerializeField] private EnemyState defaultState = EnemyState.IDLE;
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private VisionSensor  visionSensor;
    [SerializeField] private PlayerProximityDetectorSensor playerProximityDetectorSensor;
    
    private int _currentHealth;
    
    private EnemyState _currentState;
    
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
            default: DoIdle(); break;
        }
    }

    private bool CanSeePlayer()
    {
        return visionSensor.VisibleObjects.Exists(go => go.CompareTag("Player"));
    }
    
    private bool AmNextToTarget()
    {
        return playerProximityDetectorSensor.NearPlayer;
    }

    private void DoIdle()
    {
        _animator.SetFloat("Speed", 0);
        navMeshAgent.isStopped = true;
        
        if (CanSeePlayer() && !AmNextToTarget())
        {
            _currentState = EnemyState.CHASE;
        }
    }

    private void DoChase()
    {
        _animator.SetFloat("Speed", 0.5f);
        navMeshAgent.SetDestination(_target.gameObject.transform.position);
        navMeshAgent.isStopped = false;
        
        if (AmNextToTarget())
        {
            _currentState = EnemyState.IDLE;
        }

    }

    private void DoAttacking()
    {
        
    }

    private void DoDeath()
    {
        navMeshAgent.isStopped = true;
        _animator.SetBool("IsDead", true);
    }

    public void TakeDamage(int amount, PainTypes painType = PainTypes.Hit)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            _currentState = EnemyState.DEAD;
            return;
        }
        _animator.SetTrigger("Hit");
    }
}
