using UnityEngine;

public class PlayerDeathScriipt : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool isDead = false;
    void Start()
    {
        HealthManager healthManager = GetComponent<HealthManager>();
        if (healthManager != null)
        {
            healthManager.OnDeath += OnPlayerDeath;
        }
    }

    private void OnDisable()
    {
        HealthManager healthManager = GetComponent<HealthManager>();
        if (healthManager != null)
        {
            healthManager.OnDeath -= OnPlayerDeath;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerDeath()
    {
        if (isDead) return; // Prevent multiple death triggers
        isDead = true;
        Debug.Log("Player has died by script.");
        // Add additional logic for player death here
        GameObject gameManager = GameObject.FindWithTag(tag: "GameController");
        if (!gameManager)
        {
            Debug.LogError("GameManager not found in the scene.");
            return;
        }

        GameManagerScript gms = gameManager.GetComponent<GameManagerScript>();
        if (gms != null)
        {
            gms.OnPlayerDeath();
        }
        else
        {
            Debug.LogError("GameManagerScript component not found on GameManager.");
        }

        //ŽžŠÔ‚ð’x‚­‚·‚é
        Time.timeScale = 0.2f;
    }

    }

