using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 2f; // Tốc độ cuộn của background
    [SerializeField] private float restartPosition = -10f; // Vị trí Y mà background sẽ được reset
    [SerializeField] private float startPosition = 10f; // Vị trí Y ban đầu của background khi reset
    
    void Start()
    {
        // Để đảm bảo camera chính đã được khởi tạo
        if (Camera.main == null)
        {
            Debug.LogError("Main Camera not found! Please ensure your Camera has the 'MainCamera' tag.");
            enabled = false; // Tắt script nếu không tìm thấy Camera
            return;
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            float bgHeight = sr.bounds.size.y;
            
        }
    }

    void Update()
    {

        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

        // Kiểm tra nếu background đã đi quá giới hạn dưới màn hình
        if (transform.position.y < restartPosition)
        {
            // Di chuyển background trở lại vị trí phía trên
            transform.position = new Vector3(transform.position.x, startPosition, transform.position.z);
        }
    }
}