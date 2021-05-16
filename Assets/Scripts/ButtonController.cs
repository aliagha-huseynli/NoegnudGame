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
    public static event Action OnPlayerHeal;
    public static event Action OnPlayerGainArmor;

    private void Awake()
    {
        DiceController.OnDicesRolled += EnableButtons;
        Player.OnPlayerTurnPass += HideRollUI;
        Monster.OnMonsterTurnPass += ShowRollUI;
    }

    private void Start()
    {
        RollAd.interactable = false;
        RollArmor.interactable = false;
        RollHp.interactable = false;
    }


    private void EnableButtons()
    {
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
        OnPlayerGainArmor?.Invoke();
        RollArmor.interactable = false;
        RollAd.interactable = false;
        RollHp.interactable = false;
    }

    public void RollHpBehavior()
    {
        print("+ Hp");
        OnPlayerHeal?.Invoke();
        RollArmor.interactable = false;
        RollAd.interactable = false;
        RollHp.interactable = false;
    }

    public void HideRollUI()
    {
        gameObject.SetActive(false);
    }

    public void ShowRollUI()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        DiceController.OnDicesRolled -= EnableButtons;
        Player.OnPlayerTurnPass -= HideRollUI;
        Monster.OnMonsterTurnPass -= ShowRollUI;
    }
}