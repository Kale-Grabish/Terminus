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
        private bool _paused;

        private VisualElement _rootVe;
        private VisualElement _menuUIRoot;
        public void Awake()
        {
            _rootVe = GetComponent<UIDocument>().rootVisualElement;
            
            //ensure hidden by default
            _menuUIRoot =  _rootVe.Q<VisualElement>("menuUIRoot");
            _menuUIRoot.AddToClassList("hideme");
            _menuUIRoot.RemoveFromClassList("showme");

            // use pause action to toggle menu
            _pauseAction = InputSystem.actions.FindAction("Pause");
            _pauseAction.performed += _ =>
            {
                _paused = !_paused;
                
                Cursor.lockState = _paused ? CursorLockMode.None : CursorLockMode.Locked;
                Cursor.visible = _paused;
                Time.timeScale = _paused ? 0f : 1f;
                

                if (_paused)
                {
                    _menuUIRoot.RemoveFromClassList("hideme");
                    _menuUIRoot.AddToClassList("showme");
                }
                else
                {
                    _menuUIRoot.AddToClassList("hideme");
                    _menuUIRoot.RemoveFromClassList("showme");
                }
            };
        }
        
        public void OnEnable()
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
            SceneManager.LoadScene(sceneName);
            
            _menuUIRoot.RemoveFromClassList("showme");
            _menuUIRoot.AddToClassList("hideme");
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
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
