using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public TMP_Text healthText;
    private PlayerMovement movement;
    private bool isDead;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level 1")
        {
            ResetPlayer();
        }
    }

    void OnEnable()
    {
        isDead = false;
        health = maxHealth;
        UpdateHealthText();
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + health;
        }
    }

    public void TakeDamage(int value)
    {
        if (movement != null && movement.IsInvincible) return;
        health -= value;
        UpdateHealthText();

        if (health <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    public void Heal(int value)
    {
        health = Mathf.Min(health + value, maxHealth);
        UpdateHealthText();
    }

    public void Die()
    {
        gameObject.SetActive(false);
        GameManager.Instance.GetComponent<MenuManager>().gameOver();
    }

    public void ResetPlayer()
    {
        isDead = false;
        health = maxHealth;
        gameObject.SetActive(true);
        UpdateHealthText();
    }
}