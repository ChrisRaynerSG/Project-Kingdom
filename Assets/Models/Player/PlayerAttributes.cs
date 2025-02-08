
public class PlayerAttributes {

    private int strength;
    public int Strength {
        get{
            return strength;
        } 
        set{
        strength = value;
    }}

    private int dexterity;
    public int Dexterity {
        get{
            return dexterity;
        } 
        set{
        dexterity = value;
    }}

    private int intelligence;
    public int Intelligence {
        get{
            return intelligence;
        } 
        set{
        intelligence = value;
    }}

    private int charisma;
    public int Charisma {
        get{
            return charisma;
        } 
        set{
        charisma = value;
    }}

    private int constitution;
    public int Constitution {
        get{
            return constitution;
        } 
        set{
        constitution = value;
    }}

    private int wisdom;
    public int Wisdom {
        get{
            return wisdom;
        } 
        set{
        wisdom = value;
    }}

    private int luck;
    public int Luck {
        get{
            return luck;
        } 
        set{
        luck = value;
    }}

    private int perception;
    public int Perception {
        get{
            return perception;
        } 
        set{
        perception = value;
    }}

    public PlayerAttributes(int strength, 
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
}