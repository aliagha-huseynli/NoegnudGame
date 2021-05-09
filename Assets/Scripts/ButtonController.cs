using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class ButtonController : MonoBehaviour
{
    private int _skillPoint;
    [SerializeField] private Button RollAd;
    [SerializeField] private Button RollArmor;
    [SerializeField] private Button RollHp;

    private DiceController _diceController;

    public static event Action OnPlayerAttack;

    private void Awake()
    {
        DiceController.OnDicesRolled += EnableButtons;
    }

    private void Start()
    {
        RollAd.interactable = false;
        RollArmor.interactable = false;
        RollHp.interactable = false;
    }
    

    private void EnableButtons()
    {
        print("www");
        RollAd.interactable = true;
        RollArmor.interactable = true;
        RollHp.interactable = true;
    }

    public void RollAdBehavior()
    {
        print("+ Attack Damage");
        OnPlayerAttack?.Invoke();
        RollArmor.interactable = false;
        RollHp.interactable = false;
        RollAd.interactable = false;
    }

    public void RollArmorBehavior()
    {
        print("+ Armor");
        RollArmor.interactable = false;
        RollAd.interactable = false;
        RollHp.interactable = false;
    }

    public void RollHpBehavior()
    {
        print("+ Hp");
        RollArmor.interactable = false;
        RollAd.interactable = false;
        RollHp.interactable = false;
    }
}