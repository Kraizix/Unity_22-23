using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject endMenu;

    [SerializeField] private List<Button> _buttons;

    [SerializeField] private GameObject upgradeMenu;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.isEnded)
        {
            endMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    
    public void Exit()
    {
        Application.Quit();
    }

    public void Replay()
    {
        GameManager.gm.isEnded = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }

    public void Upgrade(List<Upgrade> upgrades)
    {
        upgradeMenu.SetActive(true);
        Time.timeScale = 0f;
        for (int i = 0; i < upgrades.Count; i++)
        {
            var up = upgrades[i];
            _buttons[i].onClick.AddListener(delegate { up.Upgrad();
                Time.timeScale = 1f; upgradeMenu.SetActive(false);
            });
            _buttons[i].GetComponentInChildren<TMP_Text>().text = upgrades[i].Name;
        }
    }
}
