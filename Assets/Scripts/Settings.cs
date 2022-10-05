using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
                _settingsPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = GameManager.GM.Forward.ToString();
            } else if (_settingsPanel.GetChild(i).name == "Backward")
            {
                _settingsPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = GameManager.GM.Backward.ToString();
            } else if (_settingsPanel.GetChild(i).name == "Left")
            {
                _settingsPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = GameManager.GM.Left.ToString();
            } else if (_settingsPanel.GetChild(i).name == "Right")
            {
                _settingsPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = GameManager.GM.Right.ToString();
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

    public IEnumerator AssignKey(string keyName)
    {
        _waitingForKey = true;
        yield return WaitForKey();

        switch (keyName)
        {
            case "Forward":
                GameManager.GM.Forward = _newKey;
                _buttonText.text = GameManager.GM.Forward.ToString();
                PlayerPrefs.SetString("forwardKey", GameManager.GM.Forward.ToString());
                break;
            case "Backward":
                GameManager.GM.Backward = _newKey;
                _buttonText.text = GameManager.GM.Backward.ToString();
                PlayerPrefs.SetString("backwardKey", GameManager.GM.Backward.ToString());
                break;
            case "Left":
                GameManager.GM.Left = _newKey;
                _buttonText.text = GameManager.GM.Left.ToString();
                PlayerPrefs.SetString("leftKey", GameManager.GM.Left.ToString());
                break;
            case "Right":
                GameManager.GM.Right = _newKey;
                _buttonText.text = GameManager.GM.Right.ToString();
                PlayerPrefs.SetString("rightKey", GameManager.GM.Right.ToString());
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
