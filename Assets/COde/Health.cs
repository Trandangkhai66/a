using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    [HideInInspector] public int currentHealth;

    public HealthBar healthBar;

    private float safeTime;
    public float safeTimeDuration = 0f;
    public bool isDead = false;

    public bool camShake = false;

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
        Debug.Log("Player took damage: " + damage + ", current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            if (this.gameObject.tag == "Enemy")
            {
                FindObjectOfType<Killed>().UpdateKilled();
                Destroy(this.gameObject, 0.125f);
            }
            isDead = true;
        }

        // Nếu là người chơi, cập nhật thanh máu
        if (healthBar != null)
        {
            Debug.Log("Updating health bar.");
            healthBar.UpdateHealth(currentHealth, maxHealth);
        }

        safeTime = safeTimeDuration;
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
