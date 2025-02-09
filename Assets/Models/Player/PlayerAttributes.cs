
using System;

public class PlayerAttributes {

    private int strength = 10;
    public int Strength {
        get{
            return strength;
        } 
        set{
        strength = value;
        OnPlayerAttributesChanged?.Invoke(this);
    }}

    private int dexterity = 10;
    public int Dexterity {
        get{
            return dexterity;
        } 
        set{
        dexterity = value;
        OnPlayerAttributesChanged?.Invoke(this);
    }}

    private int intelligence = 10;
    public int Intelligence {
        get{
            return intelligence;
        } 
        set{
        intelligence = value;
        OnPlayerAttributesChanged?.Invoke(this);
    }}

    private int charisma = 10;
    public int Charisma {
        get{
            return charisma;
        } 
        set{
        charisma = value;
        OnPlayerAttributesChanged?.Invoke(this);
    }}

    private int constitution = 10;
    public int Constitution {
        get{
            return constitution;
        } 
        set{
        constitution = value;
        OnPlayerAttributesChanged?.Invoke(this);
    }}

    private int wisdom = 10;
    public int Wisdom {
        get{
            return wisdom;
        } 
        set{
        wisdom = value;
        OnPlayerAttributesChanged?.Invoke(this);
    }}

    private int luck = 10;
    public int Luck {
        get{
            return luck;
        } 
        set{
        luck = value;
        OnPlayerAttributesChanged?.Invoke(this);
    }}

    private int perception = 10;
    public int Perception {
        get{
            return perception;
        } 
        set{
        perception = value;
        OnPlayerAttributesChanged?.Invoke(this);
    }}

    public event Action<PlayerAttributes> OnPlayerAttributesChanged;

    public PlayerAttributes(// all arg constructor
    int strength, 
    int dexterity, 
    int intelligence, 
    int charisma, 
    int constitution, 
    int wisdom, 
    int luck, 
    int perception){
        this.strength = strength;
        this.dexterity = dexterity;
        this.intelligence = intelligence;
        this.charisma = charisma;
        this.constitution = constitution;
        this.wisdom = wisdom;
        this.luck = luck;
        this.perception = perception;
    }

    public PlayerAttributes(){//No arg constructor
    }
}