using UnityEngine;
using UnityEngine.UI;
public class EnemyHe : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Scrollbar healthBar;
    GameObject myEnemy;
    HealthManager healthManager;
    void Start()
    {
        healthBar = GetComponent<Scrollbar>();
        myEnemy = GameObject.FindWithTag("Enemy");
        if (!myEnemy)
        {
         
        }
        healthManager = myEnemy.GetComponent<HealthManager>();
        if (!healthManager)
        {
            healthManager = myEnemy.GetComponentInChildren<HealthManager>();//In case HealthManager is in a child object
            if (!healthManager)
            {
                healthManager = myEnemy.GetComponentInParent<HealthManager>();//In case HealthManager is in a parent object
            }
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
