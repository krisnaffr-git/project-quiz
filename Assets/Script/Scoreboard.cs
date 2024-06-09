using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    public TextMeshProUGUI scoreboardText; // Referensi ke TextMeshPro untuk scoreboard

    void Start()
    {
        string userName = PlayerPrefs.GetString("userName");
        int userScore = PlayerPrefs.GetInt(userName + "_Score");
        scoreboardText.text = "Nama: " + userName + "\nSkor: " + userScore;
    }
}
