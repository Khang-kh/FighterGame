using UnityEngine;
using System.Collections;

public class ReviveShip : MonoBehaviour
{
    public static ReviveShip Instance;
    public GameObject ShipPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Hàm này được gọi từ GameManager để hồi sinh tàu
    public void SpawnShip()
    {
        var startPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, -0.2f, 0));
        startPoint.z = 0;

        if (ShipPrefab == null)
        {
            Debug.LogError("ShipPrefab chưa được gán trong Inspector!");
            return;
        }

        var newShip = Instantiate(ShipPrefab, startPoint, Quaternion.identity);

        // Debug tên object spawn ra
        Debug.Log("Spawned new ship: " + newShip.name + " tại vị trí: " + startPoint);

        // Kiểm tra có script Ship.cs không
        var shipScript = newShip.GetComponent<Ship>();
        if (shipScript == null)
        {
            Debug.LogError("Prefab này KHÔNG có script Ship.cs → không điều khiển được!");
        }
        else
        {
            Debug.Log("Prefab có script Ship.cs → input sẽ hoạt động.");
            shipScript.ActivateShield();
        }

        // Kiểm tra Rigidbody2D
        if (newShip.GetComponent<Rigidbody2D>() == null)
        {
            Debug.LogWarning("ShipPrefab không có Rigidbody2D (nếu bạn dùng physics/trigger có thể lỗi).");
        }

        // Kiểm tra Collider2D
        if (newShip.GetComponent<Collider2D>() == null)
        {
            Debug.LogWarning("ShipPrefab không có Collider2D (va chạm sẽ không hoạt động).");
        }

        var endPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.2f, 0));
        endPoint.z = 0;

        Debug.Log("Bắt đầu coroutine MoveShipToPoint từ " + startPoint + " đến " + endPoint);

        StartCoroutine(MoveShipToPoint(newShip, endPoint, 0.5f));
    }

    private IEnumerator MoveShipToPoint(GameObject player, Vector3 point, float duration)
    {
        float elapsedTime = 0;
        Vector3 startPosition = player.transform.position;

        while (elapsedTime < duration)
        {
            if (player == null)
            {
                yield break;
            }

            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            player.transform.position = Vector3.Lerp(startPosition, point, t);
            yield return null;
        }

        if (player != null)
        {
            player.transform.position = point;
        }
    }

    public void ResetReviveShip()
    {

    }    
}