using Enemy;
using Interfaces;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class BasicSkeletonEnemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject _target;
        [SerializeField] private EnemyState defaultState = EnemyState.Idle;
        [SerializeField] private int maxHealth = 50;
        [SerializeField] private VisionSensor  visionSensor;
        [SerializeField] private PlayerProximityDetectorSensor playerProximityDetectorSensor;
        private EnemyWeapon _weapon;
        private int _currentHealth;
        private float turnSpeed = 3.0f;
        private EnemyState _currentState;
        private bool _isActive;
        
        private void Start()
        {
            _currentState = defaultState;
            _currentHealth = maxHealth;
            _weapon = GetComponentInChildren<EnemyWeapon>();
        }
    
        private void Update()
        {
            switch (_currentState)
            {
                case EnemyState.Idle: DoIdle(); break;
                case EnemyState.Chase:  DoChase(); break;
                case EnemyState.Attacking: DoAttacking(); break;
                case EnemyState.Dead: DoDeath(); break;
                case EnemyState.Searching: DoSearch(); break;
                case EnemyState.Cheering: DoCheer(); break;

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
                _currentState = EnemyState.Chase;
            }
            
            if (CanSeePlayer() && AmNextToTarget())
            {
                _currentState = EnemyState.Attacking;
            }

            CheckForPlayerDeath();
        }

        private void CheckForPlayerDeath()
        {
            if (!_target.GetComponent<IDamageable>().IsAlive())
            {
                _currentState = EnemyState.Cheering;
            }
        }
        
        private void DoChase()
        {
            animator.SetFloat("Speed", 0.5f);
            navMeshAgent.SetDestination(_target.gameObject.transform.position);
            navMeshAgent.isStopped = false;
            
            if (CanSeePlayer() && AmNextToTarget())
            {
                _currentState = EnemyState.Attacking;
            }
            CheckForPlayerDeath();
        }
        
        private void DoAttacking()
        {
            navMeshAgent.isStopped = true;
            animator.SetFloat("Speed", 0.0f);
            FacePlayer();
            
            if (!AmNextToTarget())
            {
                _currentState = EnemyState.Chase;
                return;
            }
            animator.SetTrigger("DoAttack");
            CheckForPlayerDeath();
        }

        private void DoSearch()
        {
            
        }
        
        private void DoDeath()
        {
            navMeshAgent.isStopped = true;
            animator.SetBool("IsDead", true);
        }

        private void DoCheer()
        {
            navMeshAgent.isStopped = true;
            animator.SetFloat("Speed", 0.0f);
            animator.SetBool("IsCheering", true);
        }
        
        public bool TakeDamage(int amount, PainTypes painType = PainTypes.Hit)
        {
            //if already dead: bail 
            if (!IsAlive()) return false;
            
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                _currentState = EnemyState.Dead;
                return true;
            }

            _weapon.DamageActive = false;
            animator.SetTrigger("Hit");
            _currentState = EnemyState.Chase;
            return true;
        }

        public bool IsAlive()
        {
            return _currentHealth > 0;
        }

        public int CurrentHealth => _currentHealth;
        public int MaxHealth => maxHealth;

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
}