using UnityEngine;
using UnityEngine.UI;

public class DisplayUserInput : MonoBehaviour
{
    public Text userInputText;

    void Start()
    {
        // Mengambil teks dari PlayerPrefs
        string userInput = PlayerPrefs.GetString("UserInput");

        // Menampilkan teks pada objek teks
        userInputText.text = userInput;
    }
}
