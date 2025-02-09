using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerAttributesController : MonoBehaviour{

    [HideInInspector]public static PlayerAttributesController Instance {get; set;}
    private PlayerAttributes playerAttributes;
    public PlayerAttributes PlayerAttributes{get{return playerAttributes;}}

    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI dexterityText;
    public TextMeshProUGUI constitutionText;
    public TextMeshProUGUI intelligenceText;
    public TextMeshProUGUI wisdomText;
    public TextMeshProUGUI charismaText;
    public TextMeshProUGUI luckText;
    public TextMeshProUGUI perceptionText;


    public void Awake(){
        playerAttributes = new PlayerAttributes();
        Instance = this;
        playerAttributes.OnPlayerAttributesChanged += UpdateAttributesText;
    }

    public void Start(){// update this later on to import player attributes from character creation, currently just creates player attributes all at 10.
        strengthText.text = playerAttributes.Strength.ToString();
        dexterityText.text = playerAttributes.Dexterity.ToString();
        constitutionText.text = playerAttributes.Constitution.ToString();
        intelligenceText.text = playerAttributes.Intelligence.ToString();
        wisdomText.text = playerAttributes.Wisdom.ToString();
        charismaText.text = playerAttributes.Charisma.ToString();
        luckText.text = playerAttributes.Luck.ToString();
        perceptionText.text = playerAttributes.Perception.ToString();
    }

    
    public void UpdateAttributesText(PlayerAttributes playerAttributes){
        strengthText.text = playerAttributes.Strength.ToString();
        dexterityText.text = playerAttributes.Dexterity.ToString();
        constitutionText.text = playerAttributes.Constitution.ToString();
        intelligenceText.text = playerAttributes.Intelligence.ToString();
        wisdomText.text = playerAttributes.Wisdom.ToString();
        charismaText.text = playerAttributes.Charisma.ToString();
        luckText.text = playerAttributes.Luck.ToString();
        perceptionText.text = playerAttributes.Perception.ToString();
    }

}