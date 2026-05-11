using System;
using UnityEngine;

namespace Enemy
{
    
    public class PlayerProximityDetectorSensor : MonoBehaviour
    {
        [SerializeField] private float range = 3;

        public GameObject DetectedPlayer { get; private set; }
        public bool NearPlayer { get; private set; }

        private SphereCollider _collider;

        private void Start()
        {
            _collider = gameObject.AddComponent<SphereCollider>();
            _collider.isTrigger = true;
            _collider.radius = range;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            NearPlayer = true;
            DetectedPlayer = other.gameObject;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            NearPlayer = false;
            DetectedPlayer = null;
        }
    }
}
