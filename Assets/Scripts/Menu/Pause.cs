using UnityEngine;

namespace Menu
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        public void Exit()
        {
            Application.Quit();
        }

        public void Continue()
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }
}
