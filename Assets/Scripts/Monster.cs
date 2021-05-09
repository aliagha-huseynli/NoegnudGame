using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    [SerializeField] private Text _healthText;
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    [SerializeField] private Slider _slider;
    [SerializeField] private Gradient hpGradient;
    [SerializeField] private Image fill;
    [SerializeField] private MonsterStats monsterStats;
    private Element element;

    void Start()
    {
        health = monsterStats.Hp;
        maxHealth = monsterStats.Hp;
        element = monsterStats.Element;


        UpdateUI(maxHealth, health);
    }

    public void UpdateUI(int max, int value)
    {
        _slider.maxValue = max;
        _slider.value = value;
        fill.color = hpGradient.Evaluate(health / maxHealth);
        _healthText.text = $"{health}/{maxHealth}";
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        UpdateUI(maxHealth, health);
    }
}