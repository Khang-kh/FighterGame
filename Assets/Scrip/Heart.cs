using UnityEngine;

public class Heart : MonoBehaviour
{    
    public GameObject[] hearts;
    public void UpdateHearts(int currentLives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLives)
            {
                hearts[i].SetActive(true); // Hiển thị trái tim nếu còn mạng
            }
            else
            {
                hearts[i].SetActive(false); // Ẩn trái tim nếu mất mạng
            }
        }
    }
}
