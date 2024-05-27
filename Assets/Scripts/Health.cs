using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    [HideInInspector] public int currentHealth;

    public HealthBar healthBar;
    public GameObject goldPrefab;

    private float safeTime;
    public float safeTimeDuration = 0f;
    public bool isDead = false;

    public bool camShake = false;
    public int coinCount = 2; // Số lượng xu rơi ra
    public float dropForce = 3f; // Lực đẩy của xu khi rơi ra
    private void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDam(int damage)
    {
        if (safeTime <= 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                if (this.gameObject.tag == "Enemy")
                {
                    DropGold();
                    FindObjectOfType<WeaponManager>().RemoveEnemyToFireRange(this.transform);
                    FindObjectOfType<Killed>().UpdateKilled();
                    FindObjectOfType<PlayerExp>().UpdateExperience(UnityEngine.Random.Range(1, 4));
                    Destroy(this.gameObject, 0.125f);
                }
                isDead = true;
            }

            // If player then update health bar
            if (healthBar != null)
                healthBar.UpdateHealth(currentHealth, maxHealth);

            safeTime = safeTimeDuration;
        }
    }
    private void DropGold()
    {
        if (goldPrefab != null)
        {
            for (int i = 0; i < coinCount; i++) // Vòng lặp để tạo ra nhiều xu hơn
            {
                // Tạo hiệu ứng vàng bắn ra xung quanh
                GameObject gold = Instantiate(goldPrefab, transform.position, Quaternion.identity);

                // Tính toán một hướng ngẫu nhiên
                Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized * 0.5f; // Giảm phạm vi của hướng ngẫu nhiên

                // Thêm lực vào xu
                Rigidbody2D rb = gold.GetComponent<Rigidbody2D>();
            }
        }
    }
    private void Update()
    {
        if (safeTime > 0)
        {
            safeTime -= Time.deltaTime;
        }
    }
}
