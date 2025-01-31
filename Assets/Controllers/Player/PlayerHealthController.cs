using UnityEngine;

public class PlayerHealthController : MonoBehaviour{

    public PlayerHealth playerHealth;
    PlayerHungerController playerHungerController;

    public void Awake(){
        this.playerHealth = new PlayerHealth();
        // maybe change this later when we have player classes and stats?
    }
    public void Start(){
        playerHungerController = GameObject.Find("Player").GetComponent<PlayerHungerController>();
    }

    public void Update(){
        if(playerHungerController.playerHunger.currentHunger <= 0){
            this.playerHealth.UpdateHealth(-playerHealth.healthRegenRate);
        }
        else{
            this.playerHealth.UpdateHealth(playerHealth.healthRegenRate);
        }
        
    }
}