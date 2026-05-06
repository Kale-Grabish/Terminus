using System.Collections;
using UnityEngine;

namespace Utilities
{
    public class AutoDie : MonoBehaviour
    {
        [SerializeField] private float lifeTimeSeconds;

        void Start()
        {
            StartCoroutine(Die());
        }

        private IEnumerator Die()
        {
            yield return new WaitForSeconds(lifeTimeSeconds);
            Destroy(gameObject);
        }
    }
}