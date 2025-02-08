using System;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEditor;
public class PlayerThirst{
    public float maxThirst = 100;
    public float currentThirst = 100;
    public string playerThirstState = "init";
    public static event Action onThirstZero;
    public static event Action onThirst25;
    public static event Action onThirst50;
    public static event Action onThirst75;
    public static event Action onThirst100;
    public static event Action<PlayerThirst> onThirstUpdated;

    public void UpdateThirst(float thirstAmount){
        this.currentThirst += thirstAmount;
        if(this.currentThirst >= this.maxThirst){
            this.currentThirst = this.maxThirst;
            if(playerThirstState != "Hydrated"){
                playerThirstState = "Hydrated";
                onThirst100?.Invoke();
            }
        }

        if(this.currentThirst <= 0){
            this.currentThirst = 0;
            if(playerThirstState != "dying"){
                playerThirstState = "dying";
                onThirstZero?.Invoke();
            }
        }
        else if(this.currentThirst <= 25){
            if(playerThirstState != "parched"){
                playerThirstState = "parched";
                onThirst25?.Invoke();
            }
        }
        else if(this.currentThirst <= 50){
            if(playerThirstState != "thirsty"){
                playerThirstState = "thirsty";
                onThirst50?.Invoke();
            }
        }
        else if(this.currentThirst <= 75){
            if(playerThirstState != "satisfied"){
                playerThirstState = "satisfied";
                onThirst75?.Invoke();
            }
        }
        else if(this.currentThirst > 75 && playerThirstState != "init"){
            playerThirstState = "init";
        }
        onThirstUpdated?.Invoke(this);
    }
}