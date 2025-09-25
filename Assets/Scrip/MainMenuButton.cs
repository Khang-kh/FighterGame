using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null && GameManager.instance != null)
        {
            btn.onClick.RemoveAllListeners();
            // Thêm listener mới, gọi hàm ExitToMainMenu từ GameManager
            btn.onClick.AddListener(GameManager.instance.ExitToMainMenu);
        }
    }
}