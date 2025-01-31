using UnityEngine;

public class PlayerHealthController : MonoBehaviour{

    PlayerHealth playerHealth;

    public void Awake(){
        this.playerHealth = new PlayerHealth();
        // maybe change this later when we have player classes and stats?
    }

}