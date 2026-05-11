using System.Collections.Generic;
using Interfaces;
using Player;
using UnityEngine;

namespace TestThings
{
    /**
     * Just for testing health & damage
     * if added to a GameObject with a trigger collider
     * if object with tag `Player` is within the collider
     * damage is applied each second
     */
    public class DangerSpace : MonoBehaviour
    {
        [SerializeField] private int damagePerSecond;
        private readonly List<GameObject> _targets = new List<GameObject>();
        private float _timeSinceZap;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
            if (other.gameObject.CompareTag("Damageable") || other.gameObject.CompareTag("Player"))
            {
                _targets.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _targets.Remove(other.gameObject);
        }

        private void Update()
        {
            _timeSinceZap += Time.deltaTime;
        
            if (_timeSinceZap >= 1)
            {
                _timeSinceZap = 0;
                _targets.ForEach(delegate(GameObject target)
                {
                    target?.GetComponent<IDamageable>()?.TakeDamage(damagePerSecond);
                });
            
            }
                
        }
    }
}
