using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Monster")]
public class MonsterStats : ScriptableObject
{
    public Element Element;
    public int Damage;
    public int Hp;
}
