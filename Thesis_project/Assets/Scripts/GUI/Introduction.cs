using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour {

    [SerializeField] private GameObject madeByText;
    [SerializeField] private GameObject emailWindow;
    [SerializeField] private Text titleScreen;
    [SerializeField] private AudioClip introductionEndingClip;
    [SerializeField] private AudioSource audioSource;
    private float showMadeByTime;
    private float hideMadeByTime;
    private float showEmailWindowTime;
    private float hideEmailWindowTime;
    private float showTitleScreenTime;
    private float hideTitleScreenTime;
    private float exitSceneTime;
    private float fadeOutRate;
    private bool fadeOutGameTitle;
    private void Start() {
        fadeOutGameTitle = false;
        fadeOutRate = 0.1f;
        showMadeByTime = 22.6f;
        hideMadeByTime = 8.0f;
        showEmailWindowTime = 53.3f;
        hideEmailWindowTime = 2.0f;
        showTitleScreenTime = 7.35f;
        hideTitleScreenTime = 5.0f;
        exitSceneTime = 3.0f;
        titleScreen.gameObject.SetActive(false);
        emailWindow.SetActive(false);
        emailWindow.SetActive(false);
        StartCoroutine(WaitToShowMadeByText());
        StartCoroutine(WaitToShowEmailWindow());
    }

    private void Update() {
        if (fadeOutGameTitle == true) {
            titleScreen.CrossFadeAlpha(0.0f, fadeOutRate, false);
        }
        if (fadeOutGameTitle == true && audioSource.isPlaying == true) {
            StartCoroutine(WaitToExitScene());
            fadeOutGameTitle = false;
        }
    }

    public void InitIntroductionEnding() {
        StartCoroutine(WaitToHideEmailWindow());
    }

    private IEnumerator WaitToShowMadeByText() {
        yield return new WaitForSeconds(showMadeByTime);
        madeByText.SetActive(true);
        StartCoroutine(WaitToDisableMadeByText());
    }

    private IEnumerator WaitToDisableMadeByText() {
        yield return new WaitForSeconds(hideMadeByTime);
        madeByText.SetActive(false);
    }

    private IEnumerator WaitToShowEmailWindow() {
        yield return new WaitForSeconds(showEmailWindowTime);
        emailWindow.SetActive(true);
        StartCoroutine(WaitToDisableMadeByText());
    }

    private IEnumerator WaitToHideEmailWindow() {
        audioSource.clip = introductionEndingClip;
        audioSource.Play();
        yield return new WaitForSeconds(hideEmailWindowTime);
        emailWindow.SetActive(false);
        StartCoroutine(WaitToShowTitle());
    }

    private IEnumerator WaitToShowTitle() {
        yield return new WaitForSeconds(showTitleScreenTime);
        titleScreen.gameObject.SetActive(true);
        StartCoroutine(WaitToHideTitle());
    }

    private IEnumerator WaitToHideTitle() {
        yield return new WaitForSeconds(hideTitleScreenTime);
        fadeOutGameTitle = true;
    }

    private IEnumerator WaitToExitScene() {
        yield return new WaitForSeconds(exitSceneTime);
        SceneManager.LoadScene(2);
    }
}