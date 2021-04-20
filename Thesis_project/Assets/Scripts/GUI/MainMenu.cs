using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject deskObj;
    [SerializeField] private Image fontImage;
    [SerializeField] private List<AudioSource> audioSources = new List<AudioSource>();
    private float fadeSceneTime;
    private YieldInstruction fadeInstruction = new YieldInstruction();
    
    private void Start() {
        fadeSceneTime = 1.5f;
        CloseOptionsPanel();
        CloseCreditsPanel();
    }

    private void Update() {
        float y = 0.0f;
        y += 0.005f;
        if (y >= 360f) {
            y = 0f;
        }
        deskObj.transform.Rotate(0f, y, 0f);
    }

    public void PlayGame() {
        StartCoroutine(FadeOutScene());
        foreach(AudioSource audio in audioSources) {
            StartCoroutine(StartFade(audio, 1.5f, 0.0f));
        }
    }

    public void ShowOptionsPanel() {
        optionsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
    
    public void ShowCreditsPanel() {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void CloseOptionsPanel() {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void CloseCreditsPanel() {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    private IEnumerator FadeOutScene() {
        float elapsedTime = 0.0f;
        Color c = fontImage.color;
        while (elapsedTime < fadeSceneTime) {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime ;
            c.a = Mathf.Clamp01(elapsedTime / fadeSceneTime);
            fontImage.color = c;
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    private IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume) {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration) {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
