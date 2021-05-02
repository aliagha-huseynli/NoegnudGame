using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class RollBonusController : MonoBehaviour
{
    private DiceController _diceController;
    private ButtonController _buttonController;
    public Text HealthText;
    private int _bonusAttack;
    private int _bonusArmor;
    private int _bonusHp;
    [SerializeField] private DiceController diceController;

    //public void AddBonusAttackDamage()
    //{
    //    _bonusAttack = GameInfo.AttackDamage + diceController.GetRolledAttack();
    //    print($"{_bonusAttack}");
    //}

    //public void AddBonusArmor()
    //{
    //    _bonusArmor = GameInfo.Armor + diceController.GetRolledArmor();
    //    print($"{_bonusArmor}");
    //}

    //public void AddBonusHp()
    //{
    //    _bonusHp = GameInfo.CurrentHp + diceController.GetRolledHp();
    //    print($"{_bonusHp}");
    //}
}
