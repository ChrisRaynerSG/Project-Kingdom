using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHungerController : MonoBehaviour {

    public Slider eatingSlider;

    float hungerRate = -0.001f;
    private bool isEating = false;
    // float saturationRate = -0.00005f;

    public static event Action<float> EatTimeProgress;

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

    public void DoCoroutineMethods(Tile tile, PlayerMovementController playerMovementController){
        Tile closestTile = TileUtils.FindClosestNeighbourToPlayer(tile, playerMovementController);
        if(closestTile != null){
            StartCoroutine(MoveToFoodCoroutine(tile, closestTile, playerMovementController));
        }
        // StartCoroutine(MoveToFoodCoroutine(tile, playerMovementController));
    }
    private IEnumerator MoveToFoodCoroutine(Tile tile, Tile closestTile, PlayerMovementController playerMovementController){

        // Vector2 targetPosition = new Vector2(tile.GlobalPosX, tile.GlobalPosY);
        //float distance = Vector2.Distance(playerMovementController.playerTransform.position, new Vector2(contextMenuController.tile.GlobalPosX, contextMenuController.tile.GlobalPosY));
        Debug.Log("Moving to food");
        if(Vector2.Distance(playerMovementController.playerTransform.position, new Vector2(tile.GlobalPosX, tile.GlobalPosY)) > 1f){
            playerMovementController.MoveToTile(closestTile);
        }
        while (Vector2.Distance(playerMovementController.playerTransform.position, new Vector2(tile.GlobalPosX, tile.GlobalPosY)) > 1.5f)
            {
                //Debug.Log("Still moving to food... Current Distance: " + Vector2.Distance(playerMovementController.playerTransform.position, targetPosition));
                yield return null;// Wait for the next frame
            }
        StartCoroutine(EatFoodCoroutine(tile, playerMovementController));
    }

    private IEnumerator EatFoodCoroutine(Tile tile, PlayerMovementController playerMovementController){
        if(isEating){
            yield break;
        }
        isEating = true;
        Debug.Log("Player is close enough to eat");
        //then eat food
        FoodEffect foodEffect = tile.inventoryItem.Item.itemEffects.OfType<FoodEffect>().FirstOrDefault();
        Debug.Log($"Starting to eat for {foodEffect.eatTime} seconds.");

        float time = 0f;
        float originalSpeed = playerMovementController.playerMovementData.MovementSpeed;
        playerMovementController.playerMovementData.MovementSpeed = 0;

        while(time < foodEffect.eatTime){
          
            eatingSlider.gameObject.SetActive(true);
            time += Time.deltaTime;
            Debug.Log($"Eating: {tile.inventoryItem.Item.name}\nAdding hunger: {foodEffect.hunger}\nAdding saturation: {foodEffect.saturation}");
            eatingSlider.value = time / foodEffect.eatTime; // update progress bar
            yield return null;
        }
        eatingSlider.gameObject.SetActive(false); // add coroutine to fade out 
        Debug.Log("Finished eating");
        EatFood(tile.inventoryItem);
        playerMovementController.playerMovementData.MovementSpeed = originalSpeed;
        tile.inventoryItem.RemoveQuantity(1);
        isEating = false;
    }


}