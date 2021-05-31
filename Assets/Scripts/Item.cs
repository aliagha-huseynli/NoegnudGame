using UnityEngine;

public enum ItemType
{
    Sword,
    Armor,
    Shield,
    Potion
}

public enum Element
{
    Fire,
    Water,
    Electric,
    None
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    //public string Name = string.Empty;
    //public Sprite Icon;
    public ItemType itemType;
    public Element itemElement;
    public int Damage = 0;
    public int Armor = 0;
    public int MaxArmor = 0;
    public int Hp = 0;
}
