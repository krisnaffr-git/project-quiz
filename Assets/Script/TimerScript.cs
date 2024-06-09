using UnityEngine;
using TMPro; // Tambahkan ini untuk menggunakan TextMesh Pro
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    // Waktu countdown dalam detik (2 menit = 120 detik)
    public float countdownTime = 120.0f;
    // Referensi ke komponen TextMeshProUGUI
    public TextMeshProUGUI countdownText;

    // Update dipanggil sekali per frame
    void Update()
    {
        // Kurangi waktu countdown
        countdownTime -= Time.deltaTime;

        // Hitung menit dan detik dari countdownTime
        int minutes = Mathf.FloorToInt(countdownTime / 60F);
        int seconds = Mathf.FloorToInt(countdownTime % 60F);

        // Tampilkan waktu pada teks
        countdownText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

        // Jika waktu countdown habis, pindah ke halaman lain
        if (countdownTime <= 0)
        {
            LoadNextScene();
        }
    }

    // Fungsi untuk memuat halaman berikutnya
    void LoadNextScene()
    {
        // Ganti "NextSceneName" dengan nama scene yang ingin Anda muat
        SceneManager.LoadScene("FinalScore");
    }
}
