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
    [SerializeField] private Text replyEmailBody;
    [SerializeField] private AudioClip introductionEndingClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Button replyButton;
    [SerializeField] private Button sendButton;
    [SerializeField] private List<Button> emailThumbnails = new List<Button>();

    private Queue<string> replyEmailContent = new Queue<string>();
    private float showMadeByTime;
    private float hideMadeByTime;
    private float showEmailWindowTime;
    private float hideEmailWindowTime;
    private float showTitleScreenTime;
    private float hideTitleScreenTime;
    private float exitSceneTime;
    private float fadeOutRate;
    private bool fadeOutGameTitle;
    private bool replyEmail;
    private string replyEmailText;

    private void Awake() {
        #region reply email content
            replyEmailContent.Enqueue("De");
            replyEmailContent.Enqueue("ar ");
            replyEmailContent.Enqueue("Ms");
            replyEmailContent.Enqueue(". ");
            replyEmailContent.Enqueue("Ca");
            replyEmailContent.Enqueue("rt");
            replyEmailContent.Enqueue("er");
            replyEmailContent.Enqueue("\n");
            replyEmailContent.Enqueue("Tha");
            replyEmailContent.Enqueue("nk");
            replyEmailContent.Enqueue(" you");
            replyEmailContent.Enqueue(" for");
            replyEmailContent.Enqueue(" re");
            replyEmailContent.Enqueue("ac");
            replyEmailContent.Enqueue("hi");
            replyEmailContent.Enqueue("ng");
            replyEmailContent.Enqueue(" out");
            replyEmailContent.Enqueue(" so");
            replyEmailContent.Enqueue(" ea");
            replyEmailContent.Enqueue("rly");
            replyEmailContent.Enqueue(", ");
            replyEmailContent.Enqueue("I ");
            replyEmailContent.Enqueue("am");
            replyEmailContent.Enqueue(" ve");
            replyEmailContent.Enqueue("ry ");
            replyEmailContent.Enqueue("ex");
            replyEmailContent.Enqueue("ci");
            replyEmailContent.Enqueue("ted");
            replyEmailContent.Enqueue(" to");
            replyEmailContent.Enqueue(" be");
            replyEmailContent.Enqueue(" pa");
            replyEmailContent.Enqueue("rt");
            replyEmailContent.Enqueue(" of");
            replyEmailContent.Enqueue(" the");
            replyEmailContent.Enqueue(" te");
            replyEmailContent.Enqueue("am ");
            replyEmailContent.Enqueue("and");
            replyEmailContent.Enqueue(" will");
            replyEmailContent.Enqueue(" gla");
            replyEmailContent.Enqueue("dly");
            replyEmailContent.Enqueue(" be");
            replyEmailContent.Enqueue(" the");
            replyEmailContent.Enqueue("re");
            replyEmailContent.Enqueue(" as");
            replyEmailContent.Enqueue(" so");
            replyEmailContent.Enqueue("on ");
            replyEmailContent.Enqueue("as");
            replyEmailContent.Enqueue(" po");
            replyEmailContent.Enqueue("ss");
            replyEmailContent.Enqueue("ib");
            replyEmailContent.Enqueue("le.");
            replyEmailContent.Enqueue(" Tha");
            replyEmailContent.Enqueue("nks ");
            replyEmailContent.Enqueue(" for");
            replyEmailContent.Enqueue(" the");
            replyEmailContent.Enqueue(" op");
            replyEmailContent.Enqueue("po");
            replyEmailContent.Enqueue("rt");
            replyEmailContent.Enqueue("un");
            replyEmailContent.Enqueue("it");
            replyEmailContent.Enqueue("y,");
            replyEmailContent.Enqueue(" I");
            replyEmailContent.Enqueue(" wi");
            replyEmailContent.Enqueue("ll");
            replyEmailContent.Enqueue(" not");
            replyEmailContent.Enqueue(" dis");
            replyEmailContent.Enqueue("ap");
            replyEmailContent.Enqueue("po");
            replyEmailContent.Enqueue("in");
            replyEmailContent.Enqueue("t.");
        #endregion
    }

    private void Start() {
        sendButton.gameObject.SetActive(false);
        sendButton.interactable = false;
        replyEmailText = "";
        replyEmail = false;
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
        madeByText.SetActive(false);
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
        if (replyEmail == true && Input.anyKeyDown) {
            if (replyEmailContent.Count != 0) {
                replyEmailText = replyEmailText + replyEmailContent.Dequeue();
                replyEmailBody.text = replyEmailText;
            } else {
                sendButton.interactable = true;
            }
        }
    }

    public void ReplyEmail() {
        foreach (Button thumbnail in emailThumbnails) {
            thumbnail.interactable = false;
        }
        replyEmail = true;
        replyButton.gameObject.SetActive(true);
        replyEmailBody.gameObject.SetActive(true);
        sendButton.gameObject.SetActive(true);
        replyButton.interactable = false;
    }

    public void SendReplyEmail() {
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