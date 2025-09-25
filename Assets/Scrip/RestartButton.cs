using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    void Start()
    {
        // Lấy thành phần Button của đối tượng này
        Button btn = GetComponent<Button>();
        if (btn != null && GameManager.instance != null)
        {
            // Xóa tất cả các listener cũ để tránh bị gọi nhiều lần
            btn.onClick.RemoveAllListeners();
            // Thêm listener mới, gọi hàm RestartGame từ GameManager
            btn.onClick.AddListener(GameManager.instance.RestartGame);
        }
    }
}