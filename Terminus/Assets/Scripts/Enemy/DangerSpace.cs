using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Enemy
{
    public class DangerSpace : MonoBehaviour
    {
        [SerializeField] private int damagePerSecond;
        private readonly List<GameObject> _targets = new List<GameObject>();
        private float _timeSinceZap;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _targets.Add(other.gameObject);    

            }
        }

        private void OnTriggerExit(Collider other)
        {
            _targets.Remove(other.gameObject);

        }
    

        void Update()
        {
            _timeSinceZap += Time.deltaTime;
        
            if (_timeSinceZap >= 1)
            {
                // Debug.Log(" =========================================================================== zap");
                _timeSinceZap = 0;
                _targets.ForEach(delegate(GameObject target)
                {
                    target?.GetComponent<PlayerHealth>().TakeDamage(damagePerSecond);
                });
            
            }
                
        }
    }
}
