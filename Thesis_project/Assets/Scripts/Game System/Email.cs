using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Email : MonoBehaviour {

    [SerializeField] private GameObject email;
    [SerializeField] private string title;
    [SerializeField] private string body;
    [SerializeField] private Text titleField;
    [SerializeField] private Text bodyField;

    public void OpenEmail() {
        email.SetActive(true);
    }

    public void CloseEmail() {
        email.SetActive(false);
    }
}
