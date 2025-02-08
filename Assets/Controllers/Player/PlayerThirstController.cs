using UnityEngine;
using System.Collections;
using System;
public class PlayerThirstController : MonoBehaviour{

    public PlayerThirst PlayerThirst { get; set; }

    public static event Action<float> DrinkProgress;

    public void Awake(){
        PlayerThirst = new PlayerThirst();
    }

    public void Update(){
        PlayerThirst.UpdateThirst(-0.0005f);
    }

    public void DrinkWater(Tile tile, PlayerMovementController playerMovementController, float thirstAmount){
        Tile closestTile = TileUtils.FindClosestNeighbourToPlayer(tile, playerMovementController);
        if(closestTile != null){
            StartCoroutine(WalkToAndDrinkWaterCoroutine(playerMovementController, tile, closestTile, thirstAmount));
        }
    }

    private IEnumerator WalkToAndDrinkWaterCoroutine(PlayerMovementController playerMovementController, Tile tile, Tile neighbourTile, float thirstAmount){

        if(Vector2.Distance(playerMovementController.playerTransform.position, new Vector2(tile.GlobalPosX, tile.GlobalPosY)) > 1f){
            playerMovementController.MoveToTile(neighbourTile);
        }

        while (Vector2.Distance(playerMovementController.playerTransform.position, new Vector2(tile.GlobalPosX, tile.GlobalPosY)) > 1.5f){
            yield return null;
        }
        
        float timeElapsed = 0f;
        float drinkDuration = 1f;
        while(timeElapsed < drinkDuration){
            timeElapsed += Time.deltaTime;
            DrinkProgress?.Invoke(timeElapsed/drinkDuration);
            yield return null;
        }
        PlayerThirst.UpdateThirst(thirstAmount);
    }
}