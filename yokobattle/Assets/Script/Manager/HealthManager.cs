using UnityEngine;

public class HealthManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }


    [SerializeField] private float invincibilityDuration = 0f;
    private float invincibilityTimer = 0f;

    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }

    public delegate void DeathEventHandler();
    public event DeathEventHandler OnDeath;

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health at the start
    }

    // Update is called once per frame
    void Update()
    {
        invincibilityTimer -= Time.deltaTime;

    }

    public void TakeDamage(float damage)
    {
        if(invincibilityTimer > 0 ) return; //無敵時間中はダメージを受けない
        invincibilityTimer = invincibilityDuration; //無敵時間をリセット
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            //死亡処理を送りたい
            Debug.Log("Player has died");
            OnDeath?.Invoke();
        }
        // Optionally, you can add logic here for when health reaches zero
    }
}
