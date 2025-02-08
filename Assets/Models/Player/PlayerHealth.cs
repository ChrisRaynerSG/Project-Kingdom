/// <summary>
/// PlayerHealth class is responsible for storing the player's health and health regeneration properties.
/// </summary>
using System;
public class PlayerHealth{

    public float currentHealth;
    public float maxHealth;
    public float healthRegenRate;
    public float healthRegenTimer;
    public float healthRegenInterval;

    public static event Action<PlayerHealth> onHealthUpdated;
    public static event Action<PlayerHealth> onPlayerDeath;

    public PlayerHealth(){
        this.currentHealth = 100;
        this.maxHealth = 100;
        this.healthRegenRate = 0.00005f;
        this.healthRegenTimer = 0;
        this.healthRegenInterval = 1;
    }

    public PlayerHealth(float maxHealth, float healthRegenRate){
        this.currentHealth = maxHealth;
        this.maxHealth = maxHealth;
        this.healthRegenRate = healthRegenRate;
        this.healthRegenTimer = 0;
        this.healthRegenInterval = 1;
    }

    public void UpdateHealth(float health){
        this.currentHealth += health;
        if(this.currentHealth > this.maxHealth){
            this.currentHealth = this.maxHealth;
        }
        if(this.currentHealth < 0){
            this.currentHealth = 0;
            onPlayerDeath?.Invoke(this);
        }
        onHealthUpdated?.Invoke(this);
    }
}