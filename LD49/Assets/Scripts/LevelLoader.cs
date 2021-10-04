using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public Animator transition;
    public AudioManager audioManager;

    private float transitionTime = 1f;
    private void Awake() {
        Time.timeScale = 1f;
    }

    private void Start() {
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            audioManager.Play("OpenTheme");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1) {
            audioManager.Play("GameTheme");
        }
    }

    public void LoadMainLevel() {
        // Loads main level
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void ToMainMenu() {
        // Resumes time scale and goes to main menu
        Time.timeScale = 1f;
        PauseMenu.isPaused = false;
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    }
    public void RestartLevel() {
        // Resumes time scale and goes to main menu
        StopAllCoroutines();
        Time.timeScale = 1f;
        PauseMenu.isPaused = false;
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    private IEnumerator LoadLevel(int levelIndex) {
        // Switches to designated scene after some time
        transition.SetTrigger("Switch");
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

}
