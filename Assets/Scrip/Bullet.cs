using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private int damage = 30;

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * Speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra va chạm với đối tượng có tag "Enemy1", "Enemy2" và "Enemy3" và "Boss"
        if ((collision.CompareTag("Enemy1")) || collision.CompareTag("Enemy2") || collision.CompareTag("Enemy3") ||
            collision.CompareTag("Boss"))
        {
            if (collision.CompareTag("Enemy1"))
            {
                Enemy1 enemy1 = collision.GetComponent<Enemy1>();
                if (enemy1 != null)
                {
                    enemy1.PutDamage(damage);
                }
                Destroy(gameObject);
            }
            if (collision.CompareTag("Enemy2"))
            {
                Enemy2 enemy2 = collision.GetComponent<Enemy2>();
                if (enemy2 != null)
                {
                    enemy2.PutDamage(damage);
                }
                Destroy(gameObject);
            }
            if (collision.CompareTag("Enemy3"))
            {
                Enemy3 enemy3 = collision.GetComponent<Enemy3>();
                if (enemy3 != null)
                {
                    enemy3.PutDamage(damage);
                }
                Destroy(gameObject);
            }
            else if (collision.CompareTag("Boss"))
            {
                Boss boss = collision.GetComponent<Boss>();
                if (boss != null)
                {
                    boss.PutDamage(damage);
                }
                Destroy(gameObject);
            }
        }

    }
}

