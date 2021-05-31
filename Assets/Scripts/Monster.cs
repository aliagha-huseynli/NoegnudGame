using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    [SerializeField] private Text _healthText;
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private Slider _slider;
    [SerializeField] private Gradient hpGradient;
    [SerializeField] private Image fill;
    [SerializeField] private MonsterStats monsterStats;
    private Element element;

    public static event Action<int> OnMonsterAttack;
    public static event Action OnMonsterTurnPass;
    public static event Action OnMonsterDefeated;

    void Start()
    {
        health = monsterStats.Hp;
        maxHealth = monsterStats.Hp;
        element = monsterStats.Element;
        damage = monsterStats.Damage;
        UpdateUI(maxHealth, health);

        Player.OnPlayerTurnPass += Attack;
    }

    public void UpdateUI(int max, int value)
    {
        _slider.maxValue = max;
        _slider.value = value;
        fill.color = hpGradient.Evaluate((float)health / (float)maxHealth);
        _healthText.text = $"{health}/{maxHealth}";
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);

        if (IsDead())
        {
            OnMonsterDefeated?.Invoke();
        }

        UpdateUI(maxHealth, health);
    }

    private bool IsDead()
    {
        return health > 0 ? false : true;
    }

    private void OnDestroy()
    {
        Player.OnPlayerTurnPass -= Attack;
    }

    private void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    private int RandomizeDamagePercentage()
    {
        return UnityEngine.Random.Range(-2, 3);
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(2);
        OnMonsterAttack?.Invoke(damage + RandomizeDamagePercentage());
        yield return new WaitForSeconds(2);
        OnMonsterTurnPass?.Invoke();
    }
}