using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace UI
{
    public class SceneSelectorMenu : MonoBehaviour
    {
        [Tooltip("List of scenes to show in menu.  Must also be in build properties scene list.")]
        [SerializeField] private List<string> scenes = new List<string>();
        
        private InputAction _pauseAction;
        private InputAction _closeAction;
        private bool _paused;
        private bool _locked; // stops the menu being hidden without selection
        
        private VisualElement _rootVe;
        private VisualElement _menuUIRoot;

        public bool MenuVisible => _paused;
        
        public void Awake()
        {
            _rootVe = GetComponent<UIDocument>().rootVisualElement;
            
            _pauseAction = InputSystem.actions.FindAction("Pause");
            _closeAction = InputSystem.actions.FindAction("Cancel");

            _menuUIRoot = _rootVe.Q<VisualElement>("menuUIRoot");

            SetUpButtons();
            UpdateMenu();
        }

        public void ShowMenu(bool locked = false)
        {
            _locked = locked;
            _paused = true;
            UpdateMenu();
        }
        
        private void HideMenu()
        {
            if (_locked) return;
            
            _paused = false;
            UpdateMenu();
        }
        
        private void ShowMenu(InputAction.CallbackContext context)
        {
            ShowMenu();
        }

        private void HideMenu(InputAction.CallbackContext context)
        {
            HideMenu();
        }

        private void SwitchToPlayerActions()
        {
            _closeAction.performed -= HideMenu;
            InputSystem.actions.FindActionMap("Menu").Disable();

            InputSystem.actions.FindActionMap("Player").Enable();
            _pauseAction.performed += ShowMenu;
        }

        private void SwitchToUIActions()
        {
            _pauseAction.performed -= ShowMenu;
            InputSystem.actions.FindActionMap("Player").Disable();

            InputSystem.actions.FindActionMap("Menu").Enable();
            _closeAction.performed += HideMenu;
        }
        
        private void UpdateMenu()
        {
            Cursor.lockState = _paused ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = _paused;
            
            if (_paused)
            {
                SwitchToUIActions();
                _menuUIRoot.RemoveFromClassList("hideme");
                _menuUIRoot.AddToClassList("showme");
                
            }
            else
            {
                SwitchToPlayerActions();
                _menuUIRoot.AddToClassList("hideme");
                _menuUIRoot.RemoveFromClassList("showme");
            }
        }
        
        private void SetUpButtons()
        {
            // create and add buttons for scenes
            GroupBox buttonGroup = _rootVe.Q<GroupBox>("ButtonGroup");
            foreach (string scene in scenes)
            {
                Button newButton = new Button();
                newButton.text = Regex.Replace(scene, "(\\B[A-Z])", " $1");
                newButton.clickable.clicked += () => StartScene(scene);
                buttonGroup.Add(newButton);
            }
            
            Button exitButton = _rootVe.Q<Button>("ExitButton");
            exitButton.clickable.clicked += ExitGame;
        }

        private void StartScene(string sceneName)
        {
            HideMenu();
            SceneManager.LoadScene(sceneName);
        }
        
        private void ExitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
            // Quit the Game
            Application.Quit();
        }
    }
}
