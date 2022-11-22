using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject settingsMenu;
    
        public void Exit()
        {
            Application.Quit();
        }
    
        public void Settings()
        {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }
    
        public void Play()
        {
            SceneManager.LoadScene("Main");
        }
    }
}
