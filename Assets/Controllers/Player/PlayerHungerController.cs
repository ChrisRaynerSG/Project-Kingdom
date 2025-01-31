using System.Linq;
using UnityEngine;

public class PlayerHungerController : MonoBehaviour {

    float hungerRate = -0.0005f;
    float saturationRate = -0.00005f;

    PlayerHunger playerHunger;
    public void Awake(){
        this.playerHunger = new PlayerHunger();
    }

    public void Update(){
        this.playerHunger.UpdateHunger(hungerRate, saturationRate); // slowly decrease hunger and saturation over time
    }

    // basic methods for updating hunger and saturation, should eat food be here?
    public void UpdateHunger(){
    }

    public void UpdateSaturation(){

    }
    public void EatFood(InventoryItem food){

        FoodEffect foodEffect = food.Item.itemEffects.OfType<FoodEffect>().FirstOrDefault();
        if(foodEffect == null){
            Debug.LogWarning("No food effect found on item");
            return;
        }
        else{
            playerHunger.UpdateHunger(foodEffect.hunger, foodEffect.saturation);
        }
    }
}