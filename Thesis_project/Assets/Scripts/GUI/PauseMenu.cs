using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    #region Singleton Pattern
        private static PauseMenu instance;
        public static PauseMenu Instance {
            get {
                return instance;
            }
        }
        private void Awake() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(this);
            }
        }
    #endregion

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
    private bool gamePaused;

    private void Start() {
        gamePaused = false;
        pausePanel.SetActive(false);
    }

    public void PauseGame() {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void ResumeGame() {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void BackToMainMenu() {
        SceneManager.LoadScene(0);
    }

    public bool GetIsGamePaused() {
        return gamePaused;
    }

    public void ShowOptiosnPanel() {
        optionsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void CloseOptionsPanel() {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
        
    }
}
