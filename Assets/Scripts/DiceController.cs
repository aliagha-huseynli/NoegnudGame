using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DiceController : MonoBehaviour
{
    

    private Player _player;
    //private readonly char[] _numbersRandom = { '1', '2', '3', '4', '5' };
    public Text SlotNumber1;
    public Text SlotNumber2;
    public Text SlotNumber3;
    private Text _chosenAttackDamage;
    private Text _chosenArmor;
    private Text _chosenHeal;
    public float Time;
    int attackRolled, armorRolled, hpRolled;
    public static Action OnDicesRolled;
   

    public IEnumerator SleepTime()
    {
        yield return new WaitForSeconds(3f);

        _chosenAttackDamage = GameObject.Find("AttackDamage").GetComponent<Text>();
        _chosenArmor = GameObject.Find("ArmorMagicResist").GetComponent<Text>();
        _chosenHeal = GameObject.Find("Heal").GetComponent<Text>();




        if (_chosenAttackDamage.text != "0")
        {
            print("Your Attack Damage is " + _chosenAttackDamage.text);
        }

        if (_chosenArmor.text != "0")
        {
            print("Your Armor&MagicResist is " + _chosenArmor.text);
        }

        if (_chosenHeal.text != "0")
        {
            print("Your Health Increased is " + _chosenHeal.text);
        }

       

    }

    public int GetRolledAttack()
    {
        return attackRolled;
    }

    public int GetRolledArmor()
    {
        return armorRolled;
    }

    public int GetRolledHp()
    {
        return hpRolled;
    }

    private void Update()
    {
        
        attackRolled = Random.Range(1, 6);
        armorRolled = Random.Range(1, 6);
        hpRolled = Random.Range(1, 6);
        SlotNumber1.text = attackRolled.ToString();
        SlotNumber2.text = armorRolled.ToString();
        SlotNumber3.text = hpRolled.ToString();
        OnBeingDisable();
        StartCoroutine(SleepTime());

    }

 


    public void OnBeingDisable()
    {
        Invoke(nameof(DelayNum), Time);

    }

    public void OnBeingEnable()
    {
        enabled = true;

    }

    private void DelayNum()
    {
        enabled = false;
        OnDicesRolled?.Invoke();

    }
}