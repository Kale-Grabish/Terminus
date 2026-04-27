using Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class PlayerHUD : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [SerializeField] private PlayerHealth  playerHealth;
        [SerializeField] private PlayerPower playerPower;
    
        private VisualElement _rootVe;
        private Label _healthLabel;
        private Label _powerLabel;
        void Start()
        {
            _rootVe = GetComponent<UIDocument>().rootVisualElement;
            _healthLabel = _rootVe.Q<Label>("healthLabel");
            _powerLabel = _rootVe.Q<Label>("powerLabel");
        }

        // Update is called once per frame
        void Update()
        {
            _healthLabel.text = playerHealth.CurrentHealth.ToString();
            _powerLabel.text = playerPower.CurrentPower.ToString();
        }
    }
}
