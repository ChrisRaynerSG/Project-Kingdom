using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerSpeechController : MonoBehaviour
{
    public GameObject speechBubble;
    public TextMeshProUGUI speechText;

    public float fadeDuration = 0.5f;

    public void Start(){

        PlayerHunger.onHunger100 += OnHunger100;
        PlayerHunger.onHunger75 += OnHunger75;
        PlayerHunger.onHunger50 += OnHunger50;
        PlayerHunger.onHunger25 += OnHunger25;
        PlayerHunger.onHungerZero += OnHungerZero;

        PlayerThirst.onThirst100 += OnThirst100;
        PlayerThirst.onThirst75 += OnThirst75;
        PlayerThirst.onThirst50 += OnThirst50;
        PlayerThirst.onThirst25 += OnThirst25;
        PlayerThirst.onThirstZero += OnThirstZero;

    }
    public void Speak(string speech)
    {

        Debug.Log($"Player says: {speech}");
        speechText.text = speech;
        speechText.gameObject.SetActive(true);
        speechBubble.SetActive(true); //is this wrong?
        DoCoroutineMethods();

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

    private void OnThirst100()
    {
        Speak("I'm hydrated!");
    }
    private void OnThirst75()
    {
        Speak("I'm getting thirsty");
    }
    private void OnThirst50()
    {
        Speak("I'm thirsty...");
    }
    private void OnThirst25()
    {
        Speak("I'm parched!");
    }
    private void OnThirstZero()
    {
        Speak("I'm dying of thirst!");
    }

    private IEnumerator FadeOutSpeech()
    {
        yield return new WaitForSeconds(2f);

        CanvasGroup canvasGroup = speechBubble.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = speechBubble.AddComponent<CanvasGroup>();
        }

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f; // Ensure it's fully transparent
        speechBubble.SetActive(false); // Optionally deactivate the bubble after fading
    }

    private IEnumerator FadeInSpeech()
    {
        CanvasGroup canvasGroup = speechBubble.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = speechBubble.AddComponent<CanvasGroup>();
        }

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f; // Ensure it's fully opaque
    }

    private void DoCoroutineMethods()
    {
        StartCoroutine(FadeInSpeech());
        StartCoroutine(FadeOutSpeech());
    }
}