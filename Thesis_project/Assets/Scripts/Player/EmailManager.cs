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

    public void OpenEmailScreen() {
        emailScreen.SetActive(true);
    }

    public void CloseEmailScreen() {
        emailScreen.SetActive(false);
    }
}
