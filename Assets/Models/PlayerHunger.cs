using System;
using UnityEngine;

public class PlayerHunger {

    public float currentHunger;
    public float maxHunger;
    float hungerDecayRate;
    float hungerDecayTimer;
    float hungerDecayInterval;
    public float currentSaturation;
    public float maxSaturation;
    public float saturationDecayRate;
    float saturationDecayTimer;
    float saturationDecayInterval;

    public static event Action onHungerZero;
    public static event Action<PlayerHunger> onHungerUpdated;
    public static event Action onHunger100;
    public static event Action onHunger75;
    public static event Action onHunger50;
    public static event Action onHunger25;


    public PlayerHunger(){
        this.currentHunger = 100;
        this.maxHunger = 100;
        this.hungerDecayRate = 1;
        this.hungerDecayTimer = 0;
        this.hungerDecayInterval = 1;
        this.currentSaturation = 0;
        this.maxSaturation = 100;
        this.saturationDecayRate = 1;
        this.saturationDecayTimer = 0;
        this.saturationDecayInterval = 1;
        PlayerMovementController.playerMoving += UpdateSaturationRateOnMovement;
        PlayerMovementController.playerNotMoving += UpdateSaturationRateOnStopMovement;
    }

    public void UpdateHunger(float hungerAmount, float saturationAmount){
        //if hunger is ticking down and player has saturation, use saturation to offset hunger
        if(this.currentSaturation > 0 && hungerAmount < 0 && saturationAmount <0){
            this.currentSaturation += saturationAmount;
            if(this.currentSaturation > this.maxSaturation){
                this.currentSaturation = this.maxSaturation;
            }
            if(this.currentSaturation < 0){
                this.currentSaturation = 0;
            }

        }
        //if hunger is ticking down and player has no saturation, use hunger to offset hunger
        else if(this.currentSaturation == 0 && hungerAmount < 0 && saturationAmount < 0){
            this.currentHunger += hungerAmount;
            if(this.currentHunger == 75){
                onHunger75?.Invoke();
            }
            if(this.currentHunger == 50){
                onHunger50?.Invoke();
            }
            if(this.currentHunger == 25){
                onHunger25?.Invoke();
            }
            if(this.currentHunger < 0){
                this.currentHunger = 0;
                onHungerZero?.Invoke();
            }
        }
        //if hunger is ticking up, add to hunger and saturation
        else{
            this.currentHunger += hungerAmount;
            this.currentSaturation += saturationAmount;
            if(this.currentHunger > this.maxHunger){
                this.currentHunger = this.maxHunger;
                onHunger100?.Invoke();
            }
            if(this.currentSaturation > this.maxSaturation){
                this.currentSaturation = this.maxSaturation;
            }
        }
        onHungerUpdated?.Invoke(this);
    }
    private void UpdateSaturationRateOnMovement(){
        saturationDecayRate = -0.0005f;
    }
    private void UpdateSaturationRateOnStopMovement(){
        saturationDecayRate = -0.00005f;
    }
}