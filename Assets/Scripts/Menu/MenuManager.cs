using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject endMenu;
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
}
