using TMPro;
using UnityEngine;

public class PlayerInformationController : MonoBehaviour
{
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI healthText;

    public void Awake()
    {
        PlayerHunger.onHungerUpdated += OnHungerUpdated;
        // maybe change this later when we have player classes and stats?
    }

    public void OnHungerUpdated(PlayerHunger playerHunger)
    {
        hungerText.text = $"Hunger: {playerHunger.currentHunger:F2}/{playerHunger.maxHunger}\nSaturation: {playerHunger.currentSaturation:F2}/{playerHunger.maxSaturation}"; 
    }
}