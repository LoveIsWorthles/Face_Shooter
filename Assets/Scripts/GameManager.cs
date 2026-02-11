using UnityEngine;
using TMPro; // Required for TextMeshPro

public class GameManager : MonoBehaviour
{
    public float gameDuration = 180f; 
    public float elapsedTime = 0f;
    
    private bool isGameOver = false;
    
    [Header("UI References")]
    public TextMeshProUGUI timerText; // Drag your TextMeshPro object here
    public GameObject victoryScreen;

    public float DifficultyPercent => Mathf.Clamp01(elapsedTime / gameDuration);

    void Update()
    {
        if (elapsedTime < gameDuration)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            WinGame();
        }
    }

    void UpdateTimerUI()
    {
        float timeRemaining = gameDuration - elapsedTime;

        // Convert seconds into Minutes:Seconds format
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        // This format string "00" ensures 1 minute looks like "01"
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void WinGame()
    {
        isGameOver = true;
        timerText.text = "00:00";
        
        // 1. Show the victory message
        if (victoryScreen != null) victoryScreen.SetActive(true);

        // 2. Clear all existing enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // 3. Stop the spawner
        Object.FindFirstObjectByType<EnemySpawner>().enabled = false;
    }
}