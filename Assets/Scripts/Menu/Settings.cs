using System.Collections;
using TMPro;
using UnityEngine;

namespace Menu
{
    public class Settings : MonoBehaviour
    {
        // Start is called before the first frame update
        private Transform _settingsPanel;
        private Event _keyEvent;
        private TMP_Text _buttonText;
        private KeyCode _newKey;
        private bool _waitingForKey;
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject settingsMenu;

        void Start()
        {
            _settingsPanel = transform;
            _waitingForKey = false;
            for (int i = 0; i < _settingsPanel.childCount; i++)
            {
                if (_settingsPanel.GetChild(i).name == "Forward")
                {
                    _settingsPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = GameManager.gm.Forward.ToString();
                } else if (_settingsPanel.GetChild(i).name == "Backward")
                {
                    _settingsPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = GameManager.gm.Backward.ToString();
                } else if (_settingsPanel.GetChild(i).name == "Left")
                {
                    _settingsPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = GameManager.gm.Left.ToString();
                } else if (_settingsPanel.GetChild(i).name == "Right")
                {
                    _settingsPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = GameManager.gm.Right.ToString();
                }
            }
        }

        void OnGUI()
        {
            _keyEvent = Event.current;
            if (!_keyEvent.isKey || !_waitingForKey) return;
            _newKey = _keyEvent.keyCode;
            _waitingForKey = false;
        }

        public void StartAssignment(string keyName)
        {
            if (!_waitingForKey)
            {
                StartCoroutine(AssignKey(keyName));
            }
        }

        public void SendText(TMP_Text text)
        {
            _buttonText = text;
        }

        private IEnumerator WaitForKey()
        {
            while (!_keyEvent.isKey)
            {
                yield return null;
            }
        }

        private IEnumerator AssignKey(string keyName)
        {
            _waitingForKey = true;
            yield return WaitForKey();

            switch (keyName)
            {
                case "Forward":
                    GameManager.gm.Forward = _newKey;
                    _buttonText.text = GameManager.gm.Forward.ToString();
                    PlayerPrefs.SetString("forwardKey", GameManager.gm.Forward.ToString());
                    break;
                case "Backward":
                    GameManager.gm.Backward = _newKey;
                    _buttonText.text = GameManager.gm.Backward.ToString();
                    PlayerPrefs.SetString("backwardKey", GameManager.gm.Backward.ToString());
                    break;
                case "Left":
                    GameManager.gm.Left = _newKey;
                    _buttonText.text = GameManager.gm.Left.ToString();
                    PlayerPrefs.SetString("leftKey", GameManager.gm.Left.ToString());
                    break;
                case "Right":
                    GameManager.gm.Right = _newKey;
                    _buttonText.text = GameManager.gm.Right.ToString();
                    PlayerPrefs.SetString("rightKey", GameManager.gm.Right.ToString());
                    break;
            }

            yield return null;
        }
    
        public void ExitSettings()
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
        }
    }
}
