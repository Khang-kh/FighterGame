using UnityEngine;
using System.Collections;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private float Speed;
   
    // Biến mới để điều chỉnh khoảng cách tự hủy trong Inspector
    [SerializeField] private float distanceToDestroy = 20f;

    void Update()
    {
        // Viên đạn di chuyển về phía trước
        transform.Translate(Vector3.down * Time.deltaTime * Speed);

        // Gọi hàm kiểm tra khoảng cách
        CheckAndDestroy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Ship"))
        {
            Destroy(gameObject);
        }
    }

    // Hàm kiểm tra và hủy đối tượng nếu quá xa
    private void CheckAndDestroy()
    {
        Vector3 centerScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));

        // Tính toán khoảng cách từ viên đạn đến tâm màn hình
        if (Vector3.Distance(centerScreen, transform.position) > distanceToDestroy)
        {
            // Hủy đối tượng ngay lập tức nếu khoảng cách vượt quá giới hạn
            Destroy(gameObject);
        }
    }
}
