using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailManager : MonoBehaviour {

    #region Singleton Pattern
        private static EmailManager instance;
        public static EmailManager Instance { 
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

    [SerializeField] private GameObject emailScreen;
    [SerializeField] private GameObject bg;
    [SerializeField] private List<GameObject> emails = new List<GameObject>();
    private bool gamePaused;

    private void Start() {
        OpenEmailScreen();
    }

    public void OpenEmailScreen() {
        emailScreen.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        foreach (GameObject e in emails) {
            e.SetActive(false);
        }
        bg.SetActive(true);
    }

    public void CloseEmailScreen() {
        emailScreen.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public bool GetIsGamePaused() {
        return gamePaused;
    }
}