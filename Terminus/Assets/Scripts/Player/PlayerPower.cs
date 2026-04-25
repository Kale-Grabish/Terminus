using UnityEngine;

namespace Player
{
    public class PlayerPower : MonoBehaviour
    {
        [SerializeField] 
        private int maxPower = 10;
        private int _currentPower;


        private void Start()
        {
            CurrentPower = maxPower;
        }
        
        public int MaxPower { 
            get => maxPower;
            set
            {
                maxPower = value; 
                _currentPower = Mathf.Clamp(value, 0, maxPower);
            }
        }
        
        public int CurrentPower
        {
            get => _currentPower;
            set => _currentPower = Mathf.Clamp(value, 0, maxPower);
        }
        
    }
}
