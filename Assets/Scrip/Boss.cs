using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject BulletEnemyPrefab;
    [SerializeField] private int health = 300;
    [SerializeField] private GameObject CoinPrefab;

    public static Boss instance;

    // Các biến cho hiệu ứng nghiêng máy bay của Boss
    // Boss thường nghiêng ít hơn hoặc chậm hơn để trông nặng nề hơn
    [SerializeField] private float maxTiltAngle = 15f; // Giảm góc nghiêng tối đa
    [SerializeField] private float tiltSpeed = 1.5f;   // Giảm tốc độ nghiêng
    private Quaternion targetRotation;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Khởi tạo góc quay mục tiêu ban đầu
        targetRotation = transform.rotation;
        StartCoroutine(SpawnBulletEnemy());
        StartCoroutine(MoveBossToRandomPoint());
    }

    void Update()
    {
        // Làm mịn chuyển động xoay
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);
    }

    public void PutDamage(int damage)
    {
        health -= damage;
        Debug.Log("Boss bị sát thương, máu còn lại: " + health);
        if (health <= 0)
        {
            Debug.Log("Boss bị tiêu diệt!");
            if (CoinPrefab != null)
            {
                Instantiate(CoinPrefab, transform.position, Quaternion.identity);
            }
            if (WaveManager.Instance != null)
            {
                WaveManager.Instance.EnemyDefeated("Boss");
            }
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnBulletEnemy()
    {
        while (true)
        {
            // Vị trí đạn đã được điều chỉnh lên 1.6f, có thể tốt hơn để tránh lỗi va chạm
            Vector3 spawnPos = transform.position + Vector3.down * 1.6f;
            Instantiate(BulletEnemyPrefab, spawnPos, Quaternion.identity);

            // Tốc độ bắn cực nhanh (0.0f - 0.1f)
            yield return new WaitForSeconds(Random.Range(0.0f, 0.1f));
        }
    }

    IEnumerator MoveBossToRandomPoint()
    {
        while (true)
        {
            Vector3 targetPoint = getRandomPoint();
            Vector3 startPoint = transform.position;
            Vector3 direction = (targetPoint - startPoint).normalized;

            // Tính toán góc nghiêng dựa trên hướng di chuyển
            float tiltAngle = -direction.x * maxTiltAngle;
            targetRotation = Quaternion.Euler(0, 0, tiltAngle);

            while (Vector3.Distance(transform.position, targetPoint) > 0.01f)
            {
                // Giữ nguyên tốc độ di chuyển
                transform.position = Vector3.MoveTowards(transform.position, targetPoint, 5f * Time.deltaTime);
                yield return null;
            }

            // Khi đến điểm đích, quay về góc 0
            targetRotation = Quaternion.Euler(0, 0, 0);
            yield return new WaitForSeconds(1.0f); // Boss chờ lâu hơn một chút (1.0f)
        }
    }

    Vector3 getRandomPoint()
    {
        Vector3 posRandom = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0.5f, 0.8f))); // Giới hạn vùng bay thấp hơn cho Boss
        posRandom.z = 0;
        return posRandom;
    }
}

