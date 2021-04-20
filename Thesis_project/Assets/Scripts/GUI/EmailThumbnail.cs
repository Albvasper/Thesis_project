using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailThumbnail : MonoBehaviour {

    [SerializeField] private GameObject email;
    [SerializeField] private List<GameObject> otherEmails = new List<GameObject>();
    [SerializeField] private GameObject noEmailSelectedBG;
    
    public void ShowMail(){
        foreach (GameObject email in otherEmails) {
            email.SetActive(false);
        }
        noEmailSelectedBG.SetActive(false);
        email.SetActive(true);
    }
}
