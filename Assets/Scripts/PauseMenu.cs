using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{
    public static bool isGamePaused = false;
    
    public GameObject pauseMenu;

    public void TogglePause() {
        isGamePaused = !isGamePaused;
        if(isGamePaused) {
            Time.timeScale = 0f;
            AudioListener.pause = true;
            pauseMenu.SetActive(true);
        } else {
            Time.timeScale = 1f;
            AudioListener.pause = false;
            pauseMenu.SetActive(false);
        }
    }

    public void Restart() {
        TogglePause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit() {
        TogglePause();
        SceneManager.LoadScene("Title Screen");
    }
}