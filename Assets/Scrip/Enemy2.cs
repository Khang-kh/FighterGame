using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour
{
    [SerializeField] private GameObject BulletEnemyPrefab;
    [SerializeField] private int health = 150;
    [SerializeField] private GameObject CoinPrefab;

    public static Enemy2 instance;

    // Các biến cho hiệu ứng nghiêng máy bay
    [SerializeField] private float maxTiltAngle = 30f; // Góc nghiêng tối đa
    [SerializeField] private float tiltSpeed = 2f; // Tốc độ nghiêng
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
        StartCoroutine(MoveEnemy2ToRandomPoint());
    }

    void Update()
    {
        // Làm mịn chuyển động xoay
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);
    }

    public void PutDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy2 bị sát thương, máu còn lại: " + health);
        if (health <= 0)
        {
            Debug.Log("Enemy2 bị tiêu diệt!");
            if (CoinPrefab != null)
            {
                Instantiate(CoinPrefab, transform.position, Quaternion.identity);
            }

            // Thông báo cho WaveManager khi kẻ thù bị tiêu diệt
            if (WaveManager.Instance != null)
            {
                WaveManager.Instance.EnemyDefeated("Enemy2");
            }

            // Phá hủy đối tượng kẻ thù
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnBulletEnemy()
    {
        while (true)
        {
            Vector3 spawnPos = transform.position + Vector3.down * 1.6f; // lệch xuống dưới enemy
            Instantiate(BulletEnemyPrefab, spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0.7f, 0.9f));
        }
    }

    IEnumerator MoveEnemy2ToRandomPoint()
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
                transform.position = Vector3.MoveTowards(transform.position, targetPoint, 5f * Time.deltaTime);
                yield return null;
            }

            // Khi đến điểm đích, quay về góc 0
            targetRotation = Quaternion.Euler(0, 0, 0);
            yield return new WaitForSeconds(0.5f); // Chờ một chút trước khi tìm điểm mới
        }
    }

    Vector3 getRandomPoint()
    {
        Vector3 posRandom = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0.5f, 1)));
        posRandom.z = 0;
        return posRandom;
    }
}
