using TMPro;
using UnityEngine;

public class PlayerSpeechController : MonoBehaviour
{
    public GameObject speechBubble;
    public TextMeshProUGUI speechText;

    public void Start(){

        PlayerHunger.onHunger100 += OnHunger100;
        PlayerHunger.onHunger75 += OnHunger75;
        PlayerHunger.onHunger50 += OnHunger50;
        PlayerHunger.onHunger25 += OnHunger25;
        PlayerHunger.onHungerZero += OnHungerZero;

    }
    public void Speak(string speech)
    {
        Debug.Log($"Player says: {speech}");
        speechText.text = speech;
        speechText.gameObject.SetActive(true);
        speechBubble.SetActive(true); //is this wrong?
        speechText.text = speech;
    }

    private void OnHunger100()
    {
        Speak("I'm full!");
    }
    private void OnHunger75()
    {
        Speak("I'm getting peckish");
    }
    private void OnHunger50()
    {
        Speak("I'm hungry...");
    }
    private void OnHunger25()
    {
        Speak("I'm starving!");
    }
    private void OnHungerZero()
    {
        Speak("I'm dying of hunger!");
    }
}