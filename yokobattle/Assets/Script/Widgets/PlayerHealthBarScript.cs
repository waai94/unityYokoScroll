using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthBarScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Scrollbar healthBar;
    GameObject player;
    HealthManager healthManager;
    void Start()
    {
        healthBar = GetComponent<Scrollbar>();
        player = GameObject.FindWithTag("Player");
        if (!player)
        {
            Debug.LogError("PlayerHealthBarScript: Player object not found!");
        }
        healthManager = player.GetComponent<HealthManager>();
        if (!healthManager)
        {
            Debug.LogError("PlayerHealthBarScript: HealthManager component not found on player!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update the health bar's value based on the player's health
        if(healthManager && healthBar)
        {
            float healthPercentage = healthManager.GetHealthPercentage();
            healthBar.size = healthPercentage;
        }
    }
}
