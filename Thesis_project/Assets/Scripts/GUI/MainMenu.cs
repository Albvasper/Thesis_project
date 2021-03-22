using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject creditsPanel;

    private void Start() {
        CloseOptionsPanel();
        CloseCreditsPanel();
    }

    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void ShowOptionsPanel() {
        optionsPanel.SetActive(true);
    }
    
    public void ShowCreditsPanel() {
        creditsPanel.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void CloseOptionsPanel() {
        optionsPanel.SetActive(false);
    }

    public void CloseCreditsPanel() {
        creditsPanel.SetActive(false);
    }
}
