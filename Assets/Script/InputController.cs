using UnityEngine;
using UnityEngine.UI;
using TMPro; // Pastikan untuk menambahkan ini
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    // Gunakan TMP_InputField untuk TextMeshPro Input Field
    public TMP_InputField inputField; 
    public Button submitButton;
    public TMP_Text displayNameText; // Gunakan TMP_Text untuk TextMeshPro Text

    public static string userName;

    void Start()
    {
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(SubmitName);
        }

        if (displayNameText != null)
        {
            displayNameText.text = "Welcome, " + userName;
        }
    }

    void SubmitName()
    {
        userName = inputField.text;
        PlayerPrefs.SetString("userName", userName);
        SceneManager.LoadScene("HalamanMode");
    }
}
