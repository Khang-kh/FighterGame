using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private GameObject[] BulletList;
    [SerializeField] private int CurrentTierBullet;
    [SerializeField] private int score;
    [SerializeField] private GameObject CoinPrefaps;
    [SerializeField] private GameObject ShieldPrefaps;
    private GameObject currentShield;

    void Start()
    {
        ActivateShield();
    }

    void Update()
    {
        Move();
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Nút bắn đang hoạt động");
            Fire();
        }
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(x, y, 0);
        transform.Translate(direction.normalized * Speed * Time.deltaTime);
        Vector3 TopLeftPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, -TopLeftPoint.x, TopLeftPoint.x),
          Mathf.Clamp(transform.position.y, -TopLeftPoint.y, TopLeftPoint.y)
        );
    }

    void Fire()
    {
        Instantiate(BulletList[CurrentTierBullet], transform.position, Quaternion.identity);
    }

    public void Fire2()
    {
        Instantiate(BulletList[CurrentTierBullet], transform.position, Quaternion.identity);
    }

    IEnumerator DisableSheld()
    {
        yield return new WaitForSeconds(5f);
        
        if (currentShield != null)
        {
            currentShield.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Va chạm với: " + collision.gameObject.name + " có Tag: " + collision.tag);
        // Gộp tất cả các tag gây sát thương vào một biến logic duy nhất
        bool isDamagingObject = collision.CompareTag("Enemy1") || collision.CompareTag("Enemy2") || collision.CompareTag("Enemy3") ||
      collision.CompareTag("Boss") || collision.CompareTag("BulletEnemy");
        // Logic chính để xử lý va chạm với kẻ thù và đạn
        if (isDamagingObject)
        {
            if (currentShield != null && currentShield.activeSelf)
            {
                Destroy(collision.gameObject);
                Debug.Log("Khiên đã bảo vệ tàu khỏi bị tấn công!");
            }
            else
            {
                // Nếu không có khiên hoặc khiên không hoạt động, tàu sẽ bị tiêu diệt
                if (GameManager.instance != null)
                {
                    GameManager.instance.TakeDamage();
                }
               Destroy(gameObject);
            }
        }

        // Logic riêng biệt cho va chạm với Coin
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            ScoreController.Instance.GetScore(1);
        }

    }

    public void ActivateShield()
    {
        if (currentShield == null)
        {
            currentShield = Instantiate(ShieldPrefaps, transform.position, Quaternion.identity, transform);
        }
        currentShield.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(DisableSheld());
    }
}