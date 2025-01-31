public class PlayerHealth{

    float currentHealth;
    float maxHealth;
    float healthRegenRate;
    float healthRegenTimer;
    float healthRegenInterval;

    public PlayerHealth(){
        this.currentHealth = 100;
        this.maxHealth = 100;
        this.healthRegenRate = 1;
        this.healthRegenTimer = 0;
        this.healthRegenInterval = 1;
    }
}