using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] private GameObject enemy1Prefab;
    [SerializeField] private GameObject enemy2Prefab;
    [SerializeField] private GameObject enemy3Prefab;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform enemyParent;

    public int enemiesRemaining;
    public int currentWave = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Bắt đầu chuỗi quản lý các đợt sóng
        StartCoroutine(ManageWaves());
    }

    // Hàm này được gọi khi một kẻ địch bị tiêu diệt
    public void EnemyDefeated(string enemyTag)
    {
        // Kiểm tra xem kẻ địch bị tiêu diệt có phải là Boss không
        if (enemyTag == "Boss")
        {
            // Nếu đúng là Boss, gọi hàm WinGame() và dừng game
            GameManager.instance.WinGame();
            return; // Dừng hàm tại đây, không cần làm gì thêm
        }

        enemiesRemaining--;

        // Kiểm tra xem đợt sóng đã kết thúc chưa
        if (enemiesRemaining <= 0)
        {
            Debug.Log($"Đợt {currentWave} kết thúc. Đã hoàn thành.");
            // Coroutine ManageWaves sẽ tự động xử lý đợt tiếp theo
        }
    }

    IEnumerator ManageWaves()
    {
        // Đợt 1
        Debug.Log("Đợt 1: Bắt đầu.");
        enemiesRemaining = 2;
        yield return StartCoroutine(SpawnEnemy1Wave(enemiesRemaining));
        yield return new WaitUntil(() => enemiesRemaining <= 0);
        yield return new WaitForSeconds(2f);

        // Đợt 2
        Debug.Log("Đợt 2: Bắt đầu.");
        enemiesRemaining = 3;
        yield return StartCoroutine(SpawnEnemy2Wave(enemiesRemaining));
        yield return new WaitUntil(() => enemiesRemaining <= 0);
        yield return new WaitForSeconds(2f);

        // Đợt 3
        Debug.Log("Đợt 3: Bắt đầu.");
        enemiesRemaining = 3;
        yield return StartCoroutine(SpawnEnemy3Wave(enemiesRemaining));
        yield return new WaitUntil(() => enemiesRemaining <= 0);
        yield return new WaitForSeconds(2f);

        // Đợt Boss
        Debug.Log("Đợt Boss: Bắt đầu.");
        // Gán số lượng kẻ thù còn lại là 1 (chính là boss)
        enemiesRemaining = 1;
        yield return StartCoroutine(SpawnBossWave());
    }

    IEnumerator SpawnEnemy1Wave(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), 1.1f, 0));
            GameObject enemy1 = Instantiate(enemy1Prefab, spawnPos, Quaternion.identity, enemyParent);
            enemy1.tag = "Enemy1";
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator SpawnEnemy2Wave(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), 1.1f, 0));
            GameObject enemy2 = Instantiate(enemy2Prefab, spawnPos, Quaternion.identity, enemyParent);
            enemy2.tag = "Enemy2";
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator SpawnEnemy3Wave(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), 1.1f, 0));
            GameObject enemy3 = Instantiate(enemy3Prefab, spawnPos, Quaternion.identity, enemyParent);
            enemy3.tag = "Enemy3";
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator SpawnBossWave()
    {
        Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1.1f, 0));
        GameObject boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity, enemyParent);
        boss.tag = "Boss";
        yield return null;
    }
}
