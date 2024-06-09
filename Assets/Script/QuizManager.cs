using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText; // Referensi ke TextMeshPro untuk pertanyaan
    public Button trueButton; // Referensi ke tombol "Benar"
    public Button falseButton; // Referensi ke tombol "Salah"
    public TextMeshProUGUI resultText; // Referensi ke TextMeshPro untuk hasil
    public TextMeshProUGUI scoreText; // Referensi ke TextMeshPro untuk skor
    public Slider healthBar; // Referensi ke UI Slider untuk health bar

    private int currentQuestionIndex = 0; // Indeks pertanyaan saat ini
    private int score = 0; // Variabel untuk menyimpan skor
    private int health = 5; // Variabel untuk menyimpan health

    [System.Serializable]
    public struct Question
    {
        public string questionText;
        public bool correctAnswer;
    }

    public Question[] questions;

    void Start()
    {
        if (questions.Length > 0)
        {
            DisplayQuestion();
        }

        trueButton.onClick.AddListener(() => CheckAnswer(true));
        falseButton.onClick.AddListener(() => CheckAnswer(false));

        resultText.gameObject.SetActive(false);
        UpdateScoreText();
        UpdateHealthBar();
    }

    void DisplayQuestion()
    {
        questionText.text = questions[currentQuestionIndex].questionText;
    }

    void CheckAnswer(bool playerAnswer)
    {
        if (playerAnswer == questions[currentQuestionIndex].correctAnswer)
        {
            resultText.text = "Jawaban Benar!";
            score++;
            UpdateScoreText();
        }
        else
        {
            resultText.text = "Jawaban Salah!";
            health--;
            UpdateHealthBar();
        }

        resultText.gameObject.SetActive(true);
        Invoke("NextQuestion", 2.0f);
    }

    void NextQuestion()
    {
        resultText.gameObject.SetActive(false);

        if (health <= 0)
        {
            questionText.text = "Kamu Kalah!";
            trueButton.gameObject.SetActive(false);
            falseButton.gameObject.SetActive(false);
            SaveScore();
            return;
        }

        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Length)
        {
            DisplayQuestion();
        }
        else
        {
            questionText.text = "Kuis selesai!";
            trueButton.gameObject.SetActive(false);
            falseButton.gameObject.SetActive(false);
            SaveScore();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Skor: " + score;
    }

    void UpdateHealthBar()
    {
        healthBar.value = (float)health / 5f;
    }

    void SaveScore()
    {
        string userName = PlayerPrefs.GetString("userName");
        PlayerPrefs.SetInt(userName + "_Score", score);
        SceneManager.LoadScene("FinalScore"); // Ganti dengan nama scene scoreboard Anda
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
