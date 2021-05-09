using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DiceController : MonoBehaviour
{
    private Player _player;
    public Text SlotNumber1;
    public Text SlotNumber2;
    public Text SlotNumber3;
    [SerializeField] private Text _chosenAttackDamage;
    [SerializeField] private Text _chosenArmor;
    [SerializeField] private Text _chosenHeal;
    int attackRolled, armorRolled, hpRolled;
    public static Action OnDicesRolled;

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

    public void RollStats()
    {
        StartCoroutine(RollStatsAsync());
    }

    private IEnumerator RollStatsAsync()
    {
        float timer = 0;

        while(timer < 2)
        {
            attackRolled = Random.Range(1, 6);
            armorRolled = Random.Range(1, 6);
            hpRolled = Random.Range(1, 6);
            SlotNumber1.text = attackRolled.ToString();
            SlotNumber2.text = armorRolled.ToString();
            SlotNumber3.text = hpRolled.ToString();
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        OnDicesRolled?.Invoke();
    }
}