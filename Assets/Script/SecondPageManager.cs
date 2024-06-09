using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SecondPageManager : MonoBehaviour
{
    public TMP_Text displayNameText;
    public Button Button; // Referensi ke tombol start

     void Start()
    {
        Button.onClick.AddListener(StartQuizFunction);

         if (displayNameText != null)
        {
            displayNameText.text = "Welcome, " + InputController.userName;
        }
    }

    void StartQuizFunction()
        {   
            string userName = PlayerPrefs.GetString("userName");
            SceneManager.LoadScene("HalamanQuizEasy"); // Ganti dengan nama scene kuis Anda
        }
    }

