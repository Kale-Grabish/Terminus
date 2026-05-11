using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{

        public class VisionSensor : MonoBehaviour
        {
            [SerializeField, Range(0,360)] private float fovAngle;
            [SerializeField] private float viewDistance;
            [SerializeField] private LayerMask layerMask;
            [SerializeField] private GameObject viewPoint;

            public List<GameObject> VisibleObjects { get; } = new List<GameObject>();

            private void Start()
            {
                if(!viewPoint) viewPoint = gameObject;
            }

            private void FovCheck()
            {   
                VisibleObjects.Clear();
            
                // ReSharper disable once Unity.PreferNonAllocApi
                Collider[] results = Physics.OverlapSphere(viewPoint.transform.position, viewDistance, layerMask);
            
                foreach (var result in results)
                {
                    Transform target = result.transform;
                    Vector3 dir = (target.position - viewPoint.transform.position).normalized;
                 
                    if (!(Vector3.Angle(viewPoint.transform.forward, dir) < fovAngle / 2)) continue;
                    if (!Physics.Raycast(viewPoint.transform.position, dir, out RaycastHit hit)) continue;
                    
                    if (hit.transform.gameObject != target.gameObject) continue;
                    Debug.DrawLine(viewPoint.transform.position, hit.point, Color.red);
                
                    VisibleObjects.Add(target.gameObject);
                }
            }
        
            void Update()
            {
                FovCheck();
            }

        }
    }

