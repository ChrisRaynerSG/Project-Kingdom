using TMPro;
using UnityEngine;

public class PlayerInformationController : MonoBehaviour
{
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI healthText;

    public void Awake()
    {
        PlayerHunger.onHungerUpdated += OnHungerUpdated;
        PlayerHealth.onHealthUpdated += OnHealthUpdated;
        // maybe change this later when we have player classes and stats?
    }

    public void OnHungerUpdated(PlayerHunger playerHunger)
    {
        hungerText.text = $"Hunger: {playerHunger.currentHunger:F0}/{playerHunger.maxHunger}\nSaturation: {playerHunger.currentSaturation:F0}/{playerHunger.maxSaturation}"; 
    }

    public void OnHealthUpdated(PlayerHealth playerHealth)
    {
        healthText.text = $"Health: {playerHealth.currentHealth:F0}/{playerHealth.maxHealth}";
    }
}