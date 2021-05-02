using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static string PlayerName { get; set; }
    public static int SkillPoints { get; set; }
    public static Player Player { get; set; }

    public static int AttackDamage { get; set; }
    public static int Armor { get; set; }
    public static int MaxHp { get; set; }
    public static int CurrentHp { get; set; }
    public static PlayerPrefs GenderPlayerPrefs { get; set; }
    public static PlayerPrefs PlayerNamePrefs { get; set; }
}
