using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenuUI;
    public GameObject gameOver;
    public GameObject score;
    public AudioManager audioManager;

    public static bool isPaused = false;

    private void Awake() {
        isPaused = false;
    }
    private void Update() {
        if (StaminaManager.isOver) {
            audioManager.StopSound("GameTheme");
            GameOver();
            StaminaManager.isOver = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) { // Pauses when escape is pressed
            if (isPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() {
        // Resumes game
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause() {
        // Pauses Game
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void GameOver() {
        // Ends Game
        score.GetComponent<Text>().text = "SCORE: " + GameManager.score.ToString();
        gameOver.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
