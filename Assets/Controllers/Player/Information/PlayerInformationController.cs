using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInformationController : MonoBehaviour
{
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI healthText;

    public TextMeshProUGUI thirstText;

    public Slider hungerSlider;
    public Slider healthSlider;
    public Slider thirstSlider;

    public GameObject playerInformationPanel;

    public Button characterSheetButton;

    public bool isPlayerInformationPanelActive = false;

    public void Awake()
    {
        PlayerHunger.onHungerUpdated += OnHungerUpdated;
        PlayerHealth.onHealthUpdated += OnHealthUpdated;
        PlayerThirst.onThirstUpdated += OnThirstUpdated;
        // maybe change this later when we have player classes and stats?
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.C)){
            if(isPlayerInformationPanelActive){
                playerInformationPanel.SetActive(false);
                isPlayerInformationPanelActive = false;
            }
            else{
                playerInformationPanel.SetActive(true);
                isPlayerInformationPanelActive = true;
            }
        }
        characterSheetButton.onClick.AddListener(OpenCharacterSheet);
    }

    public void OnHungerUpdated(PlayerHunger playerHunger)
    {
        hungerText.text = $"Hunger: {playerHunger.currentHunger:F0}/{playerHunger.maxHunger}\nSaturation: {playerHunger.currentSaturation:F0}/{playerHunger.maxSaturation}";
        hungerSlider.value = playerHunger.currentHunger / playerHunger.maxHunger;
    }

    public void OnHealthUpdated(PlayerHealth playerHealth)
    {
        healthText.text = $"Health: {playerHealth.currentHealth:F0}/{playerHealth.maxHealth}";
        healthSlider.value = playerHealth.currentHealth / playerHealth.maxHealth;
    }

    public void OnThirstUpdated(PlayerThirst playerThirst)
    {
        thirstText.text = $"Thirst: {playerThirst.currentThirst:F0}/{playerThirst.maxThirst}";
        thirstSlider.value = playerThirst.currentThirst / playerThirst.maxThirst;
    }

    public void OpenCharacterSheet(){
        if(isPlayerInformationPanelActive){
            playerInformationPanel.SetActive(false);
            isPlayerInformationPanelActive = false;
        }
        else{
            playerInformationPanel.SetActive(true);
            isPlayerInformationPanelActive = true;
        }
    }
}