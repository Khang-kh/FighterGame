using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerLives = 3;
    public Heart heartUI;
    public ReviveShip reviveShip; 
    public ScoreController scoreController;
    public static GameManager instance;

    public GameObject gameOverUI;
    public GameObject VictoryUI;

    public bool isVictory = false;
    public bool isGameOver = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Hàm này được gọi khi tàu bị phá hủy
    public void TakeDamage()
    {
        playerLives--;
        Debug.Log("Mạng còn lại: " + playerLives);

        if (heartUI != null)
        {
            heartUI.UpdateHearts(playerLives);
        }

        if (playerLives <= 0)
        {
            EndGame();
        }
        else
        {
            // Nếu còn mạng, yêu cầu ReviveShip hồi sinh
            if (reviveShip != null)
            {
                reviveShip.SpawnShip();
            }
        }
    }

    void EndGame()
    {
        isGameOver = true;
        Debug.Log("Game Over!");
        Time.timeScale = 0f;
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

    public void WinGame()
    {
        isVictory = true;
        Debug.Log("Bạn đã chiến thắng");

        Time.timeScale = 0f;
        if (VictoryUI != null)
        {
            VictoryUI.SetActive(true);
        }
        //Đảm bảo ẩn UI Game Over nếu nó đang hiển thị
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
        
    }

    public void ResetGame()
    {
        playerLives = 3;
        isVictory = false;
        isGameOver = false;

        Time.timeScale = 1f;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
        if (VictoryUI != null)
        {
            VictoryUI.SetActive(false);
        }

        if (heartUI != null)
        {
            heartUI.UpdateHearts(playerLives);
        }
        if (scoreController != null)
        {
            scoreController.ResetScore();
        }
        if (reviveShip != null)
        {
            
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Khi scene được tải, tìm và gán lại các instance của các script khác
        heartUI = FindObjectOfType<Heart>();
        scoreController = FindObjectOfType<ScoreController>();
        reviveShip = FindObjectOfType<ReviveShip>();

        // Tìm Canvas và các UI panel
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            Transform gameOverPanelTransform = canvas.transform.Find("GameOverPanel");
            if (gameOverPanelTransform != null)
            {
                gameOverUI = gameOverPanelTransform.gameObject;
            }

            Transform VictoryPanelTransform = canvas.transform.Find("VictoryPanel");
            if (VictoryPanelTransform != null)
            {
                VictoryUI = VictoryPanelTransform.gameObject;
            }
        } 
        ResetGame();
    }
}