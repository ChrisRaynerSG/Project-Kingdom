using System.Data.Common;
using System.Linq;
using UnityEngine;

public class PlayerHungerController : MonoBehaviour {

    float hungerRate = -0.05f;
    // float saturationRate = -0.00005f;

    public PlayerHunger playerHunger;
    public void Awake(){
        this.playerHunger = new PlayerHunger();
    }

    public void Update(){
        this.playerHunger.UpdateHunger(hungerRate, playerHunger.saturationDecayRate); // slowly decrease hunger and saturation over time
    }

    // basic methods for updating hunger and saturation, should eat food be here?
    public void UpdateHunger(){
    }

    public void UpdateSaturation(){

    }
    public void EatFood(InventoryItem food){

        PlayerHealthController playerHealthController = GameObject.Find("Player").GetComponent<PlayerHealthController>();
        PlayerHealth playerHealth = playerHealthController.playerHealth;
        FoodEffect foodEffect = food.Item.itemEffects.OfType<FoodEffect>().FirstOrDefault();
        if(foodEffect == null){
            Debug.LogWarning("No food effect found on item");
            return;
        }
        else{
            playerHunger.UpdateHunger(foodEffect.hunger, foodEffect.saturation);
            playerHealth.UpdateHealth(foodEffect.health);
        }
    }
}